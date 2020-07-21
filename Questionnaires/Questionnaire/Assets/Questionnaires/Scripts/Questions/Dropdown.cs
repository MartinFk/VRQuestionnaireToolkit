using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;

/// <summary>
/// Dropdown.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class Dropdown : MonoBehaviour
    {
        public int NumDropDown;
        public string QuestionnaireId;
        public string QId;
        public string QType;
        public string QInstructions;
        public string QText;
        public bool QMandatory;

        public GameObject Dropbdown;
        public JSONArray QOptions;

        private RectTransform _questionRecTest;
        public List<GameObject> DropdownList; //contains all radiobuttons which correspond to one question

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateDropdownQuestion(string questionnaireId, string qType, string qInstructions, string qId, string qText, JSONArray qOptions, int numberQuestion, RectTransform questionRec)
        {
            this.QuestionnaireId = questionnaireId;
            this.QId = qId;
            this.QType = qType;
            this.QInstructions = qInstructions;
            this.QText = qText;
            this.QOptions = qOptions;
            this.NumDropDown = numberQuestion;
            this._questionRecTest = questionRec;

            DropdownList = new List<GameObject>();

            // generate dropdowns and corresponding text labels on a single page
            if (QText != "")
            {
                if (NumDropDown <= 7)
                    InitDropdown(NumDropDown);
                else
                {
                    Debug.LogError("We currently only support up to 7 dropdown questions on a single page");
                }
            }

            return DropdownList;
        }

        void InitDropdown(int numQuestions)
        {
            // Instantiate dropdown prefabs
            GameObject temp = Instantiate(Dropbdown);
            temp.name = "dropdown" + numQuestions;

            // Set dropdown options (Text) ;image also possible
            for (int i = 0; i < QOptions.Count; i++)
                temp.GetComponentInChildren<TMP_Dropdown>().options[i].text = QOptions[i].Value;

            // Place in hierarchy 
            RectTransform dropbDownRec = temp.GetComponent<RectTransform>();
            dropbDownRec.SetParent(_questionRecTest);
            dropbDownRec.localPosition = new Vector3(0, 80 - (numQuestions * 90), 0);
            // dropbDownRec.localRotation = Quaternion.identity;
            dropbDownRec.localScale = new Vector3(dropbDownRec.localScale.x * 0.01f, dropbDownRec.localScale.y * 0.01f, dropbDownRec.localScale.z * 0.01f);

            DropdownList.Add(temp);
        }
    }
}