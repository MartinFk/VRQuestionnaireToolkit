using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// StudySetup.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// TODO implement class
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class StudySetup : MonoBehaviour
    {
        public string ParticipantId;
        public string Condition;
        [Tooltip("Switch on/off tactile feedback.")]
        public bool ControllerTactileFeedbackOnOff = true;
        [Tooltip("Switch on/off sound feedback.")]
        private bool SoundOnOff;

        void Update()
        {
            AdjustTransform(); 
        }

        // Resizing the questionnaire panel by hitting keys + and -
        void AdjustTransform()
        {
            // Press + to scale up
            if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
                this.transform.localScale = Vector3.Scale(this.transform.localScale, new Vector3(1.1f, 1.1f, 1.0f));
            // Press - to scale down
            if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
                this.transform.localScale = Vector3.Scale(this.transform.localScale, new Vector3(0.9f, 0.9f, 1.0f));
            // Press 0 to reset
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            // Press UpArrow to push the panel farther away
            if (Input.GetKeyDown(KeyCode.UpArrow))
                this.transform.Translate(new Vector3(0.0f, 0.0f, 0.2f));
            // Press DownArrow to bring the panel closer
            if (Input.GetKeyDown(KeyCode.DownArrow))
                this.transform.Translate(new Vector3(0.0f, 0.0f, -0.2f));
        }
    }
}