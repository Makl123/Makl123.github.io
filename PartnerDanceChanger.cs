using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartnerDanceChanger : MonoBehaviour
{
   [SerializeField] private LMATracker leftTracker;
    [SerializeField] private LMATracker rightTracker;

    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [Header("Kinesphere")]
    [SerializeField] private float kinesphereThreshold;
    [SerializeField] private float maxKinesphereDistance;

    [Header("Animation & Audio")]
    [SerializeField] private Animator myAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float lowSound;
    [SerializeField] private float highSound;

    [Header("Partner Movement")]
    [SerializeField] private Transform partner;
    [SerializeField] private Transform player;
    [SerializeField] private Transform restPosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    [Header("Engagement Control")]
    [SerializeField] private float engageThreshold = 0.25f;
    [SerializeField] private float disengageThreshold = 0.15f;

    [Header("Visuals")]
    [SerializeField] private GameObject[] objectsToChange;
    [SerializeField] private Material materialA;
    [SerializeField] private Material materialB;
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Color colorA = Color.blue;
    [SerializeField] private Color colorB = Color.red;

    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] private float minEmission;
    [SerializeField] private float maxEmission;
    [SerializeField] private float minParticleSpeed;
    [SerializeField] private float maxParticleSpeed;

    [Header("Light")]
    [SerializeField] private Light danceLight;
    [SerializeField] private float lightMinIntensity;
    [SerializeField] private float lightMaxIntensity;

    private float smoothBlend;
    private float engageBlend; 
    private MaterialPropertyBlock block;

    void Start()
    {
        block = new MaterialPropertyBlock();
        renderers = new Renderer[objectsToChange.Length];

        for (int i = 0; i < objectsToChange.Length; i++)
            renderers[i] = objectsToChange[i].GetComponent<Renderer>();
    }

    void Update()
    {
        if (leftTracker == null || rightTracker == null) return;

        float dt = Time.deltaTime;

       
        float distance = Vector3.Distance(leftHand.position, rightHand.position);
        float kinesphere = Mathf.InverseLerp(kinesphereThreshold, maxKinesphereDistance, distance);
        
        float timeScore = leftTracker.sudden * rightTracker.sudden; 
        float weightScore = leftTracker.strong * rightTracker.strong; 
        float spaceScore = leftTracker.direct * rightTracker.direct; 
        float flowScore = leftTracker.free * rightTracker.free; 
        
        float baseExpressiveness =
            timeScore * 0.4f +
            weightScore * 0.3f +
            spaceScore * 0.2f +
            flowScore * 0.2f;

        float synergy =
            timeScore * weightScore * spaceScore * flowScore;

        float expressiveness =
            baseExpressiveness + synergy * 0.3f;

        float expressivePower = Mathf.Pow(expressiveness, 1.5f);
        
        float targetBlend = expressivePower * Mathf.Lerp(0.2f, 1f, kinesphere);
        smoothBlend = Mathf.Lerp(smoothBlend, targetBlend, dt * 5f);
        
       
        float targetEngage = Mathf.InverseLerp(engageThreshold, 1f, smoothBlend);
        engageBlend = Mathf.Lerp(engageBlend, targetEngage, dt * 5f);

        bool isEngaged = engageBlend > 0.1f;

        
        myAnimator.SetFloat("Time", smoothBlend);
        audioSource.volume = Mathf.Lerp(lowSound, highSound, smoothBlend);

        float currentDistance = Vector3.Distance(player.position, partner.position);
        Vector3 toPlayer = (player.position - partner.position).normalized;
        Vector3 awayFromPlayer = -toPlayer;


        if (engageBlend > 0.2f)
        {
            Vector3 direction = toPlayer;

            float targetDistance = Mathf.Lerp(maxDistance, minDistance, smoothBlend);
            Vector3 targetPos = player.position - direction * targetDistance;

            float responsiveness = Mathf.Lerp(1f, 5f, smoothBlend);

            partner.position = Vector3.Lerp(
                partner.position,
                targetPos,
                dt * moveSpeed * responsiveness * engageBlend
            );
        }


        else
        {
            float proximityInfluence = Mathf.InverseLerp(3f, 0.5f, currentDistance);
            Vector3 repulsion = awayFromPlayer * (proximityInfluence * 2f);
            partner.position = Vector3.Lerp(partner.position, partner.position + repulsion, dt);
        }

       
        float visualBlend = smoothBlend;

        danceLight.intensity = Mathf.Lerp(lightMinIntensity, lightMaxIntensity, visualBlend);
        danceLight.color = Color.Lerp(Color.blue, Color.red, visualBlend);

        
        Color currentColor = Color.Lerp(colorA, colorB, visualBlend);

        foreach (Renderer r in renderers)
        {
            r.GetPropertyBlock(block);
            block.SetColor("_BaseColor", currentColor);
            block.SetColor("_Color", currentColor);
            r.SetPropertyBlock(block);
        }

       
        foreach (ParticleSystem ps in particles)
        {
            var main = ps.main;
            main.startColor = currentColor;
            main.simulationSpeed = Mathf.Lerp(minParticleSpeed, maxParticleSpeed, visualBlend);

            var emission = ps.emission;
            float boosted = Mathf.Clamp01(visualBlend * 2f);
            emission.rateOverTime = Mathf.Lerp(minEmission, maxEmission, boosted);
        }
        float fixedY = 0f;
        partner.position = new Vector3(
            partner.position.x,
            fixedY,
            partner.position.z
        );
    }
}
