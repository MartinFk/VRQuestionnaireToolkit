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
        private AudioSource _audioSource;
        private AudioClip _hoverSoundClip;


        void Start()
        {
            GameObject _vrToolkit = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit");
            _studySetup = _vrToolkit.GetComponent<StudySetup>();
            _audioSource = _studySetup.GetComponent<AudioSource>();
            _hoverSoundClip = _studySetup.hoverSoundClip;
            AddTriggerListener();
        }

        public void Pulse(SteamVR_Input_Sources source)
        {
            hapticAction.Execute(0, _duration, _frequency, _amplitude, source);
        }

        public void PulseBothHands()
        {
            Pulse(SteamVR_Input_Sources.LeftHand);
            Pulse(SteamVR_Input_Sources.RightHand);
        }

        // Add a listener to the hovering event over the current element.  
        public void AddTriggerListener()
        {
            gameObject.AddComponent<EventTrigger>();
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entryHovering = new EventTrigger.Entry();
            entryHovering.eventID = EventTriggerType.PointerEnter;  // Adding hovering listener
            entryHovering.callback.AddListener((data) => { OnHovering(); });
            trigger.triggers.Add(entryHovering);
        }

        public void OnHovering()
        {
            if (_studySetup.ControllerTactileFeedbackOnOff) // If tactile feedback is switched on
            {
                PulseBothHands();
            }
            if (_studySetup.SoundOnOff)
            {
                _audioSource.PlayOneShot(_hoverSoundClip);
                // Debug.Log("Beep!");
            }
                
        }

    }
}