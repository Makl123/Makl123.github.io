# Makl123.github.io
Embodied Interaction Mini-project:

This miniproject aims to support the main semester project, which is about translating a witness testimony to a virtual reality experience, with a goal of evoking Response-As-If-Real (RAIR) to immerse the user in the narrative told in the experience. RAIR is described as being the high level of presence, where a sensation feels so real that the user would react as if it was real. Presence can be elevated through auditory and visual design, so RAIR through virtual agents could potentially be elevated through human-like movement. Therefore, affective body movement in VR is explored as a method of enhancing perceived realism and emotional engagement.

With this in mind, the paper "Affective Body Expression Perception and Recognition: A Survey" by Andrea Kleinsmith and Nadia Bianchi-Berthouze and "Perception of Affective Body Movements in HRI Across Age Groups: Comparison Between Results from Denmark and Japan" by Matthias Rehm, Anders Krogsager and Nicolaj Segatowere are researched, which investigate how bodily movement functions as a channel for emotional expression and perception. Some differences in understanding the meaning of movements can lead to misinterpreting different emotions, and for a VR experience, that is supposed to feel as real to immerse the user in the story told. So how can that effectively be communicated through a virtual avatar, from the background to the front and center of attention?

This can be done by using Kleinsmith et al.'s research where they have compiled specific attributes from body emotions, that can work as a framework. To test if these descriptions work, non-verbal scenes can be created in VR, where the user can then self-report what emotions they thought were presented and then follow-up questions of why they thought it was that emotion can cover if the descriptors worked. The descriptions can be translated into Laban Movement Analysis as guidance for the performance. Here, the system can approximate LMA Effort qualities from user movement, and then a virtual agent can then show an approximation of the user's effort to show an emotional display.

<img width="752" height="1244" alt="image" src="https://github.com/user-attachments/assets/bf406616-01f1-4fbd-8930-1d984a62f559" />

Table of specific body features and how they may be attributed to recognizing specific affective states [Kleinsmith et al. (2012)]

This can work as a way to see if the users see the agent's body movement response as fitting, however this mini-project also wants to incorporate an Embodied Interaction, by incorporating the user's movement. Therefore, this project will investigate how real-time movement, using LMA as descriptors, can create perceived emotional responsiveness in a virtual agent, supporting a sense of Response-As-If-Real (RAIR).

# Design

To test this out, a virtual reality experience was developed, where the user has to use their expressiveness to get closer to a virtual agent, who will match the user's expressiveness. The system takes in bodily movement through the VR controllers, as input, and then outputs bodily expressions through the avatar, which the user perceives and responds to, based on what they perceive in the agent's movement, and then the loop continues with the user then giving new bodily movement input. 

To do this, a Unity script was made, that could track the effort of the user's movement, which could then change the effort, on a virtual character. This effort is shown through two animations: one idle animation, and another that uses the space and moves more freely. This animation is then interpolated, so the agent's effort can work like a scale from low to high expressiveness, with the agent's body using more space and becoming more expressive. Therefore, the goal for this version of the system is to test whether the user feels that their effort is translated to the character or not, and if the response feels real.

| Dance  | Description |
| ------------- | ------------- |
| <img width="960" height="540" alt="image" src="https://github.com/user-attachments/assets/dce43b76-a9b8-489e-b691-99aa45aa9eec" /> | **Low Expression Level** <br/> <br/> The agent stands idle, using as little space as possible |
| <img width="960" height="540" alt="image" src="https://github.com/user-attachments/assets/b007505d-69e4-43e4-beb6-6aff3110f925" /> | **Medium Expression Level** <br/> <br/> The agent starts to move, slowly becoming more expressive and using their kinesphere |
|<img width="960" height="540" alt="image" src="https://github.com/user-attachments/assets/1bd3258b-9862-4a71-b99b-c94be0ba86a1" /> | **High Expression Level** <br/> <br/> The agent moves fast, using expressive body language and using their kinesphere |


The system uses scripts that are inspired by the LMA descriptors, and approximately maps them by measuring smooth velocity, acceleration, and jerk, to reduce noise, which are then run through four "Update" methods. These update methods then run the smoothed values through them and then measure through a min and max value to see where the values are and then outputs between 0-1 how much the effort is one or the other. 

These values are then given to the virtual agent, which can then calculate expressiveness and how much the user uses the kinesphere, which is the space reachable by the body, to show a level of expressiveness that can match the user's effort. To make an approximation of kinesphere usage, the system measures the distance between the hands as an indicator of how much space the user occupies. If the user is not expressive enough, the agent will not get closer, so engaging with the agent requires expressive user movement to get the agent closer. This measurement of getting closer to the user also uses Hall's theory of Proxemics to keep the agent in the social space, to not break any boundaries.

| Code  | Description |
| ------------- | ------------- |
| <img width="873" height="242" alt="image" src="https://github.com/user-attachments/assets/1fefaa5e-3c01-42fc-ba4b-949ad2e9068c" /> | **Time** <br/> <br/> Interprets velocity with faster movement as more sudden, and slower movement as more sustained. Velocity is used as a method to map the rate of change in the hand movements, to see if the rate of change is sudden or sustained. |
| <img width="847" height="197" alt="image" src="https://github.com/user-attachments/assets/89badc7b-87aa-43b1-ae98-5a504c6e0728" /> | **Weight** <br/> <br/> Interprets faster acceleration as strong, and slower accelerations as light. Acceleration is used as a method to map the velocity change in the hand movements to see if the acceleration is strong or light. |
|<img width="963" height="385" alt="image" src="https://github.com/user-attachments/assets/81552e7a-d39a-471a-b9bb-17c960623583" /> | **Space** <br/> <br/> Interprets a straight path as direct, and frequent direction changes as indirect. Uses the Vector3.Angle to see whether the trajectory is direct or indirect. |
|<img width="1055" height="307" alt="image" src="https://github.com/user-attachments/assets/333e770c-283e-4a34-a317-64938235b5de" /> | **Flow** <br/> <br/> Interprets smooth and continuous movement as free, and jerky and interrupted movement as bound. Uses jerk and continuous movement to map whether the speed change is free or bound. |
|<img width="1128" height="94" alt="image" src="https://github.com/user-attachments/assets/4e698ec2-a3bd-41e9-bea0-d6602a257860" /> | **Kinesphere** <br/> <br/> Uses the distance between the hands to measure how much of the user's kinesphere the user is using. |


# Evaluation

To test this, the system’s ability to evoke RAIR will be evaluated on four aspects of the project: 
- perceived expressiveness of the agent
- the boundary of the agent entering the user's space
- feeling of presence
- whether the users adjust movement based on the agent's behavior

After testing the prototype, the participants will be asked to self-report how they perceived each aspect on a five-point Likert scale. They were also asked if they wanted to elaborate on why they answered the way they did.

This prototype was tested on five Medialogy master's students. The overall response was positive with the question of perceived expressiveness and adjustments of movements having mean scores of 4 and 4.2 respectively. The participants said that they felt the avatar matched their current expressiveness and they wanted to express themselves more to see how close they could get the avatar. Furthermore, the agent entering their social space worked well, as the mean ended up being 4.6, with the participants feeling that it was fun to get the agent closer and it did not break any boundaries. 
However, the sense of presence was rated lower, with a mean of 2.6, as the agent’s sudden movement away from the user felt forced and robotic and broke the immersion for one of the participants. Two of the participants also suggested more animations, as the avatar felt "fun to move with", but felt a bit robotic in general, which suggests that more animations could maybe increase the level of RAIR from a more realistic moving partner. 

# Discussion

Using a real-time avatar, that could go from low to high expressiveness, translated well to the user's expressiveness. However, as the feeling of presence fell as the avatar only contains the limited animations, adding more to make the avatar feel more realistic could help to increase the immersion. This could also then be tested on more participants, as this project as of right now only has a small testing sample, and the project is then more exploratory than having solid findings. Furthermore, further adjustments to the LMA system could also help to elevate the system, as these methods work as an approximation of the system, but cannot tell intention from the user. This did not affect the participants' experience, but it is important to address that this LMA system currently only works as an approximation of the kinematics.

## Bibliography
[1] Hall, E. T., Birdwhistell, R. L., Bock, B., Bohannan, P., Diebold Jr, A. R., Durbin, M., ... & Vayda, A. P. (1968). Proxemics [and comments and replies]. Current anthropology, 9(2/3), 83-108.

[2] Kleinsmith, A., & Bianchi-Berthouze, N. (2012). Affective body expression perception and recognition: A survey. IEEE Transactions on Affective Computing, 4(1), 15-33.

[3] Laban, R. (1947). with Lawrence, FC Effort: Economy of Human Movement. London: MacDonald and Evans.

[4] Rehm, M., Krogsager, A., & Segato, N. (2015, October). Perception of affective body movements in HRI across age groups: Comparison between results from Denmark and Japan. In 2015 International Conference on Culture and Computing (Culture Computing) (pp. 25-32). IEEE.

