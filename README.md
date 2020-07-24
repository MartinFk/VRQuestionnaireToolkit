# VRQuestionnaireToolkit

| VRMode | DesktopMode |
| ----------- | ----------- |
| ![VRMode](http://martinfeick.com/wp-content/uploads/2020/07/VR_Mode.gif)      | ![DesktopMode](http://martinfeick.com/wp-content/uploads/2020/07/DesktopMode.gif)      |


This repository hosts the open-source VRQuestionnaireToolkit developed to ease assessing subjective measurements in Virtual Reality. It comes with an easy-to-use Unity3D package which can be integrated in existing projects.

This work is provided under a MIT License.

Please adequately cite this work, and show us your amazing projects!

We greatly appreciate any contributions and pull-requests.


## Features
1. Plug & Play integration.
2. Supports Desktop (no VR required) & Virtual Reality mode (Vive standard controller).
3. Works as build and in editor.
4. Comes with six standard questionnaire types.
5. NASA TLX, Simulation Sickness Questionnaire, IPQ and SUS Presence Questionnaire as well as System Usability Scale (SUS) already included.
6. Auto-export as .csv or .txt file.
7. Fully compatible with other frameworks 

## Downloads
- Existing VR projects (Unity package): <a href="http://martinfeick.com/wp-content/uploads/2020/07/integration.zip"> Integration </a><br>
- Standalone version (Unity package): <a href="http://martinfeick.com/wp-content/uploads/2020/07/standalone.zip"> Standalone </a><br>
- JSON Files:  <a href="http://martinfeick.com/wp-content/uploads/2020/07/jsonSamples.zip">json samples</a><br>
- Pre-print: 

## Requirements
- Unity3D 2019.x.x (https://unity.com/) -> tested on several 2019.2 and 2019.3 versions
- SteamVR (Vive) (https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647)
- Vive Input Utility (https://assetstore.unity.com/packages/tools/integration/vive-input-utility-64219)

## How To Get Started
1. Download standalone unitypackage
2. Import package into assets folder
3. Load samples scene (delete standard scene)
4. Run Demo

(For existing projects, please see Integration)

## Questiontypes

<table>
	<tr>
		<th>
			Question Type
		</th>
		<th>
			JSON Format
		</th>
	</tr>
	<tr>
		<td>
      		<h2>StartPage</h2>
			<pre>
<img src="http://martinfeick.com/wp-content/uploads/2020/07/StartPage.png">
			</pre>
		</td>
		<td>
			<pre>
{
    "qTitle":"Example Questionnaire",
    "qInstructions":"Please answer all questions....",
    "qId":"exampleQuestionnaire"
}
			</pre>
		</td>
	</tr>
	<tr>
		<td>
		<h2>Radio</h2>
			<pre>
<img src="http://martinfeick.com/wp-content/uploads/2020/07/Radio.png">
			</pre>
   <h3>Description</h3>
   <ul>
	 <li>Up to three radio questions per page</li>
	 <li>Individually configurable from 3- to 7-point Likert-type scales</li>
			</ul>
		</td>
		<td>
			<pre>
{
   "pId":"page_1",
   "qType":"radio",
   "qInstructions":"Choose multiple options",
   "qData":[
      {
         "qId":"q1",
         "qText":"UIST is an amazing conference",
         "qMandatory":"false",
         "qOptions":[
            "",
            "Strongly Disagree",
            "Disagree",
            "Neutral",
            "Agree",
            "Strongly Agree",
            ""
         ]
      }
   ]
}
			</pre>
		</td>
	</tr>
	<tr>
		<td>
	        <h2>RadioGrid</h2>      
			<pre>
<img src="http://martinfeick.com/wp-content/uploads/2020/07/5-PointGrid.png">
			</pre>
			   <h3>Description</h3>
 	<ul>
	 <li>One radio grid question per page</li>
	 <li>5- or 7-point Likert-type scale</li>
	 <li>Supports up to 4 conditions</li>
			</ul>
		</td>
		<td>
			<pre>
{
   "pId":"page_2",
   "qType":"radioGrid",
   "qInstructions":"Choose one option for each condition",
   "qOptions":[
      "",
      "Strongly Disagree",
      "Disagree",
      "Neutral",
      "Agree",
      "Strongly Agree",
      ""
   ],
   "qConditions":[
      {
         "qId":"c1",
         "qText":"Hololens"
      },
      {
         "qId":"c2",
         "qText":"Oculus Rift"
      },
      {
         "qId":"c3",
         "qText":"Vive"
      },
      {
         "qId":"c4",
         "qText":"Magic Leap"
      }
   ],
   "qData":[
      {
         "qId":"q1",
         "qText":"The system was easy to use",
         "qMandatory":"true"
      }
   ]
}
			</pre>
		</td>
	</tr>
	<tr>
		<td>
	        <h2>Checkbox</h2>      
			<pre>
<img src="http://martinfeick.com/wp-content/uploads/2020/07/checkbox.png">
			</pre>
	<h3>Description</h3>
 	<ul>
	 <li>One checkbox question per page</li>
	 <li>Supports up to 7 answers</li>
	</ul>
		</td>
		<td>
			<pre>
{
   "pId":"page_3",
   "qType":"checkbox",
   "qInstructions":"Choose all options that apply",
   "qOptions":[
      "It's too expensive",
      "The technology isn't that good yet ",
      "There is not enough content and/or games ",
      "I don't like wearing the headset ",
      "The experience isn't immersive enough",
      "The experience isn't better than traditional gaming"
   ],
   "qData":[
      {
         "qId":"q1",
         "qText":" If you don’t use VR frequently...",
         "qMandatory":"true"
      }
   ]
}
			</pre>
		</td>
	</tr>
	<tr>
		<td>
	       <h2>LinearSlider</h2>      
			<pre>
<img src="http://martinfeick.com/wp-content/uploads/2020/07/slider-even-odd.png">
			</pre>
	<h3>Description</h3>
 	<ul>
	 <li>Up to three individual sliders per page</li>
	 <li>Between three and 21 tick marks</li>
	 <li>Even number of tick marks: Start value (qMin) is pre-selected</li>
	 <li>Odd number of tick marks: Middle value is pre-selected</li>
	 <li>Note: Start value qMin = 0 required</li>
	</ul>
		</td>
		<td>
			<pre>
{
   "pId":"page_4",
   "qType":"linearSlider",
   "qInstructions":"Choose the most suitable position",
   "qData":[
      {
         "qId":"s1",
         "qText":"Temporal Demand: How hurried or rushed...",
         "qMin":"0",
         "qMinLabel":"Low",
         "qMax":"3",
         "qMaxLabel":"High"
      },
      ...
   ]
}
			</pre>
		</td>
	</tr>
	<tr>
		<td>
	        <h2>LinearGrid</h2>
			<pre>
<img src="http://martinfeick.com/wp-content/uploads/2020/07/lineargrid.png">
			</pre>
		<h3>Description</h3>
 	<ul>
	 <li>A variation of the LinearSlider question type specifically developed to support NASA TLX</li>
	 <li>Should not be used for anything else</li>
	</ul>
		</td>
		<td>
			<pre>
{
   "pId":"page_5",
   "qType":"linearGrid",
   "qInstructions":"Please rate all measures by clicking...",
   "qData":[
      {
         "qId":"s1",
         "qText":"Mental Demand: How mentally demanding...",
         "qMandatory":"true",
         "qMin":"0",
         "qMinLabel":"Low",
         "qMax":"20",
         "qMaxLabel":"High"
      },
      ...
   ]
}
			</pre>
		</td>
	</tr>
	<tr>
		<td>
	       <h2>Dropdown</h2>
			<pre>
<img src="http://martinfeick.com/wp-content/uploads/2020/07/dropdown.png">
			</pre>
	   <h3>Description</h3>
   	<ul>
	 <li>Up to three dropdown questions per page</li>
	 <li>Start value = qOptions[0]</li>
	</ul>
		</td>
		<td>
			<pre>
{
   "pId":"page_6",
   "qType":"dropdown",
   "qInstructions":"Please select the correct answer",
   "qData":[
      {
         "qId":"d1",
         "qText":"Demanding",
         "qOptions":[
            "Yes",
            "Average",
            "No"
         ]
      },
      ...
   ]
}
			</pre>
		</td>
	</tr>
	<tr>
		<td>
	       <h2>FinalPage</h2>
			<pre>
<img src="http://martinfeick.com/wp-content/uploads/2020/07/LastPage.png">
			</pre>
		</td>
		<td>
			<pre>
{
   "qMessage":"You have completed the questionnaire",
   "qAcknowledgments":"Thank you very much..."
}
			</pre>
		</td>
	</tr>
</table>

## Hierarchy
<p align="center">
	<img src="http://martinfeick.com/wp-content/uploads/2020/07/Hierarchy.png">
</p>

### Integration & Event Subscription

1. Import SteamVR and Vive Input Utility assets (if you haven't already)

2. Drag VivePointers Prefab into your scene

![VivePointer](http://martinfeick.com/wp-content/uploads/2020/07/VivePointer.gif)

3. Drag VRQuestionnaireToolkit Prefab into your scene

![VivePointer](http://martinfeick.com/wp-content/uploads/2020/07/VRQuestionnairePrefab.gif)

The package generates all required components based on the specified .json file when running the scene. You can
dis-/enable the questionnaires on demand by accessing the questionnairelist using the following code. That's all it takes =)

```
    private GameObject _vrQuestionnaireToolkit;
    private GenerateQuestionnaire _generateQuestionnaire;

    void Start()
    {
        _vrQuestionnaireToolkit = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit");
        _generateQuestionnaire = _vrQuestionnaireToolkit.GetComponentInChildren<GenerateQuestionnaire>();
    }

    void Demonstrate()
    {
         _generateQuestionnaire.Questionnaires[0].SetActive(false); // disable questionnaire 0
         _generateQuestionnaire.Questionnaires[1].SetActive(true); // enable questionnaire 1
    }
 ```


Subscribe to questionnaire events (sends an event after pressing the "Submit" button)
 ```
    private ExportToCSV _exportToCsvScript;
    private GameObject _exportToCsv;
          
    void Start()
    {
        _exportToCsv = GameObject.FindGameObjectWithTag("ExportToCSV");
        _exportToCsvScript = _exportToCsv.GetComponent<ExportToCSV>();
        _exportToCsvScript.QuestionnaireFinishedEvent.AddListener(YourFunction); // e.g, call next questionnaire from list
    }       	
 ```


## Manual

1. Configure experimental metadata (participant number etc,).

![Config](http://martinfeick.com/wp-content/uploads/2020/07/Config.gif)

2. Copy-paste json path to include your custom questionnaire.

![IncludeJson](http://martinfeick.com/wp-content/uploads/2020/07/ImportQuestionnaire.gif)

3. Set output path, filename, delimiter and type.

Outputfile naming convention:
```
_path = _folderPath + "questionnaireID_" + _questionnaireID + "_participantID_" + _studySetup.ParticipantId + "_condition_" + _studySetup.Condition + "_" + FileName + "." + _fileType;
```

![SetFolder](http://martinfeick.com/wp-content/uploads/2020/07/SetFolder.gif)

4. World-anchor questionnaire in 3D environment

![World-anchoring](http://martinfeick.com/wp-content/uploads/2020/07/SetWorldAnchor.gif)


## Known Issues
1) Do not use your selected delimiter ( "," by default) in your question
2) When HTC Vive connected, Desktop mode does not work (restart Unity3D required)
3) Large/Small Vive laser dot -> dis-/enable rectile autoscale in VivePointers Right and Left

## Work in Progress
1) Add additional standardized questionnaires
2) Support "effective" text input
3) Oculus support

## Issues
If you face any problems while using the toolkit, please open an issue here - https://github.com/MartinFk/VRQuestionnaire/issues or contact us under martin.feick@dfki.de.

## Dependencies 
Simple Json Parser: https://github.com/Bunny83/SimpleJSON

## Questionnaire References
1.	John Brooke. 1986. System usability scale (SUS): a quick-and-dirty method of system evaluation user information. Reading, UK: Digital Equipment Co Ltd 43
2.	Sandra G. Hart and Lowell E. Staveland. 1988. Development of NASA-TLX (Task Load Index): Results of Empirical and Theoretical Research. In Advances in Psychology, Peter A. Hancock and Najmedin Meshkati (eds.). North-Holland, 139–183. https://doi.org/10.1016/S0166-4115(08)62386-9
3.	Robert S. Kennedy, Norman E. Lane, Kevin S. Berbaum, and Michael G. Lilienthal. 1993. Simulator Sickness Questionnaire: An Enhanced Method for Quantifying Simulator Sickness. The International Journal of Aviation Psychology 3, 3: 203–220. https://doi.org/10.1207/s15327108ijap0303_3
4.	Holger Regenbrecht and Thomas Schubert. 2002. Real and Illusory Interactions Enhance Presence in Virtual Environments. Presence: Teleoperators and Virtual Environments https://doi.org/10.1162/105474602760204318
5.	Martin Usoh, Ernest Catena, Sima Arman, and Mel Slater. 2000. Using Presence Questionnaires in Reality. Presence: Teleoperators and Virtual Environments

