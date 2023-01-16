<a name="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/RyanStander/Therapist-Battler">
    <img src="https://www.saxion.nl/binaries/content/assets/over-saxion/organisatie/toolkit/lg_saxion_rgb.png" alt="Logo" width="240" height="90">
  </a>

  <h3 align="center">Therapist Battler</h3>

  <p align="center">
    Welcome to the AMI ACL Rehabilitation game repository. You are most likely a CMGT student continueing our project. Below you will find a guide explaining how to work with the software and hardware!
    <br />
    <a href="https://github.com/RyanStander/Therapist-Battler"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/RyanStander/Therapist-Battler">View Demo</a>
    ·
    <a href="https://github.com/RyanStander/Therapist-Battler/issues">Report Bug</a>
    ·
    <a href="https://github.com/RyanStander/Therapist-Battler/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project
This project is a sound-based solution to help keep patients motivated in their recovery process from ACL replacement surgery. The gamified experience focuses on patients' progression and offers data collection during the therapy sessions.
 
<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With
Below is a list of applications used for building our project. Make sure to familiarise yourself with these before attempting to work on it

* [![Unity3D][Unity.com]][Unity-url]
* [![Xsens][Xsens.com]][Xsens-url]
* [![Cubase Pro12][Cubase.com]][Cubase-url]
* [![Office Suite][Office.com]][Office-url]
* [![Adobe][Adobe.com]][Adobe-url]
* [![FL Studio][FLStudio.com]][FLStudio-url]
* [![FMOD][Fmod.com]][Fmod-url]
* [![JetBrains Rider][JetBrains.com]][JetBrains-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started
Below we will discuss how to get everything you need to run the Therapy project in Unity

### Prerequisites

#### Setting Up Xsens Awinda Suit with Ruelsing systems

_All exercises are recorded with the same strap placements and it is important for all users to wear it the same as to prevent odd rotation tracking and other potential issues. Below we will show the strap placements of the lower body as well as where sensors are to be placed._

The first step is to put on the Xsens shirt, you can replace this with an extra strap, if available, but it might cause problems and is thus not recommended. AFter that place on a strap above your belt as shown:

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SuitSetup/TorsoNPose.jpg" alt="Torso straps and shirt shown" width="440" >
 
The next step is the leg straps, you can place the straps the exact same way on the opposite leg, the upper leg strap must be placed slightly above on the thigh and the lower leg strap must be placed on the part where your calf bends inwards as this prevents the strap from slipping:

 <img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SuitSetup/RightLegWithStraps.jpg" alt="Upper and lower leg straps shown" width="440">

The foot strap is placed on the center of the foot as to prevent moving it when bending the foot, alternatively you can placed it inside the tongue of the shoe but this does not work for all shoes and so a strap is placed as shown:

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SuitSetup/RightFootWithStrap.png" alt="right foot strap shown" width="440">

Following this is the sensors, it is recommended to place these inside the strap so that it does not move as much.

The sternum sensor can be placed inside the pocket.

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SuitSetup/SternumSensorPlacement.jpg" alt="Sternum Sensor Placement" width="440">

The pelvis sensor is placed at the center of behind you.

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SuitSetup/PelvisSensorPlacement.jpg" alt="Pelvis Sensor Placement" width="440">

The upper leg sensors is placed on the outside of your legs (still inside the strap).

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SuitSetup/RightUpperLegSensorPlacement.jpg" alt="Right Upper Leg Sensor Placement" width="440">

The lower leg sensor is placed inbetween the inside and the center of the leg.

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SuitSetup/LowerLegSensorPlacement.jpg" alt="Lower Leg Sensor Placement" width="440">

Lastly the foot sensor is placed on the front of the foot as shown.

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SuitSetup/RightFootSensor.jpg" alt="Right Foot Sensor" width="440">

This can be mirrored

Unity version 2021.3.6f1

### Installation

_Setting up fusion clinic software_

[Unity Install Instructions](https://learn.unity.com/tutorial/install-the-unity-hub-and-editor#6273ef3cedbc2a0924192d2f)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->
## Usage

### Creating Levels
One of the main points this project was designed for was making the creation of levels rely on no extra scripting. Below you will find a step guide on how to create a level.

#### Creating Exercises - Pose Data Sets
_The first step would be making the exercise in Xsens, this explenation will be added at a later point_

##### Character Setup
After creating a recording of an exercise the next step is setting up a character in Unity for creating PoseDataSet. The following are requirements for setting up a Pose Data:
* A humanoid rigged model with an animator attached.
* The ModelBodyPoints component attached and each of its fields filled in with the respective body part in the rig.
* The ModelBodyPointsSaver script which will allow you to save all the data necessary for creating exercises. Make sure to add the ModelBodyPoints component to its field.
* Make sure that the animator has a controller with at least 1 animation in it, this will be necessary later.
Once all of this is setup, it should look something like this in the inspector:
 <img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/ModelBodyPointsSetup.png" alt="Model Body Points Setup" >

##### Creating PoseDataSet
The next step is for creating an exercise, this will be all done through the ModelBodyPointsSaver that you made in the previous step. Below is a list of all of the fields and their function:
* Model Body Points - A reference to the ModelBodyPoints component, this is used for getting the rotation data
* Pose Data Set To Save To - A reference to the PoseDataSet where each PoseData will be saved to, if you do not have one, you can use the saver to create it which will be explained how at a later point.
* Add To Pose Data Set - When checked it will add the PoseData to the PoseDataSet, this means it will be placed at the bottom of the list of PoseDatas.
* Save Path - This describes where the asset will be saved to, it is recommended to start the save path at "Assets/ScriptableObjects/Exercises/Poses/" and then add an extra path to the folder for each different exercise such as "Squat/" for example, make sure the folder is created, it does not create one if it cannot find it to avoid other user errors.
* Pose Set Save Name - This is what the PoseDataSet will be named when you create it, this is only relevant if the PoseDataSet does not already exist i.e. you have not yet created one.
* Pose Save Name - The name of the pose of the current exercise point. Imagine this like checkpoints in an exercise, you would want to create them at very vital parts of an exercise such as for a squat you'd want a standing pose, a squatting down pose, and then another standing pose. So, for the squatting down you would call it "Squat-Down".
* BUTTON Create Holder For Pose - Pressing this button assuming other fields are correctly filled in, will create a holder and assign it to PoseDataSetToSaveTo field.
* BUTTON Save Model Body Points - pressing this button assuming other fields are correctly filled in and set up, will create a pose, making sure a pose is correctly saved will be described below.

Here is an example of a fully set up Model Body Points Saver:

 <img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/FullySetupModelBodyPointsSaver.png" alt="Fully set up model body points saver" >

1. If you have all the other parts set up, you should be ready for the next part, which is creating a pose set, you will need a few windows open: 
    * Scene
    * Animation
    * Inspector
2. Make sure you have the character with the model body points saver selected.
3. Select the animation you want to use to create the exercise.
4. Move the slider to the point you want, you can see where you are by looking in the scene at the character. If you did this wrong the character wont move when previewing the animation.
5. Once happy with the character pose, press the Save Model Body Points button and it will save it to the selected location
6. Repeat until you make a full clip of an exercise, you don't need too many, try to keep it only to vital parts of a movement where limbs might be to their maximum extensions.

Here is an example of the scene setup and choosing a specific pose

 <img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/SquatAnimationCreation.png" alt="Squatting down point for creating pose" >

#### Creating Game Events

Game events are sections of a level, there are 3 different game events, namely:
* Dialogue Events: Used when purely voice overs are necessary, used to describe an environment, generally if you want to explain something without needing an exercise
* Puzzle Events: Used when there is a certain action that needs to be performed to progress further in the level, can be multiple exercises or one, each one having its own voice line. These are useful if you want to have some type of event happen such as moving an object out of the players way with an exercise.
* Fighting Events: Used when the player encounters an enemy. This contains a set of exercises that the player must continously complete and it cycles until the enemy has been killed. 

##### How To Make A Game Event
_Creating a game event is the same as any other scriptable object, an explenation is below in case:_
1. Navigate to the project window in unity
2. Find a folder you would like to create it in
3. In an open spot, right click to open the menu
4. Navigate to Create> Scriptable Objects > Game Events > [Game Event of choice]
5. Select it and give it a name

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/CreatingGameEvents.png" alt="Creating Game Events" >

_Below will be an explenation of each the fields of each event_

##### General Fields
There are a few fields that is present on all kinds of events, namely:
* Override Currently playing music - A toggle when active it will override the music currently playing.
* Override Music - The music that will override the currently active song, only used when the toggle is active.
* Background Sprite - can be left empty, but when it has an image it will change the background scene, useful if the player reaches a new location.


##### Dialogue Event
<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/DialogueEvent.png" alt="Dialogue Event Example" >

* Dialogue Clip - The voice line played, once it finishes it moves on to the next event.
 
##### Puzzle Event
<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/PuzzleEvent.png" alt="Puzzle Event Example" >

* Exercise Data - This is an array that contains a few fields, each of these is for performing a certain exercise. They contain the following:
  * Exercise to perform - This is a pose data set that you made in the previous section, so whatever you put here will be the exercise that the player performs
  * Voice line to play - This will be said to inform the player of what they have to do.
  * Sprite To Show - This will be put on an image, it could be an exercise or it could be a character, what exactly it is has yet to be decided on.

##### Fighting Event
<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/FightingEvent.png" alt="Fighting event example" >

* Enemy Sprite - The sprite that is displayed for the enemy in combat
* Enemy Health - How much health the enemy has.
* Enemy Damage - The amount of damage the enemy does to the player.
* Enemy Attack Sounds - The sounds the enemy makes when attacking.
* Enemy Attacked Sounds - The sounds the enemy makes when he is attacked.
* Player Attack Sequence - This is the list that will keep repeating exercsises. It has the following fields:
  * Exercise Name - This is an audio clip that will tell the player what exercise they must perofmr
  * Player Attack - This is a list of the attacks, the more in this list the more the player has to perform before completing a sequence.

#### Creating a level
_You've made it to the end, congratulations!_
This one's rather simple, the same path as creation a Game Event, find the Game Event Holder in that section and create it. Once created and named approapriately, you have the following fields to fill:

<img src="https://github.com/RyanStander/Therapist-Battler/blob/main/ImagesIconsForReadMe/LevelExample.png" alt="Level Example" >

* Starting Background - The background that is loaded on launch
* Game Events - this is where you put all the events you made, the 0th in the list is played first and the rest in order.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

MIT License

Copyright (c) 2022 Ryan Stander

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Ryan Stander - [@ryanstander](https://twitter.com/ryan_stander) - stander.ryan@gmail.com

Project Link: [https://github.com/RyanStander/Therapist-Battler](https://github.com/RyanStander/Therapist-Battler)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

These are some special credits for our project.

_Acknowledgements_
* [Choose an Open Source License](https://choosealicense.com)
* [Img Shields](https://shields.io)
* [React Icons](https://react-icons.github.io/react-icons/search)
* [Night Cafe](https://creator.nightcafe.studio)
* [Replica Studios](https://replicastudios.com)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/RyanStander/Therapist-Battler.svg?style=for-the-badge
[contributors-url]: https://github.com/RyanStander/Therapist-Battler/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/RyanStander/Therapist-Battler.svg?style=for-the-badge
[forks-url]: https://github.com/RyanStander/Therapist-Battler/network/members
[stars-shield]: https://img.shields.io/github/stars/RyanStander/Therapist-Battler.svg?style=for-the-badge
[stars-url]: https://github.com/RyanStander/Therapist-Battler/stargazers
[issues-shield]: https://img.shields.io/github/issues/RyanStander/Therapist-Battler.svg?style=for-the-badge
[issues-url]: https://github.com/RyanStander/Therapist-Battler/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/ryan-stander/
[product-screenshot]: images/screenshot.png
[Unity.com]: https://img.shields.io/badge/Unity-000000?style=for-the-badge&logo=unity&logoColor=white
[Unity-url]: https://unity.com
[Xsens.com]: https://img.shields.io/badge/Xsens-ff8800?style=for-the-badge&logo=XState&logoColor=white
[Xsens-url]: https://www.xsens.com
[Ableton.com]: https://img.shields.io/badge/Ableton-000000?style=for-the-badge&logo=abletonlive&logoColor=white
[Ableton-url]: https://www.ableton.com/en/
[Cubase.com]: https://img.shields.io/badge/Cubase-b30c00?style=for-the-badge&logo=abletonLive&logoColor=white
[Cubase-url]: https://www.steinberg.net/cubase/
[Adobe.com]: https://img.shields.io/badge/Adobe-b30c00?style=for-the-badge&logo=adobe&logoColor=white
[Adobe-url]: https://www.adobe.com/#
[Office.com]: https://img.shields.io/badge/Office-000000?style=for-the-badge&logo=microsoftoffice&logoColor=white
[Office-url]: https://www.office.com
[FLStudio.com]: https://img.shields.io/badge/FLStudio-000000?style=for-the-badge&logo=instacart&logoColor=white
[FLStudio-url]: https://www.image-line.com
[Fmod.com]: https://img.shields.io/badge/FMOD-000000?style=for-the-badge&logo=fmod&logoColor=white
[Fmod-url]: https://www.fmod.com
[JetBrains.com]: https://img.shields.io/badge/JetBrains-000000?style=for-the-badge&logo=jetbrains&logoColor=white
[JetBrains-url]: https://www.jetbrains.com/rider/
