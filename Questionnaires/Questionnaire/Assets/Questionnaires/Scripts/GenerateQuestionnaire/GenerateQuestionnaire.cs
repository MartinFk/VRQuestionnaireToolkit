using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

/// <summary>
/// GenerateQuestionnaire.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class GenerateQuestionnaire : MonoBehaviour
    {
        public string JsonInputPath_1;
        public string JsonInputPath_2;
        public string JsonInputPath_3;
        public string JsonInputPath_4;
        public string JsonInputPath_5;
        public string JsonInputPath_6;
        public string JsonInputPath_7;
        public string JsonInputPath_8;
        public string JsonInputPath_9;
        public string JsonInputPath_10;

        private List<string> JsonInputFiles;
        public List<GameObject> Questionnaires; // list containing all questionnaires 

        private PageFactory _pageFactory;
        private ExportToCSV _exportToCsvScript;
        private GameObject _exportToCsv;
        public GameObject questionnaire;
        public RectTransform QuestionRecTest;

        private JSONArray _qData;
        private JSONArray _qConditions;
        private JSONArray _qOptions;

        private GameObject currentQuestionnaire;
        private int numberQuestionnaires;
        private string qId;
        private string pId;

        private void FireEvent()
        {
            print("QuestionnaireFinishedEvent");
        }

        void Start()
        {
            _exportToCsv = GameObject.FindGameObjectWithTag("ExportToCSV");
            _exportToCsvScript = _exportToCsv.GetComponent<ExportToCSV>();
            _exportToCsvScript.QuestionnaireFinishedEvent.AddListener(FireEvent);

            numberQuestionnaires = 1;
            Questionnaires = new List<GameObject>();
            JsonInputFiles = new List<string>();

            if (JsonInputPath_1 != "")
            {
                JsonInputFiles.Add(JsonInputPath_1);
            }
            if (JsonInputPath_2 != "")
            {
                JsonInputFiles.Add(JsonInputPath_2);
            }
            if (JsonInputPath_3 != "")
            {
                JsonInputFiles.Add(JsonInputPath_3);
            }
            if (JsonInputPath_4 != "")
            {
                JsonInputFiles.Add(JsonInputPath_4);
            }
            if (JsonInputPath_5 != "")
            {
                JsonInputFiles.Add(JsonInputPath_5);
            }
            if (JsonInputPath_6 != "")
            {
                JsonInputFiles.Add(JsonInputPath_6);
            }
            if (JsonInputPath_7 != "")
            {
                JsonInputFiles.Add(JsonInputPath_7);
            }
            if (JsonInputPath_8 != "")
            {
                JsonInputFiles.Add(JsonInputPath_8);
            }
            if (JsonInputPath_9 != "")
            {
                JsonInputFiles.Add(JsonInputPath_9);
            }
            if (JsonInputPath_10 != "")
            {
                JsonInputFiles.Add(JsonInputPath_10);
            }


            foreach (string InputPath in JsonInputFiles)
                GenerateNewQuestionnaire(InputPath);

            for (int i = 1; i < Questionnaires.Count; i++)
                Questionnaires[i].SetActive(false);

            Questionnaires[0].SetActive(true);

        }

        void GenerateNewQuestionnaire(string inputPath)
        {
            if (numberQuestionnaires > 1)
                currentQuestionnaire.SetActive(false);

            currentQuestionnaire = Instantiate(questionnaire);
            currentQuestionnaire.name = "Questionnaire_" + numberQuestionnaires;

            // Place in hierarchy 
            RectTransform radioGridRec = currentQuestionnaire.GetComponent<RectTransform>();
            radioGridRec.SetParent(QuestionRecTest);
            radioGridRec.localPosition = new Vector3(0, 0, 0);
            radioGridRec.localRotation = Quaternion.identity;
            radioGridRec.localScale = new Vector3(radioGridRec.localScale.x * 0.01f, radioGridRec.localScale.y * 0.01f, radioGridRec.localScale.z * 0.01f);

            _pageFactory = this.GetComponentInChildren<PageFactory>();

            Questionnaires.Add(currentQuestionnaire);
            numberQuestionnaires++;

            ReadJson(inputPath);
        }

        void ReadJson(string jsonPath)
        {
            // reads and parses .json input file
            string JSONString = File.ReadAllText(jsonPath);
            var N = JSON.Parse(JSONString);

            //----------- Read metadata from .JSON file ----------//
            string title = N["qTitle"].Value;
            string instructions = N["qInstructions"].Value;
            qId = N["qId"].Value; //read questionnaire ID

            // Generates the last page
            _pageFactory.GenerateAndDisplayFirstAndLastPage(true, instructions, title);

            int i = 0;

            /*
            Continuously reads data from the .json file 
            */
            while (true)
            {
                pId = N["questions"][i]["pId"].Value; //read new page

                if (pId != "")
                {
                    string qType = N["questions"][i]["qType"].Value;
                    string qInstructions = N["questions"][i]["qInstructions"].Value;

                    _qData = N["questions"][i]["qData"].AsArray;
                    if (_qData == "")
                        _qData[0] = N["questions"][i]["qData"].Value;

                    _qConditions = N["questions"][i]["qConditions"].AsArray;
                    if (_qConditions == "")
                        _qConditions[0] = N["questions"][i]["qConditions"].Value;

                    _qOptions = N["questions"][i]["qOptions"].AsArray;
                    if (_qOptions == "")
                        _qOptions[0] = N["questions"][i]["qOptions"].Value;

                    _pageFactory.AddPage(qId, qType, qInstructions, _qData, _qConditions, _qOptions);
                    i++;
                }
                else
                {
                    // Read data for final page from .JSON file
                    string headerFinalSlide = N["qMessage"].Value;
                    string textFinalSlide = N["qAcknowledgments"].Value;

                    // Generates the last page
                    _pageFactory.GenerateAndDisplayFirstAndLastPage(false, textFinalSlide, headerFinalSlide);

                    // Initialize (Dis-/enable GameObjects)
                    _pageFactory.InitSetup();

                    break;
                }
            }
        }
    }
}