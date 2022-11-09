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
This project is a sound-based solution to help keep patients motivated in their recovery process from ACL replacement surgery. The gamified experience focuses on patients' progrssion and offers data collection during the therapy sessions.

 <img src="https://cdn.discordapp.com/attachments/1016574318827810870/1019533953989873714/unknown.png" alt="Product Name Screen Shot" >
 
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

This is an example of how to list things you need to use the software and how to install them.
* npm
  ```sh
  npm install npm@latest -g
  ```

### Installation

_Below is a template example of how to install the software._

1. Get a free API Key at [https://example.com](https://example.com)
2. Clone the repo
   ```sh
   git clone https://github.com/your_username_/Project-Name.git
   ```
3. Install NPM packages
   ```sh
   npm install
   ```
4. Enter your API in `config.js`
   ```js
   const API_KEY = 'ENTER YOUR API';
   ```
### Code structure:

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage
This is where we will show examples of how the project can be used, screenshots, code examples and demos would work.

_For more examples, please refer to the [Documentation](https://example.com)_

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

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->
## Roadmap

 _Template roadmap_
- [x] Add Changelog
- [x] Add back to top links
- [ ] Add Additional Templates w/ Examples
- [ ] Add "components" document to easily copy & paste sections of the readme
- [ ] Multi-language Support
    - [ ] Chinese
    - [ ] Spanish

See the [open issues](https://github.com/othneildrew/Best-README-Template/issues) for a full list of proposed features (and known issues).

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

_Figure out what liscence we work under_

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Ryan Stander - [@ryanstander](https://twitter.com/ryan_stander) - stander.ryan@gmail.com

Project Link: [https://github.com/RyanStander/Therapist-Battler](https://github.com/RyanStander/Therapist-Battler)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

These are some special credits for our project.

_Template acknowledgements_
* [Choose an Open Source License](https://choosealicense.com)
* [GitHub Emoji Cheat Sheet](https://www.webpagefx.com/tools/emoji-cheat-sheet)
* [Malven's Flexbox Cheatsheet](https://flexbox.malven.co/)
* [Malven's Grid Cheatsheet](https://grid.malven.co/)
* [Img Shields](https://shields.io)
* [GitHub Pages](https://pages.github.com)
* [Font Awesome](https://fontawesome.com)
* [React Icons](https://react-icons.github.io/react-icons/search)

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
