using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.WSA;
using Application = UnityEngine.Application;
using UnityEngine.Networking;

/// <summary>
/// ExportToCSV.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class ExportToCSV : MonoBehaviour
    {
        public string FileName;
        public string Delimiter;
        public enum FileType
        {
            Csv,
            Txt
        }
        public FileType Filetype;

        [Header("Configure if you want to save the results to local storage:")]
        [Tooltip("Save results locally on this device.")]
        public bool SaveToLocal = true;
        public string StorePath;
        public bool UseGlobalPath;

        [Header("Configure if you want to save the results to remote server:")]
        public bool SaveToServer = false;
        [Tooltip("The target URI to send the results to")]
        public string TargetURI = "http://www.example-server.com/survey-results.php";

        private List<string[]> _csvRows;
        private GameObject _pageFactory;
        private GameObject _vrQuestionnaireToolkit;
        private StudySetup _studySetup;
        private string _folderPath;
        private string _fileType;
        private string _questionnaireID;

        public UnityEvent QuestionnaireFinishedEvent;

        // Use this for initialization
        void Start()
        {
            _vrQuestionnaireToolkit = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit");
            _studySetup = _vrQuestionnaireToolkit.GetComponent<StudySetup>();

            if (QuestionnaireFinishedEvent == null)
                QuestionnaireFinishedEvent = new UnityEvent();

            if (Filetype == 0)
                _fileType = "csv";
            else
            {
                _fileType = "txt";
            }
        }

        public void Save()
        {
            _folderPath = UseGlobalPath ? StorePath : Application.dataPath + StorePath;
            
            try // Create a new folder if the specified folder does not exist.
            {
                if (!Directory.Exists(_folderPath))
                {
                    Directory.CreateDirectory(_folderPath);
                    Debug.LogWarning("Folder path does not exist! New folder created at " + _folderPath);
                }
            }
            catch (IOException ex)
            {
                Debug.Log(ex.Message);
            }

            int currentQuestionnaire = 1;

            for (int i = 0; i < _vrQuestionnaireToolkit.GetComponent<GenerateQuestionnaire>().Questionnaires.Count; i++)
            {
                if (_vrQuestionnaireToolkit.GetComponent<GenerateQuestionnaire>().Questionnaires[i].gameObject.activeSelf)
                    currentQuestionnaire = i + 1;
            }


            _pageFactory = GameObject.FindGameObjectWithTag("QuestionnaireFactory");
            _csvRows = new List<string[]>();

            // creating title rows
            string[] csvTitleRow = new string[4];
            csvTitleRow[0] = "QuestionType";
            csvTitleRow[1] = "Question";
            csvTitleRow[2] = "QuestionID";
            csvTitleRow[3] = "Answer";
            _csvRows.Add(csvTitleRow);

            string[] csvTemp = new string[4];

            // enable all GameObjects (except the first and last page) in order to read the responses
            for (int i = 1; i < _pageFactory.GetComponent<PageFactory>().NumPages - 1; i++)
                _pageFactory.GetComponent<PageFactory>().PageList[i].SetActive(true);

            // read participants' responses 
            for (int i = 0; i < _pageFactory.GetComponent<PageFactory>().QuestionList.Count; i++)
            {
                if (_pageFactory.GetComponent<PageFactory>().QuestionList[i] != null)
                {
                    csvTemp = new string[4];

                    if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>() != null)
                    {
                        _questionnaireID = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>().QuestionnaireId;
                        csvTemp[0] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>().QType;
                        csvTemp[1] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>().QText;
                        csvTemp[2] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>().QId;

                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>()
                                .RadioList.Count;
                            j++)
                        {
                            if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>().isOn)
                            {
                                if (_questionnaireID != "SSQ")
                                {
                                    csvTemp[3] = "" + (j + 1);
                                }
                                else
                                {
                                    csvTemp[3] = "" + j;
                                }
                            }
                        }
                        _csvRows.Add(csvTemp);
                    }
                    else if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>() != null)
                    {
                        _questionnaireID = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>().QuestionnaireId;
                        csvTemp[0] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>().QType;
                        csvTemp[1] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>().QText;
                        csvTemp[2] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>().QId;


                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>()
                                .LinearGridList.Count;
                            j++)
                        {
                            if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>().isOn)
                            {
                                csvTemp[3] = "" + (j + 1);
                            }
                        }
                        _csvRows.Add(csvTemp);
                    }
                    else if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>() != null)
                    {
                        _questionnaireID = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>().QuestionnaireId;
                        csvTemp[0] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>().QType;
                        csvTemp[1] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>().QConditions + "_" + _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>().QText;
                        csvTemp[2] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>().QId;

                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>()
                                .RadioList.Count;
                            j++)
                        {
                            if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>().isOn)
                            {
                                csvTemp[3] = "" + (j + 1);
                            }
                        }
                        _csvRows.Add(csvTemp);
                    }
                    else if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>() != null)
                    {
                        _questionnaireID = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QuestionnaireId;

                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>()
                                .CheckboxList.Count;
                            j++)
                        {
                            csvTemp = new string[4];
                            csvTemp[0] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QType;
                            csvTemp[1] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QText + " -" +
                                        _pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInParent<Checkbox>().QOptions[j]; // "xxxQuestionxxx? -xxxOptionxxx"
                            csvTemp[2] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QId;
                            csvTemp[3] = (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>().isOn ? ("" + 1) : ""); // 1 if checked, blank if unchecked
                            _csvRows.Add(csvTemp);
                        }
                    }
                    else if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Slider>() != null)
                    {
                        _questionnaireID = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Slider>().QuestionnaireId;
                        csvTemp[0] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Slider>().QType;
                        csvTemp[1] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Slider>().QText;
                        csvTemp[2] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Slider>().QId;


                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Slider>()
                                .SliderList.Count;
                            j++)
                        {
                            csvTemp[3] = "" + _pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<UnityEngine.UI.Slider>().value;
                        }
                        _csvRows.Add(csvTemp);
                    }
                    else if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Dropdown>() != null)
                    {
                        _questionnaireID = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Dropdown>().QuestionnaireId;
                        csvTemp[0] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Dropdown>().QType;
                        csvTemp[1] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Dropdown>().QText;
                        csvTemp[2] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Dropdown>().QId;


                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Dropdown>()
                                .DropdownList.Count;
                            j++)
                        {
                            csvTemp[3] = "" + _pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<TMP_Dropdown>().value;
                        }
                        _csvRows.Add(csvTemp);
                    }
                }
            }

            // disable all GameObjects (except the last page) 
            for (int i = 1; i < _pageFactory.GetComponent<PageFactory>().NumPages - 1; i++)
                _pageFactory.GetComponent<PageFactory>().PageList[i].SetActive(false);


            //-----Processing responses into the specified data format-----//

            string _completeFileName = "questionnaireID_" + _questionnaireID + "_participantID_" + _studySetup.ParticipantId + "_condition_" + _studySetup.Condition + "_" + FileName + "." + _fileType;
            string _completeFileName_allResults = "questionnaireID_" + _questionnaireID + "_ALL_" + FileName + "." + _fileType;
            string _path = _folderPath + _completeFileName;
            string _path_allResults = _folderPath + _completeFileName_allResults;


            string[][] output = new string[_csvRows.Count][];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = _csvRows[i];
            }

            int length = output.GetLength(0);

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(Delimiter, output[index]));

            /* WRITING RESULTS TO LOCAL STORAGE */
            if (SaveToLocal)
            {
                WriteToLocal(_path, sb);
            }

            /* SENDING RESULTS TO REMOTE SERVER */
            if (SaveToServer)
            {
                StartCoroutine(SendToServer(TargetURI, _completeFileName, sb.ToString()));
            }

            /* CONSOLIDATING RESULTS */
            if (_studySetup.AlsoConsolidateResults)
            {
                string header = "Answer_Participant_" + _studySetup.ParticipantId + "_condition_" + _studySetup.Condition;

                try
                {
                    if (!File.Exists(_path_allResults)) // if the summary sheet has not been created yet
                    {
                        StreamWriter sw = new StreamWriter(_path_allResults);
                        sw.WriteLine(csvTitleRow[0] + Delimiter + csvTitleRow[1] + Delimiter + csvTitleRow[2] + Delimiter + header);
                        for (int row = 1; row < length; row++)
                        {
                            sw.WriteLine(string.Join(Delimiter, output[row]));
                        }
                        sw.Close();
                        print("Answers consolidated in path: " + _path_allResults);
                    }
                    else
                    {
                        StringBuilder sb2 = new StringBuilder();
                        StreamReader sr = new StreamReader(_path_allResults);
                        sb2.AppendLine(sr.ReadLine() + Delimiter + header);
                        for (int row = 1; row < length; row++)
                        {
                            sb2.AppendLine(sr.ReadLine() + Delimiter + output[row][3]); // repeat the old lines and append the new answer at the end
                        }
                        sr.Close();

                        StreamWriter sw = System.IO.File.CreateText(_path_allResults);
                        sw.WriteLine(sb2);
                        sw.Close();
                        print("Answers consolidated in path: " + _path_allResults);
                    }
                }
                catch (IOException ex)
                {
                    Debug.Log(ex.Message);
                }

                if (SaveToServer)
                {
                    string allText = System.IO.File.ReadAllText(_path_allResults);
                    StartCoroutine(SendToServer(TargetURI, _completeFileName_allResults, allText));
                }
            }

            QuestionnaireFinishedEvent.Invoke(); //notify 
        }

        void WriteToLocal(string localPath, StringBuilder content)
        {
            print("Answers stored in path: " + localPath);
            StreamWriter outStream = System.IO.File.CreateText(localPath);
            outStream.WriteLine(content);
            outStream.Close();
        }

        /// <summary>
        /// Post data to a specific server location.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="filename"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        IEnumerator SendToServer(string uri, string filename, string inputData)
        {
            WWWForm form = new WWWForm();
            form.AddField("fileName", filename);
            form.AddField("inputData", inputData);

            using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
            {
                yield return www.SendWebRequest();

                if (www.isHttpError || www.isNetworkError)
                {
                    Debug.LogError(www.error + "\nPlease check the validity of the server URI.");
                }
                else
                {
                    string responseText = www.downloadHandler.text;
                    Debug.Log("Message from the server: " + responseText);
                }
            }
        }
    }
}

