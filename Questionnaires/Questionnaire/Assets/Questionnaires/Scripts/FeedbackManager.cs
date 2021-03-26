using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        // A selecting action should trigger the feedback no earlier than 1 second after a hovering feedback is finished.
        // This is to avoid the discomfort when two types of actions are triggered too close to each other.
        private float feedbackInterval = 1.0f;
        private bool flag_isBusy = false;

        void Start()
        {
            GameObject _vrToolkit = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit");
            _studySetup = _vrToolkit.GetComponent<StudySetup>();
            _audioSource = _studySetup.GetComponent<AudioSource>();
            _hoverSoundClip = _studySetup.soundClipForHovering;
            _selectSoundClip = _studySetup.soundClipForSelecting;
            AddTriggerListener();
        }

        //public void Pulse(SteamVR_Input_Sources source)
        //{
        //    //hapticAction.Execute(0, _duration, _frequency, _amplitude, source);
        //    hapticAction.Execute(0, _studySetup.duration, _studySetup.frequency, _studySetup.amplitude, source);
        //}

        public void PulseBothHands(float duration, float frequency, float amplitude)
        {
            try
            {
                hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.LeftHand);
                hapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.RightHand);
            }
            catch  // If a NullReferenceException is catched, turn off the tactile feedback.
            {
                _studySetup.ControllerTactileFeedbackOnOff = false;
                Debug.LogWarning("Please make sure your Vive controllers are correctly set up. \nTactile feedback is now switched off.");
            }
            
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
            CountdownFeedback(feedbackInterval);

            if (_studySetup.ControllerTactileFeedbackOnOff) // If tactile feedback is switched on
            {
                PulseBothHands(_studySetup.vibratingDurationForHovering, _studySetup.vibratingFrequencyForHovering, _studySetup.vibratingAmplitudeForHovering);
                // Debug.Log("Controller vibration on hovering zzz!");
            }
            if (_studySetup.SoundOnOff) // If sound feedback is switched on
            {
                _audioSource.volume = _studySetup.hoveringVolume;
                _audioSource.PlayOneShot(_hoverSoundClip);
            }
        }

        public void OnSelecting()
        {
            if (!flag_isBusy)
            {
                if (_studySetup.ControllerTactileFeedbackOnOff) // If tactile feedback is switched on
                {
                    PulseBothHands(_studySetup.vibratingDurationForSelecting, _studySetup.vibratingFrequencyForSelecting, _studySetup.vibratingAmplitudeForSelecting);
                    // Debug.Log("Controller vibration on selecting zzz!");
                }
                if (_studySetup.SoundOnOff) // If sound feedback is switched on
                {
                    _audioSource.volume = _studySetup.selectingVolume;
                    _audioSource.PlayOneShot(_selectSoundClip);
                }
            }
        }

        private async void CountdownFeedback(float _interval)
        {
            flag_isBusy = true;
            await Task.Delay((int)(_interval * 1000));
            flag_isBusy = false;
        }
    }
}