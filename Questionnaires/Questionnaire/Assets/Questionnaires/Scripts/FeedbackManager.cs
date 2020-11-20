using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace VRQuestionnaireToolkit
{
    public class FeedbackManager : MonoBehaviour
    {
        [Tooltip("Switch on/off haptic feedback.")]
        public bool hapticFeedback;
        [Tooltip("Switch on/off sound feedback.")]
        public bool soundFeedback;

        [Range(0, 1)]
        public float _duartion;
        [Range(0, 200)]
        public float _frequency;
        [Range(0, 100)]
        public float _amplitude;

        [Space(20)]
        public SteamVR_Action_Vibration hapticAction;
        public SteamVR_Action_Boolean trackpadAction;



        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Pulse(_duartion, _frequency, _amplitude, SteamVR_Input_Sources.LeftHand);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Pulse(_duartion, _frequency, _amplitude, SteamVR_Input_Sources.LeftHand);
            }
        }

        void Pulse (float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
        {
            hapticAction.Execute(0, duration, frequency, amplitude, source);
        }
    }
}