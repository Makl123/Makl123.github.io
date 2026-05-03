# Makl123.github.io
Embodied Interaction Mini project:

For this mini project, the topic of External representations & computing, specifically mocap, has been chosen.
This was due to the assignment of the semester project, which was about translating a witness testimony to a virtual reality experience, which needed the sense of "response-as-if-real" (RAIR)
to immerse the user in the story told. RAIR is described as being the high level of presence, where a sensation feels so real, that the user would react as if it was real. Therefore, Affective Body Movement in VR is researched upon.

With this in mind, the paper "Affective Body Expression Perception and Recognition: A Survey" by Andrea Kleinsmith and Nadia Bianchi-Berthouze and "Perception of Affective Body Movements in HRI Across Age Groups: Comparison Between Results from Denmark and Japan" by Matthias Rehm, Anders Krogsager and Nicolaj Segatowere researched, that researches on how using the body and emotions can be used as channels of emotions and for many. Some differences in understanding the meaning of movements can lead to misinterpreting different emotions, and for a VR experience that is supposed to feel as real to immerse the user in the story told, so how can that effectively be communicated effectively through a virtual avatar, from the background to the front and center of attention?

This can be done by using Kleinsmith et al.'s research where they have compiled specific attributes from body emotions, that can work as a framework, and to test if these descriptions work, non-verbal scenes can be created in VR, where the user can then self-report what emotions they thought were presented and then follow-up questions of why they thought it was that emotion can cover if the descriptors worked. The descriptions can be translated into Laban Movement Analysis as guidance for the performance. Here, the Effort that the user gives can be calculated, and then a virtual avatar can then show an approximiation of the user's effort to show an emotional display

<img width="752" height="1244" alt="image" src="https://github.com/user-attachments/assets/bf406616-01f1-4fbd-8930-1d984a62f559" />

Table of specific body features and how they may be attributed to recognizing specific affective states [Kleinsmith et al. (2012)]

Therefore, this project will investigate how real-time movement qualities can create perceived emotional responsiveness in a virtual agent, supporting a sense of Response-As-If-Real (RAIR).

# Design

To test this out, a Virtual Reality experience is made, where the user has to use their expressiveness to get closer to a virtual agent, who will match the user's expressiveness. 

For this project, a Unity script was made, that could track the effort of the user's movement, which could then change the effort, on a virtual character. This effort is shown through two animations: One idle animation, and another that uses the space and moves more freely. This animation is then interpolated, so the agent starts to use more effort, as the user uses more effort. Therefore, the goal for this version of the system is to test whether the user feels that their effort is translated to the character or not.

The system uses an approximation of LMA, by measuring smooth velocity, acceleration, and jerk, to reduce noise, which are then run through four "Update" methods. These update methods then run the smoothed values through them and then measure through a min and max value to see where the values are and then outputs between 0-1 how much the effort is one or the other. 

These values are then given to the virtual agent, which can then calculate expressiveness and how much the user uses the kinesphere around them, to show a level of expressiveness that can match the user's effort. If the user is not expressive enough, the agent will not get closer, so to "bound" with the agent is to use the expressiveness to get the agent closer. This measurement of getting closer to the user also uses Hall's Framework to keep the agent in the social space, to not break any boundaries.
| Code  | Description |
| ------------- | ------------- |
| <img width="873" height="242" alt="image" src="https://github.com/user-attachments/assets/1fefaa5e-3c01-42fc-ba4b-949ad2e9068c" /> | **Time** <br/> <br/> Interprets faster movement as more sudden, and slower movement as more sustained. |
| <img width="847" height="197" alt="image" src="https://github.com/user-attachments/assets/89badc7b-87aa-43b1-ae98-5a504c6e0728" /> | **Weight** <br/> <br/> Interprets faster acceleration as strong, and slower accelerations as light. |
|<img width="963" height="385" alt="image" src="https://github.com/user-attachments/assets/81552e7a-d39a-471a-b9bb-17c960623583" /> | **Space** <br/> <br/> Interprets a straight path as direct, and frequent direction changes as incomplete. |
|<img width="1055" height="307" alt="image" src="https://github.com/user-attachments/assets/333e770c-283e-4a34-a317-64938235b5de" /> | **Flow** <br/> <br/> Interprets smooth and continous movement as free, and jerky and interrupted movement as bound. |


# Evaluation

To test this, this system's feeling of RAIR will be evaluated on three aspects of the project: 
- perceived expressiveness of the agent
- the boundary of the agent entering the user's space
- feeling of presence
- whether the users adjust movement based on the agent's behavior

This prototype was tested on five Medialogy masters Students. The overall response was positive with 4 out of 5 saying that they felt the avatar matched their current expressiveness and they wanted to express themselves more to see how close they could get the avatar. Furthermore, the agent entering their social space worked, as 5 out of 5 of the participants felt that it was fun to get closer and it did not break any boundaries. 
However, the feeling of presence with the avatar was not as highly praised, as the instant movement away from the user felt forced and robotic. 

# Discussion

Using a real-time avatar, that could go from low to high expressiveness, translated well to the user's expressiveness. Further design choices can help to increase the feeling of presence. This could also then be tested on more participants, as this project as of right now only has a small testing sample. Furthermore, further adjustments to the LMA system could also help to elevate the system, as these methods work as an approxmiation of the system, but cannot tell intention from the user.

## Bibliography
[1] Kleinsmith, A., & Bianchi-Berthouze, N. (2012). Affective body expression perception and recognition: A survey. IEEE Transactions on Affective Computing, 4(1), 15-33.
[2] Rehm, M., Krogsager, A., & Segato, N. (2015, October). Perception of affective body movements in HRI across age groups: Comparison between results from Denmark and Japan. In 2015 International Conference on Culture and Computing (Culture Computing) (pp. 25-32). IEEE.
