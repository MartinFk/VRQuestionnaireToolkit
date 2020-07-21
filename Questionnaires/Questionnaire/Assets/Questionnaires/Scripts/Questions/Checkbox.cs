using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Checkbox.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class Checkbox : MonoBehaviour
    {
        public int NumCheckboxButtons;
        public string QuestionnaireId;
        public string QId;
        public string QType;
        public string QInstructions;
        public string QText;
        public bool QMandatory;

        public GameObject CheckboxButtons;
        public JSONArray QOptions;

        private RectTransform _questionRecTest;
        public List<GameObject> CheckboxList; //contains all radiobuttons which correspond to one question

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateCheckboxQuestion(string questionnaireId, string qType, string qInstructions, string qId, string qText, JSONArray qOptions, int numberQuestion, RectTransform questionRec)
        {
            this.QuestionnaireId = questionnaireId;
            this.QId = qId;
            this.QType = qType;
            this.QInstructions = qInstructions;
            this.QText = qText;
            this.QOptions = qOptions;
            this.NumCheckboxButtons = qOptions.Count;
            this._questionRecTest = questionRec;

            CheckboxList = new List<GameObject>();

            // generate checkbox and corresponding text labels on a single page
            for (int j = 0; j < qOptions.Count; j++)
            {
                if (qOptions[j] != "")
                {
                    if (NumCheckboxButtons <= 7)
                        InitCheckBoxButtons(numberQuestion, j);
                    else
                    {
                        Debug.LogError("We currently only support up to 7 checkboxes on a single page");
                    }
                }
            }
            return CheckboxList;
        }

        void InitCheckBoxButtons(int numQuestions, int numOptions)
        {
            // Instantiate dropdown prefabs
            GameObject temp = Instantiate(CheckboxButtons);
            temp.name = "checkbox_" + numOptions;

            // Set dropdown options (Text) ;image also possible
            TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
            text.text = QOptions[numOptions];

            // Place in hierarchy 
            RectTransform checkBoxRec = temp.GetComponent<RectTransform>();
            checkBoxRec.SetParent(_questionRecTest);
            checkBoxRec.localPosition = new Vector3(-170 + (numQuestions * 140), 60 - (numOptions * 30), 0);
            checkBoxRec.localRotation = Quaternion.identity;
            checkBoxRec.localScale = new Vector3(checkBoxRec.localScale.x * 0.01f, checkBoxRec.localScale.y * 0.01f, checkBoxRec.localScale.z * 0.01f);

            CheckboxList.Add(temp);
        }
    }
}