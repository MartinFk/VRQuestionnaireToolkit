using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ExampleExperiment.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class ExampleExperiment : MonoBehaviour
    {
        private GameObject _vrQuestionnaireToolkit;
        private GenerateQuestionnaire _generateQuestionnaire;
        private GameObject _exportToCSV;
        public bool RunDemo;

        // Start is called before the first frame update
        void Start()
        {
            _vrQuestionnaireToolkit = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit");
            _generateQuestionnaire = _vrQuestionnaireToolkit.GetComponentInChildren<GenerateQuestionnaire>();
        }

        void Demonstrate()
        {
            if (RunDemo)
            {
                _generateQuestionnaire.Questionnaires[0].SetActive(false);
                _generateQuestionnaire.Questionnaires[1].SetActive(true);
                RunDemo = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            Demonstrate();
        }
    }
}