using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VRQuestionnaireToolkit
{
    public class LinearGrid : MonoBehaviour
    {
        public int NumRadioButtons;
        public string QId;
        public string QuestionnaireId;
        public string QType;
        public string QInstructions;
        public string QText;
        public int QMin;
        public int QMax;
        public int NumGrid;
        public bool QMandatory;

        private string _qMinLabel;
        private string _qMaxLabel;
        private Sprite _sprite;

        public GameObject LinearGridButton;
        public JSONArray QOptions;

        private RectTransform _questionRecTest;
        private bool _isOdd;
        public List<GameObject> LinearGridList; //contains all radiobuttons which correspond to one question

        //qText look how many q in one file >4 deny
        public List<GameObject> CreateLinearGridQuestion(string questionnaireId, string qType, string qInstructions, string qId, string qText, JSONArray qOptions, int numberQuestion, RectTransform questionRec, bool qMandatory, int qMin, int qMax, string qMinxLabel, string qMaxLabel)
        {
            this.QuestionnaireId = questionnaireId;
            this.QId = qId;
            this.QType = qType;
            this.QInstructions = qInstructions;
            this.QText = qText;
            this.QOptions = qOptions;
            this.NumGrid = numberQuestion;
            this._questionRecTest = questionRec;
            this.QMin = qMin;
            this.QMax = qMax;
            this._qMaxLabel = qMaxLabel;
            this._qMinLabel = qMinxLabel;
            this.QMandatory = qMandatory;

            LinearGridList = new List<GameObject>();


            // generate radio and corresponding text labels on a single page
            for (int j = 0; j < QMax; j++)
            {
                if (qOptions[j] != "")
                {
                    if (NumRadioButtons <= 20)
                        GenerateLinearGrid(numberQuestion, j); //use odd number layout

                    else
                    {
                        Debug.LogError("We currently only support up to 20 gridCells");
                    }
                }
            }
            return LinearGridList;
        }

        private void SetMiddle()
        {
            _sprite = LoadSprite();

        }

        private Sprite LoadSprite()
        {
            Sprite temp;

            string load = "stick";
            temp = Resources.Load<Sprite>(load);

            return temp;
        }

        private int CountRadioButtons(JSONArray qOptions)
        {
            int counter = 0;
            for (int i = 0; i < qOptions.Count; i++)
            {
                if (qOptions[i] != "")
                    counter++;
            }

            return counter;
        }

        void GenerateLinearGrid(int numQuestions, int numOptions)
        {
            // Instantiate radio prefabs
            GameObject temp = Instantiate(LinearGridButton);
            temp.name = "linearGrid_" + numOptions;

            // Place in hierarchy 
            RectTransform radioRec = temp.GetComponent<RectTransform>();
            radioRec.SetParent(_questionRecTest);
            radioRec.localPosition = new Vector3(-120 + (numOptions * 20), 90 - (numQuestions * 95), 0);
            radioRec.localRotation = Quaternion.identity;
            radioRec.localScale = new Vector3(radioRec.localScale.x * 0.01f, radioRec.localScale.y * 0.01f, radioRec.localScale.z * 0.01f);


            if (numOptions == 0)
            {
                temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<TextMeshProUGUI>()[0].text =
                    _qMinLabel;
                temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<TextMeshProUGUI>()[1].text =
                    _qMaxLabel;
            }
            else
            {
                temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<TextMeshProUGUI>()[0].text =
                    "";
                temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<TextMeshProUGUI>()[1].text =
                    "";
                temp.GetComponentInChildren<Toggle>().GetComponentsInChildren<Image>()[2].gameObject.SetActive(false);
            }

            // Set radiobutton group
            LinearGrid linearGridScript = temp.GetComponentInParent<LinearGrid>();
            temp.GetComponentInChildren<Toggle>().group = linearGridScript.gameObject.GetComponent<ToggleGroup>();

            LinearGridList.Add(temp);
        }
    }
}