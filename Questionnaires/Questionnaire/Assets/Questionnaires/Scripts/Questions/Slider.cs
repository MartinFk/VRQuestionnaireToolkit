using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Slider.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class Slider : MonoBehaviour
    {
        public int NumSlider;
        public string QId;
        public string QuestionnaireId;
        public string QType;
        public string QInstructions;
        public string QText;
        public int QMin;
        public int QMax;
        public bool QMandatory;

        private string _qMinLabel;
        private string _qMaxLabel;
        private Sprite _sprite;

        public GameObject Sliders;
        public JSONArray QOptions;

        private RectTransform _questionRecTest; // parent rectransform
        public List<GameObject> SliderList; //contains all radiobuttons which correspond to one question

        public List<GameObject> CreateSliderQuestion(string questionnaireId, string qType, string qInstructions, string qId, string qText, JSONArray qOptions, int numberQuestion, RectTransform questionRec, int qMin, int qMax, string qMinxLabel, string qMaxLabel)
        {
            this.QuestionnaireId = questionnaireId;
            this.QId = qId;
            this.QType = qType;
            this.QInstructions = qInstructions;
            this.QText = qText;
            this.QOptions = qOptions;
            this.NumSlider = numberQuestion;
            this._questionRecTest = questionRec;
            this.QMin = qMin;
            this.QMax = qMax;
            this._qMaxLabel = qMaxLabel;
            this._qMinLabel = qMinxLabel;

            SliderList = new List<GameObject>();

            // generate sliders and corresponding labels on a single page
            if (QText != "")
            {
                if (NumSlider <= 7)
                    InitSlider(NumSlider);
                else
                {
                    Debug.LogError("We currently only support up to 7 sliders on one page");
                }
            }

            return SliderList;
        }

        void InitSlider(int numQuestions)
        {
            // Instantiate slider prefabs
            GameObject temp = Instantiate(Sliders);
            temp.name = "slider_" + numQuestions;

            // Use this for initialization
            _sprite = LoadSprite(QMax);
            temp.GetComponentInChildren<UnityEngine.UI.Slider>().GetComponent<Image>().sprite = _sprite;

            // Set required slider properties
            temp.GetComponentInChildren<UnityEngine.UI.Slider>().minValue = QMin;
            temp.GetComponentInChildren<UnityEngine.UI.Slider>().maxValue = QMax;
            temp.GetComponentInChildren<UnityEngine.UI.Slider>().GetComponentsInChildren<TextMeshProUGUI>()[0].text =
                _qMinLabel;
            temp.GetComponentInChildren<UnityEngine.UI.Slider>().GetComponentsInChildren<TextMeshProUGUI>()[1].text =
                _qMaxLabel;

            //Set Slider start value
            temp.GetComponentInChildren<UnityEngine.UI.Slider>().value = QMax % 2 == 0 ? (int)QMax / 2 : 0;

            // Place in hierarchy 
            RectTransform sliderRec = temp.GetComponent<RectTransform>();
            sliderRec.SetParent(_questionRecTest);
            sliderRec.localPosition = new Vector3(0, 90 - (numQuestions * 100), 0);
            sliderRec.localRotation = Quaternion.identity;
            sliderRec.localScale = new Vector3(sliderRec.localScale.x * 0.01f, sliderRec.localScale.y * 0.01f, sliderRec.localScale.z * 0.01f);

            SliderList.Add(temp);
        }

        private Sprite LoadSprite(int numberTicks)
        {
            Sprite temp;

            string load = "Sprites/Slider_" + (numberTicks + 1);
            temp = Resources.Load<Sprite>(load);

            return temp;
        }

    }
}