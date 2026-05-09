using UnityEngine;

public class LMATracker : MonoBehaviour
{
 private string handName;

    // Motion data
    private Vector3 lastPosition;
    private Vector3 velocity;
    private Vector3 previousVelocity;
    private Vector3 acceleration;
    private Vector3 previousAcceleration;

    private float smoothedSpeed;
    private float smoothedAccel;
    private float smoothedJerk;

    [Header("Smoothing")]
    [SerializeField] private float velocitySmoothing = 10f;
    [SerializeField] private float accelSmoothing = 10f;
    [SerializeField] private float jerkSmoothing = 10f;
    [SerializeField] private float magnitudeSmoothing = 5f;

    [Header("Dead Zones")]
    [SerializeField] private float velocityDeadZone = 0.05f;
    [SerializeField] private float accelDeadZone = 0.1f;

    [Header("Threshold Ranges (Fuzzy Mapping)")]

    [Header("Time (Speed)")]
    [SerializeField] private float suddenMin = 0.5f;
    [SerializeField] private float suddenMax = 2.0f;

    [Header("Weight (Acceleration)")]
    [SerializeField] private float strongMin = 1.5f;
    [SerializeField] private float strongMax = 6f;

    [Header("Flow (Jerk / Interruptions)")]
    [SerializeField] private float jerkMin = 0.5f;
    [SerializeField] private float jerkMax = 5f;
    [SerializeField] private float stopThreshold = 0.1f;

    [Header("Space (Directional Deviation)")]
    [SerializeField] private float indirectMinAngle = 10f;
    [SerializeField] private float indirectMaxAngle = 90f;

    // ===== FUZZY OUTPUTS (0–1) =====
    public float sudden { get; private set; }
    public float sustained { get; private set; }

    public float strong { get; private set; }
    public float light { get; private set; }

    public float direct { get; private set; }
    public float indirect { get; private set; }

    public float free { get; private set; }
    public float bound { get; private set; }

    void Start()
    {
        lastPosition = transform.position;
        previousVelocity = Vector3.zero;
        previousAcceleration = Vector3.zero;
        handName = gameObject.name;
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        if (dt <= 0f) return;

       
        Vector3 rawVelocity = (transform.position - lastPosition) / dt;
        velocity = Vector3.Lerp(velocity, rawVelocity, velocitySmoothing * dt);

        if (velocity.magnitude < velocityDeadZone)
            velocity = Vector3.zero;

      
        Vector3 rawAccel = (velocity - previousVelocity) / dt;
        acceleration = Vector3.Lerp(acceleration, rawAccel, accelSmoothing * dt);

        if (acceleration.magnitude < accelDeadZone)
            acceleration = Vector3.zero;

        
        Vector3 rawJerk = (acceleration - previousAcceleration) / dt;
        float jerk = rawJerk.magnitude;
        smoothedJerk = Mathf.Lerp(smoothedJerk, jerk, jerkSmoothing * dt);

    
        smoothedSpeed = Mathf.Lerp(smoothedSpeed, velocity.magnitude, magnitudeSmoothing * dt);
        smoothedAccel = Mathf.Lerp(smoothedAccel, acceleration.magnitude, magnitudeSmoothing * dt);

        UpdateEfforts();

        previousVelocity = velocity;
        previousAcceleration = acceleration;
        lastPosition = transform.position;
    }

    void UpdateEfforts()
    {
        UpdateTime();
        UpdateWeight();
        UpdateSpace();
        UpdateFlow();
    }

    
    void UpdateTime()
    {
       
        sudden = Mathf.InverseLerp(suddenMin, suddenMax, smoothedSpeed); 
        sudden = Mathf.Clamp01(sudden); 
        sustained = 1f - sudden;
    }

   
    void UpdateWeight()
    {
        strong = Mathf.InverseLerp(strongMin, strongMax, smoothedAccel);
        strong = Mathf.Clamp01(strong);
        light = 1f - strong;
    }

   
    void UpdateSpace()
    {
        float angle = 0f;

        if (velocity.magnitude > 0.1f && previousVelocity.magnitude > 0.1f)
            angle = Vector3.Angle(previousVelocity, velocity);

        indirect = Mathf.InverseLerp(indirectMinAngle, indirectMaxAngle, angle);
        indirect = Mathf.Clamp01(indirect);
        direct = 1f - indirect;
    }

    
    void UpdateFlow()
    {
        
        float speedChange = Mathf.Abs(velocity.magnitude - previousVelocity.magnitude); 
        float continuity = 1f - Mathf.InverseLerp(0f, 1.5f, speedChange); 
        float jerkFactor = Mathf.InverseLerp(jerkMin, jerkMax, smoothedJerk); 
        free = Mathf.Clamp01((continuity * 0.6f) + ((1f - jerkFactor) * 0.4f)); 
        bound = 1f - free;
    }
 }
