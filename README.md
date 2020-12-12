<img src="http://martinfeick.com/wp-content/uploads/2020/11/vrquestionnairetoolkit.png">

| VRMode | DesktopMode |
| ----------- | ----------- |
| ![VRMode](http://martinfeick.com/wp-content/uploads/2020/07/VR_Mode.gif)      | ![DesktopMode](http://martinfeick.com/wp-content/uploads/2020/07/DesktopMode.gif)      |


This repository hosts the open-source VRQuestionnaireToolkit developed to ease assessing subjective measurements in Virtual Reality. It comes with an easy-to-use Unity3D package which can be integrated in existing projects supporting pre-, in situ- and post-study questionnaires.

This work is provided under a MIT License.

Please adequately <a href="https://github.com/MartinFk/VRQuestionnaireToolkit/blob/master/VRQuestionnaireToolkit.bib"> cite</a> this work, and show us your amazing projects!

```
@inproceedings{feick2020vrqt,
author = {Feick, Martin and Kleer, Niko and Tang, Anthony and Kr\"{u}ger, Antonio},
title = {The Virtual Reality Questionnaire Toolkit},
year = {2020},
isbn = {9781450375153},
publisher = {Association for Computing Machinery},
address = {New York, NY, USA},
url = {https://doi.org/10.1145/3379350.3416188},
doi = {10.1145/3379350.3416188},
location = {Virtual Event, USA},
series = {UIST '20 Adjunct}
}
```

We greatly appreciate any contributions and pull-requests (please submit to dev branch).


## Features
1. ⚡Plug & Play integration.
2. 🚩Supports Desktop (no VR required) & Virtual Reality mode (HTC VIVE or Oculus controller).
3. 🍏Works as build and in editor.
4. 👓Comes with six standard questionnaire types.
5. 📙NASA TLX, Simulation Sickness Questionnaire, IPQ and SUS Presence Questionnaire as well as System Usability Scale (SUS) already included.
6. 📁Auto-export as .csv or .txt file.
7. 🌌Fully compatible with other frameworks.
8. 🍒Customizable tactile and sound feedback.

## Downloads
- 🍧Existing VR projects (Unity package): <a href="http://martinfeick.com/wp-content/uploads/2020/07/integration.zip" target="_blank" rel="noopener noreferrer"> Integration </a><br>
- 🍪Standalone version (Unity package): <a href="http://martinfeick.com/wp-content/uploads/2020/07/standalone.zip" target="_blank" rel="noopener noreferrer"> Standalone </a><br>
- 🔖JSON Files:  <a href="http://martinfeick.com/wp-content/uploads/2020/07/jsonSamples.zip" target="_blank" rel="noopener noreferrer">json samples</a><br>
- 🎓Paper: <a href="https://dl.acm.org/doi/abs/10.1145/3379350.3416188" target="_blank" rel="noopener noreferrer"> UIST 20 - Extended Abstracts</a><br>
- 📋BibTex: <a href="https://github.com/MartinFk/VRQuestionnaireToolkit/blob/master/VRQuestionnaireToolkit.bib" target="_blank" rel="noopener noreferrer"> VRQuestionnaireToolkit.bib</a><br>
- 🔓Pre-print: <a href="http://martinfeick.com/wp-content/uploads/2020/08/VRQuestionnaireToolkit.pdf" target="_blank" rel="noopener noreferrer">Pre-Print</a><br>
- 🎥Preview: <a href="http://martinfeick.com/wp-content/uploads/2020/08/VRquestionnaireToolkit.mp4" target="_blank" rel="noopener noreferrer">Video</a><br>

## Requirements
- Unity3D 2019.x.x (https://unity.com/) -> tested on several 2019.2 and 2019.3 versions
- SteamVR (https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647)
- Vive Input Utility (https://assetstore.unity.com/packages/tools/integration/vive-input-utility-64219)

## How To Get Started ☀️
1. Download standalone unitypackage
2. Import package into assets folder
3. Load samples scene (delete standard scene)
4. Run Demo

(For existing projects, please see <a href="https://github.com/MartinFk/VRQuestionnaireToolkit/wiki/%F0%9F%9B%A0%EF%B8%8FIntegration-&-Event-Subscription"> Wiki/Integration</a>)

## Documentation
Visit the <a href="https://github.com/MartinFk/VRQuestionnaireToolkit/wiki"> Wiki 📘</a>  for full documentation and more information.

## Known Issues
1) Do not use your selected delimiter (e.g. ";") in your question
2) When VR headset is connected, Desktop mode does not work (restart Unity3D required)
3) Large/Small Vive laser dot -> dis-/enable rectile autoscale in VivePointers Right and Left

## Issues
If you face any problems while using the toolkit, please open an issue here - https://github.com/MartinFk/VRQuestionnaire/issues or contact us under martin.feick@dfki.de 📫.

## Dependencies 
Simple Json Parser: https://github.com/Bunny83/SimpleJSON

## Questionnaire References
1.	John Brooke. 1986. System usability scale (SUS): a quick-and-dirty method of system evaluation user information. Reading, UK: Digital Equipment Co Ltd 43
2.	Sandra G. Hart and Lowell E. Staveland. 1988. Development of NASA-TLX (Task Load Index): Results of Empirical and Theoretical Research. In Advances in Psychology, Peter A. Hancock and Najmedin Meshkati (eds.). North-Holland, 139–183. https://doi.org/10.1016/S0166-4115(08)62386-9
3.	Robert S. Kennedy, Norman E. Lane, Kevin S. Berbaum, and Michael G. Lilienthal. 1993. Simulator Sickness Questionnaire: An Enhanced Method for Quantifying Simulator Sickness. The International Journal of Aviation Psychology 3, 3: 203–220. https://doi.org/10.1207/s15327108ijap0303_3
4.	Holger Regenbrecht and Thomas Schubert. 2002. Real and Illusory Interactions Enhance Presence in Virtual Environments. Presence: Teleoperators and Virtual Environments https://doi.org/10.1162/105474602760204318
5.	Martin Usoh, Ernest Catena, Sima Arman, and Mel Slater. 2000. Using Presence Questionnaires in Reality. Presence: Teleoperators and Virtual Environments

