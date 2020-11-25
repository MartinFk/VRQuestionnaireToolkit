using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

namespace VRQuestionnaireToolkit
{
    public class FeedbackManager : MonoBehaviour
    {
        [Range(0, 1)]
        public float _duration = 0.1f;
        [Range(0, 200)]
        public float _frequency = 1.0f;
        [Range(0, 100)]
        public float _amplitude = 5.0f;

        
        public SteamVR_Action_Vibration hapticAction;

        // To later access the boolean state of tactile/sound feedback.
        private StudySetup _studySetup;


        void Start()
        {
            _studySetup = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit").GetComponent<StudySetup>();

            AddHoveringListener();
        }

        void Update()
        {
            //// Test test
            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            //    Pulse(SteamVR_Input_Sources.LeftHand);
            //if (Input.GetKeyDown(KeyCode.RightArrow))
            //    Pulse(SteamVR_Input_Sources.RightHand);
        }

        public void Pulse(SteamVR_Input_Sources source)
        {
            if (_studySetup.ControllerTactileFeedbackOnOff) hapticAction.Execute(0, _duration, _frequency, _amplitude, source);
        }

        public void PulseLeft()
        {
            Pulse(SteamVR_Input_Sources.LeftHand);
        }

        public void PulseRight()
        {
            Pulse(SteamVR_Input_Sources.RightHand);
        }

        public void PulseBothHands()
        {
            Pulse(SteamVR_Input_Sources.LeftHand);
            Pulse(SteamVR_Input_Sources.RightHand);
        }


        // Add a listener to the hovering event over the current element.  
        public void AddHoveringListener()
        {
            gameObject.AddComponent<EventTrigger>();
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { PulseBothHands(); });
            trigger.triggers.Add(entry);
        }

    }
}