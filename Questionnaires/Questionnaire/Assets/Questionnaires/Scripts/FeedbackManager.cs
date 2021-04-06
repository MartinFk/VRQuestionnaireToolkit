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
        public SteamVR_Action_Vibration HapticAction;

        // To later access the boolean state of tactile/sound feedback.
        private StudySetup _studySetup;
        private AudioSource _audioSource;
        private AudioClip _hoverSoundClip;
        private AudioClip _selectSoundClip;

        // A selecting action should trigger the feedback no earlier than 1 second after a hovering feedback is finished.
        // This is to avoid the discomfort when two types of actions are triggered too close to each other.
        private float _feedbackBreak = 1.0f; // one second
        private bool _flagIsBusy = false;

        void Start()
        {
            GameObject _vrToolkit = GameObject.FindGameObjectWithTag("VRQuestionnaireToolkit");
            _studySetup = _vrToolkit.GetComponent<StudySetup>();
            _audioSource = _studySetup.GetComponent<AudioSource>();
            _hoverSoundClip = _studySetup.SoundClipForHovering;
            _selectSoundClip = _studySetup.SoundClipForSelecting;
            AddTriggerListener();
        }

        public void PulseBothHands(float duration, float frequency, float amplitude)
        {
            try
            {
                HapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.LeftHand);
                HapticAction.Execute(0, duration, frequency, amplitude, SteamVR_Input_Sources.RightHand);
            }
            catch  // If a NullReferenceException is catched, turn off the tactile feedback.
            {
                _studySetup.ControllerTactileFeedbackOnOff = false;
                Debug.LogWarning("Please make sure your Vive controllers are correctly set up. \nTactile feedback is now switched off.");
            }
            
        }

        /// <summary>
        /// Add listeners to the hovering/selecting over the current element. 
        /// </summary>
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
            CountdownFeedback(_feedbackBreak);

            if (_studySetup.ControllerTactileFeedbackOnOff) // If tactile feedback is switched on
            {
                PulseBothHands(_studySetup.VibratingDurationForHovering, _studySetup.VibratingFrequencyForHovering, _studySetup.VibratingAmplitudeForHovering);
                // Debug.Log("Controller vibration on hovering zzz!");
            }
            if (_studySetup.SoundOnOff) // If sound feedback is switched on
            {
                _audioSource.volume = _studySetup.HoveringVolume;
                _audioSource.PlayOneShot(_hoverSoundClip);
            }
        }

        public void OnSelecting()
        {
            if (!_flagIsBusy)
            {
                if (_studySetup.ControllerTactileFeedbackOnOff) // If tactile feedback is switched on
                {
                    PulseBothHands(_studySetup.VibratingDurationForSelecting, _studySetup.VibratingFrequencyForSelecting, _studySetup.VibratingAmplitudeForSelecting);
                    // Debug.Log("Controller vibration on selecting zzz!");
                }
                if (_studySetup.SoundOnOff) // If sound feedback is switched on
                {
                    _audioSource.volume = _studySetup.SelectingVolume;
                    _audioSource.PlayOneShot(_selectSoundClip);
                }
            }
        }

        /// <summary>
        /// Toggle the flag that indicates whether break between feedbacks is finished.
        /// </summary>
        /// <param name="_interval"></param>
        private async void CountdownFeedback(float _interval)
        {
            _flagIsBusy = true;
            await Task.Delay((int)(_interval * 1000));
            _flagIsBusy = false;
        }
    }
}