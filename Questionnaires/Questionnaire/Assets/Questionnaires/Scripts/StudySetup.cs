using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        public bool SoundOnOff = true;

        [Header("Customize feedback parameters on hovering:")]
        [Range(0, 1)]
        public float vibratingDurationHovering = 0.05f;
        [Range(0, 200)]
        public float vibratingFrequencyHovering = 1.0f;
        [Range(0, 100)]
        public float vibratingAmplitudeHovering = 5.0f;
        [Tooltip("Choose the audio file to play upon hovering over a button.")]
        public AudioClip soundClipHovering;

        [Header("Customize feedback parameters on selecting:")]
        [Range(0, 1)]
        public float vibratingDurationSelecting = 0.05f;
        [Range(0, 200)]
        public float vibratingFrequencySelecting = 200.0f;
        [Range(0, 100)]
        public float vibratingAmplitudeSelecting = 1.0f;
        [Tooltip("Choose the audio file to play upon selecting on a button.")]
        public AudioClip soundClipSelecting;


        [Space(20)]
        [Tooltip("Remember the position, rotation and scale of the questionnaire panel after adjusting it." +
            "\n\nIf true => use the saved transform values from the last time;" +
            "\nIf false => use default transform values.")]
        public bool rememberTransformValues;
        private string _path; // file path to write the remembered transform values to

        void Start()
        {
            _path = Application.dataPath + "/Resources/saved_transform_values";

            if (rememberTransformValues & File.Exists(_path))
                SetTransformToSavedValues();
            else
                SetTransformToDefault();
        }

        void Update()
        {
            AdjustTransform();
        }

        void OnApplicationQuit()
        {
            if (rememberTransformValues)
                SaveCurrentValues();
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

            // Press UpArrow to push the panel farther away
            if (Input.GetKeyDown(KeyCode.UpArrow))
                this.transform.Translate(new Vector3(0.0f, 0.0f, 0.2f));
            // Press DownArrow to bring the panel closer
            if (Input.GetKeyDown(KeyCode.DownArrow))
                this.transform.Translate(new Vector3(0.0f, 0.0f, -0.2f));

            // Press 0 to reset transform (position, rotation & scale)
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                SetTransformToDefault();
        }

        void WriteStringToFile(string str, string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine(str);
            sw.Close();
        }

        string ReadStringFromFile(string filePath)
        {
            string str;
            StreamReader sr = new StreamReader(filePath);
            str = sr.ReadToEnd();
            sr.Close();
            return str;
        }

        // Writing the current position/rotation/scale values to a .txt file under the Resource folder.
        void SaveCurrentValues()
        {
            string thingsToWrite;
            thingsToWrite = transform.localPosition.x + "," + transform.localPosition.y + "," + transform.localPosition.z + "," +
                "\n" + transform.localRotation.x + "," + transform.localRotation.y + "," + transform.localRotation.z + "," + transform.localRotation.w + "," +
                "\n" + transform.localScale.x + "," + transform.localScale.y + "," + transform.localScale.z;
            WriteStringToFile(thingsToWrite, _path);
        }

        // Reading the saved values from the .txt file and setting the transform accordingly.
        void SetTransformToSavedValues()
        {
            string[] strings = ReadStringFromFile(_path).Split(',');
            float[] values = new float[10];
            for (int i = 0; i < strings.Length; i++)
            {
                values[i] = float.Parse(strings[i]);
            }
            this.transform.localPosition = new Vector3(values[0], values[1], values[2]);
            this.transform.localRotation = new Quaternion(values[3], values[4], values[5], values[6]);
            this.transform.localScale = new Vector3(values[7], values[8], values[9]);
        }

        void SetTransformToDefault()
        {
            this.transform.localPosition = new Vector3(0.0f, 1.5f, 6.0f);
            this.transform.localRotation = Quaternion.identity;
            this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}