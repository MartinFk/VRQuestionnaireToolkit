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
        public string StorePath;
        public string FileName;
        public string Delimiter;
        public bool UseGlobalPath;

        private string _path;
        private List<string[]> _csvRows;
        private GameObject _pageFactory;
        private GameObject _vrQuestionnaireToolkit;
        private StudySetup _studySetup;
        private string _folderPath;
        private string _fileType;
        private string _questionnaireID;

        public enum FileTyps
        {
            Csv,
            Txt
        }

        public FileTyps Filetyp;

        public UnityEvent QuestionnaireFinishedEvent;

        // Use this for initialization
        void Start()
        {
            _vrQuestionnaireToolkit = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit");
            _studySetup = _vrQuestionnaireToolkit.GetComponent<StudySetup>();

            if (QuestionnaireFinishedEvent == null)
                QuestionnaireFinishedEvent = new UnityEvent();

            if (Filetyp == 0)
                _fileType = "csv";
            else
            {
                _fileType = "txt";
            }
        }

        public void Save()
        {
            _folderPath = UseGlobalPath ? StorePath : Application.dataPath + StorePath;

            // Create a new folder if the specified folder does not exist.
            try
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
            string[] csvTemp = new string[4];

            csvTemp[0] = "QuestionType";
            csvTemp[1] = "Question";
            csvTemp[2] = "QuestionID";
            csvTemp[3] = "Answer";
            _csvRows.Add(csvTemp);

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
                        csvTemp[0] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QType;
                        csvTemp[1] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QText;
                        csvTemp[2] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QId;
                        bool multiple = false;

                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>()
                                .CheckboxList.Count;
                            j++)
                        {
                            if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>().isOn)
                            {
                                if (multiple)
                                {
                                    _csvRows.Add(csvTemp);
                                    csvTemp = new string[4];
                                    csvTemp[0] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QType;
                                    csvTemp[1] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QText;
                                    csvTemp[2] = _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>().QId;
                                    csvTemp[3] = "" + (j + 1);

                                }
                                else
                                {
                                    csvTemp[3] = "" + (j + 1);
                                    multiple = true;
                                }
                            }
                        }
                        _csvRows.Add(csvTemp);
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

            _path = _folderPath + "questionnaireID_" + _questionnaireID + "_participantID_" + _studySetup.ParticipantId + "_condition_" + _studySetup.Condition + "_" + FileName + "." + _fileType;

            string[][] output = new string[_csvRows.Count][];

            for (int i = 0; i < output.Length; i++)
                output[i] = _csvRows[i];

            int length = output.GetLength(0);

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(Delimiter, output[index]));

            print("answers stored in path: " + _path);
            StreamWriter outStream = System.IO.File.CreateText(_path);
            outStream.WriteLine(sb);
            outStream.Close();

            QuestionnaireFinishedEvent.Invoke(); //notify 
        }
    }
}

