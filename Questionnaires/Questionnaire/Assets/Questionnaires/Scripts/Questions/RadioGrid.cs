using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Toggle = UnityEngine.UI.Toggle;

/// <summary>
/// RadioGrid.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class RadioGrid : MonoBehaviour
    {
        public int NumRadioButtons;
        public string QuestionnaireId;
        public string QId;
        public string QType;
        public string QInstructions;
        public string QText;
        public bool QMandatory;

        public GameObject RadioButtons;
        public JSONArray QOptions;
        public string QConditions;

        private RectTransform _questionRecTest;
        public List<GameObject> RadioList; //contains all radiobuttons which correspond to one question

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateRadioGridQuestion(string questionnaireId, string qType, string qInstructions, string qId, string qText, bool Mandatory, JSONArray qOptions, string qConditions, int numberConditions, RectTransform questionRec)
        {
            this.QuestionnaireId = questionnaireId;
            this.QId = qId;
            this.QType = qType;
            this.QInstructions = qInstructions;
            this.QText = qText;
            this.QOptions = qOptions;
            this.QConditions = qConditions;
            this.NumRadioButtons = qOptions.Count;
            this._questionRecTest = questionRec;
            this.QMandatory = Mandatory;

            RadioList = new List<GameObject>();

            // generate radioGrid and corresponding text labels on a single page
            for (int j = 0; j < qOptions.Count; j++)
            {
                if (qOptions[j] != "")
                {
                    if (NumRadioButtons <= 7)
                        InitRadioGridButtons(numberConditions, j);
                    else
                    {
                        Debug.LogError("We currently only support up to 7 options (e.g., 7 point-likert scale)");
                    }
                }
            }
            return RadioList;
        }

        void InitRadioGridButtons(int numConditions, int numOptions)
        {
            //Instantiate radioGridbuttons
            GameObject temp = Instantiate(RadioButtons);
            temp.name = "radioGrid_" + numOptions;

            // only generate one (top) row of labels
            if (numConditions == 0)
            {
                TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = QOptions[numOptions];
            }

            // Place in hierarchy 
            RectTransform radioGridRec = temp.GetComponent<RectTransform>();
            radioGridRec.SetParent(_questionRecTest);

            radioGridRec.localPosition = new Vector3(-100 + (numOptions * 70), 35 - (numConditions * 50), 0);
            radioGridRec.localRotation = Quaternion.identity;
            radioGridRec.localScale = new Vector3(radioGridRec.localScale.x * 0.01f, radioGridRec.localScale.y * 0.01f, radioGridRec.localScale.z * 0.01f);

            // Set radiobutton group
            RadioGrid radioGridScript = temp.GetComponentInParent<RadioGrid>();
            temp.GetComponentInChildren<Toggle>().group = radioGridScript.gameObject.GetComponent<ToggleGroup>();

            RadioList.Add(temp);
        }
    }
}