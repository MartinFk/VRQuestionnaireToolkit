using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

namespace VRQuestionnaireToolkit
{
    public class FeedbackManager : MonoBehaviour
    {
        //[Range(0, 1)]
        //public float _duration = 0.05f;
        //[Range(0, 200)]
        //public float _frequency = 1.0f;
        //[Range(0, 100)]
        //public float _amplitude = 5.0f;

        
        public SteamVR_Action_Vibration hapticAction;

        // To later access the boolean state of tactile/sound feedback.
        private StudySetup _studySetup;
        private AudioSource _audioSource;
        private AudioClip _hoverSoundClip;
        private AudioClip _selectSoundClip;


        void Start()
        {
            GameObject _vrToolkit = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit");
            _studySetup = _vrToolkit.GetComponent<StudySetup>();
            _audioSource = _studySetup.GetComponent<AudioSource>();
            _hoverSoundClip = _studySetup.soundClipHovering;
            _selectSoundClip = _studySetup.soundClipSelecting;
            AddTriggerListener();
        }

        //public void Pulse(SteamVR_Input_Sources source)
        //{
        //    //hapticAction.Execute(0, _duration, _frequency, _amplitude, source);
        //    hapticAction.Execute(0, _studySetup.duration, _studySetup.frequency, _studySetup.amplitude, source);
        //}

        public void PulseBothHands(float duration, float frequency, float amplitude)
        {
            hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.LeftHand);
            hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.RightHand);
        }

        // To add listeners to the hovering event over the current element.  
        public void AddTriggerListener()
        {
            gameObject.AddComponent<EventTrigger>();
            EventTrigger trigger = GetComponent<EventTrigger>();

            // Adding hovering listener
            EventTrigger.Entry entryHovering = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            entryHovering.callback.AddListener((data) => { OnHovering(); });
            trigger.triggers.Add(entryHovering);

            // Adding selecting listener
            EventTrigger.Entry entrySelecting = new EventTrigger.Entry { eventID = EventTriggerType.Select };
            entrySelecting.callback.AddListener((data) => { OnSelecting(); });
            trigger.triggers.Add(entrySelecting);
        }

        public void OnHovering()
        {
            if (_studySetup.ControllerTactileFeedbackOnOff) // If tactile feedback is switched on
            {
                PulseBothHands(_studySetup.vibratingDurationHovering, _studySetup.vibratingFrequencyHovering, _studySetup.vibratingAmplitudeHovering);
            }
            if (_studySetup.SoundOnOff) // If sound feedback is switched on
            {
                _audioSource.PlayOneShot(_hoverSoundClip);
            }
                
        }

        public void OnSelecting()
        {
            if (_studySetup.ControllerTactileFeedbackOnOff) // If tactile feedback is switched on
            {
                PulseBothHands(_studySetup.vibratingDurationSelecting, _studySetup.vibratingFrequencySelecting, _studySetup.vibratingAmplitudeSelecting);
            }
            if (_studySetup.SoundOnOff) // If sound feedback is switched on
            {
                _audioSource.PlayOneShot(_selectSoundClip);
            }
        }

    }
}