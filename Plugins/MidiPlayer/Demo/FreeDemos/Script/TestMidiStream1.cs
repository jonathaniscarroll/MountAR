
//#define DEBUG_MULTI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using System;

namespace MidiPlayerTK
{
	using UnityEngine.Events;
	public class TestMidiStream1 : MonoBehaviour
    {

        // MPTK component able to play a stream of midi events
        // Add a MidiStreamPlayer Prefab to your game object and defined midiStreamPlayer in the inspector with this prefab.
        public MidiStreamPlayer midiStreamPlayer;

        [Range(0.05f, 10f)]
        public float Frequency = 1;

        [Range(-10f, 100f)]
        public float NoteDuration = 0;

        [Range(0f, 10f)]
        public float NoteDelay = 0;


        public bool RandomPlay = true;
        public bool DrumKit = false;
        public bool ChordPlay = false;
        public int ArpeggioPlayChord = 0;
        public int DelayPlayScale = 200;
        public bool ChordLibPlay = false;
        public bool RangeLibPlay = false;
        public int CurrentChord;

        [Range(0, 127)]
        public int StartNote = 50;

        [Range(0, 127)]
        public int EndNote = 60;

        [Range(0, 127)]
        public int Velocity = 100;

        [Range(0, 16)]
        public int StreamChannel = 0;

        //[Range(0, 127)]
	    public int CurrentNote{
	    	get;set;
	    }

        [Range(0, 127)]
        public int StartPreset = 0;

        [Range(0, 127)]
        public int EndPreset = 127;

	    [Range(0, 127)]
	    [SerializeField]
	    private int _currentPreset;
	    public int CurrentPreset{
	    	get{
	    		midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent()
	    		{
		    		Command = MPTKCommand.PatchChange,
		    		Value = _currentPreset,
		    		Channel = StreamChannel,
                });
	    		return _currentPreset;
	    	} set{
	    		_currentPreset = value;
	    		midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent()
	    		{
		    		Command = MPTKCommand.PatchChange,
		    		Value = _currentPreset,
		    		Channel = StreamChannel,
                });
	    	}
	    }

        [Range(0, 127)]
        public int CurrentPatchDrum;

        [Range(0, 127)]
        public int PanChange;

        [Range(0, 127)]
        public int ModChange;

        const float DEFAULT_PITCH = 64;
        [Range(0, 127)]
        public float PitchChange = DEFAULT_PITCH;
        private float currentVelocityPitch;
        private float LastTimePitchChange;

        [Range(0, 127)]
        public int ExpChange;

        public int CountNoteToPlay = 1;
        public int CountNoteChord = 3;
        public int DegreeChord = 1;
        public int CurrentScale = 0;

        /// <summary>
        /// Current note playing
        /// </summary>
        private MPTKEvent NotePlaying;

        private float LastTimeChange;

        /// <summary>
        /// Popup to select an instrument
        /// </summary>
        private PopupListItem PopPatchInstrument;
        private PopupListItem PopBankInstrument;
        private PopupListItem PopPatchDrum;
        private PopupListItem PopBankDrum;

        // Popup to select a realtime generator
        private PopupListItem[] PopGenerator;
        private int[] indexGenerator;
        private string[] labelGenerator;
        private float[] valueGenerator;
        private const int nbrGenerator = 4;

        // Manage skin
        public CustomStyle myStyle;

        private Vector2 scrollerWindow = Vector2.zero;
        private int buttonWidth = 250;
        private float spaceVertival = 0;
        private float widthFirstCol = 100;
        public bool IsplayingLoopNotes;
        public bool IsplayingLoopPresets;

        private void Awake()
        {
            if (midiStreamPlayer != null)
            {
                // The call of this method can also be defined in the prefab MidiStreamPlayer
                // from the Unity editor inspector. See "On Event Synth Awake". 
                // StartLoadingSynth will be called just before the initialization of the synthesizer.
                // Warning: depending on the starting order of the GameObjects, 
                //          this call may be missed if MidiStreamPlayer is started before TestMidiStream, 
                //          so it is preferable to define this event in the inspector.
                if (!midiStreamPlayer.OnEventSynthAwake.HasEvent())
                    midiStreamPlayer.OnEventSynthAwake.AddListener(StartLoadingSynth);

                // The call of this method can also be defined in the prefab MidiStreamPlayer 
                // from the Unity editor inspector. See "On Event Synth Started.
                // EndLoadingSynth will be called when the synthesizer is ready.
                if (!midiStreamPlayer.OnEventSynthStarted.HasEvent())
                    midiStreamPlayer.OnEventSynthStarted.AddListener(EndLoadingSynth);
            }
            else
                Debug.LogWarning("midiStreamPlayer is not defined. Check in Unity editor inspector of this gameComponent");
        }

        // Use this for initialization
        void Start()
	    {
		    CurrentPreset = _currentPreset;
		    midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent()
		    {
			    Command = MPTKCommand.PatchChange,
			    Value = CurrentPreset,
			    Channel = StreamChannel,
            });
            // Define popup to display to select preset and bank
            PopBankInstrument = new PopupListItem() { Title = "Select A Bank", OnSelect = BankPatchChanged, Tag = "BANK_INST", ColCount = 5, ColWidth = 150, };
            PopPatchInstrument = new PopupListItem() { Title = "Select A Patch", OnSelect = BankPatchChanged, Tag = "PATCH_INST", ColCount = 5, ColWidth = 150, };
            PopBankDrum = new PopupListItem() { Title = "Select A Bank", OnSelect = BankPatchChanged, Tag = "BANK_DRUM", ColCount = 5, ColWidth = 150, };
            PopPatchDrum = new PopupListItem() { Title = "Select A Patch", OnSelect = BankPatchChanged, Tag = "PATCH_DRUM", ColCount = 5, ColWidth = 150, };

            GenModifier.InitListGenerator();
            indexGenerator = new int[nbrGenerator];
            labelGenerator = new string[nbrGenerator];
            valueGenerator = new float[nbrGenerator];
            PopGenerator = new PopupListItem[nbrGenerator];
            for (int i = 0; i < nbrGenerator; i++)
            {
                indexGenerator[i] = GenModifier.RealTimeGenerator[0].Index;
                labelGenerator[i] = GenModifier.RealTimeGenerator[0].Label;
                if (indexGenerator[i] >= 0)
                    valueGenerator[i] = GenModifier.DefaultNormalizedVal((fluid_gen_type)indexGenerator[i]) * 100f;
                PopGenerator[i] = new PopupListItem() { Title = "Select A Generator", OnSelect = GeneratorChanged, Tag = i, ColCount = 3, ColWidth = 250, };
            }
            LastTimeChange = Time.realtimeSinceStartup;
            CurrentNote = StartNote;
            PanChange = 64;
            LastTimeChange = -9999999f;
            PitchChange = DEFAULT_PITCH;
            CountNoteToPlay = 1;
        }

        /// <summary>
        /// The call of this method is defined in MidiPlayerGlobal (it's a son of the prefab MidiStreamPlayer) from the Unity editor inspector. 
        /// The method is called when SoundFont is loaded. We use it only to statistics purpose.
        /// </summary>
        public void EndLoadingSF()
        {
            Debug.Log("End loading SoundFont. Statistics: ");

            Debug.Log("List of presets available");
            foreach (MPTKListItem preset in MidiPlayerGlobal.MPTK_ListPreset)
                Debug.Log($"   [{preset.Index,3:000}] - {preset.Label}");

            Debug.Log("   Time To Load SoundFont: " + Math.Round(MidiPlayerGlobal.MPTK_TimeToLoadSoundFont.TotalSeconds, 3).ToString() + " second");
            Debug.Log("   Time To Load Samples: " + Math.Round(MidiPlayerGlobal.MPTK_TimeToLoadWave.TotalSeconds, 3).ToString() + " second");
            Debug.Log("   Presets Loaded: " + MidiPlayerGlobal.MPTK_CountPresetLoaded);
            Debug.Log("   Samples Loaded: " + MidiPlayerGlobal.MPTK_CountWaveLoaded);

        }

        public void StartLoadingSynth(string name)
        {
            Debug.LogFormat($"Start loading Synth {name}");
        }

        /// <summary>
        /// This methods is run when the synthesizer is ready if you defined OnEventSynthStarted or set event from Inspector in Unity.
        /// It's a good place to set some synth parameter's as defined preset by channel 
        /// </summary>
        /// <param name="name"></param>
        public void EndLoadingSynth(string name)
        {
            Debug.LogFormat($"Synth {name} is loaded");

            // Set piano (preset 0) to channel 0. Could be different for another SoundFont.
            midiStreamPlayer.MPTK_ChannelPresetChange(0, 0);
            Debug.LogFormat($"Preset {midiStreamPlayer.MPTK_ChannelPresetGetName(0)} defined on channel 0");

            // Set reed organ (preset 20) to channel 1. Could be different for another SoundFont.
            midiStreamPlayer.MPTK_ChannelPresetChange(1, 20);
            Debug.LogFormat($"Preset {midiStreamPlayer.MPTK_ChannelPresetGetName(1)} defined on channel 1");
        }

        public bool testLocalchange = false;
        private void BankPatchChanged(object tag, int index, int indexList)
        {
            switch ((string)tag)
            {
                case "BANK_INST":
                    MidiPlayerGlobal.MPTK_SelectBankInstrument(index);
                    midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent() { Command = MPTKCommand.ControlChange, Controller = MPTKController.BankSelectMsb, Value = index, Channel = StreamChannel, });
                    break;

                case "PATCH_INST":
                    CurrentPreset = index;
                    if (testLocalchange)
                        midiStreamPlayer.MPTK_ChannelPresetChange(StreamChannel, index);
                    else
                        midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent() { Command = MPTKCommand.PatchChange, Value = index, Channel = StreamChannel, });
                    break;

                case "BANK_DRUM":
                    MidiPlayerGlobal.MPTK_SelectBankDrum(index);
                    midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent() { Command = MPTKCommand.ControlChange, Controller = MPTKController.BankSelectMsb, Value = index, Channel = 9, });
                    break;

                case "PATCH_DRUM":
                    CurrentPatchDrum = index;
                    if (testLocalchange)
                        midiStreamPlayer.MPTK_ChannelPresetChange(9, index);
                    else
                        midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent() { Command = MPTKCommand.PatchChange, Value = index, Channel = 9 });
                    break;
            }
        }

        private void GeneratorChanged(object tag, int index, int indexList)
        {
            int iGenerator = Convert.ToInt32(tag);
            indexGenerator[iGenerator] = index;
            labelGenerator[iGenerator] = GenModifier.RealTimeGenerator[indexList].Label;
            valueGenerator[iGenerator] = GenModifier.DefaultNormalizedVal((fluid_gen_type)indexGenerator[iGenerator]) * 100f;
            Debug.Log($"indexList:{indexList} indexGenerator:{indexGenerator[iGenerator]} valueGenerator:{valueGenerator[iGenerator]} {labelGenerator[iGenerator]}");
        }


        // Update is called once per frame
        void Update()
        {
            // Checj that SoundFont is loaded and add a little wait (0.5 s by default) because Unity AudioSource need some time to be started
            if (!MidiPlayerGlobal.MPTK_IsReady())
                return;

            if (PitchChange != DEFAULT_PITCH)
            {
                // If user change the pitch, wait 1/2 second before return to median value
                if (Time.realtimeSinceStartup - LastTimePitchChange > 0.5f)
                {
                    PitchChange = Mathf.SmoothDamp(PitchChange, DEFAULT_PITCH, ref currentVelocityPitch, 0.5f, 100, Time.unscaledDeltaTime);
                    if (Mathf.Abs(PitchChange - DEFAULT_PITCH) < 0.1f)
                        PitchChange = DEFAULT_PITCH;
                    //PitchChange = Mathf.Lerp(PitchChange, DEFAULT_PITCH, Time.deltaTime*10f);
                    //Debug.Log("DEFAULT_PITCH " + DEFAULT_PITCH + " " + PitchChange + " " + currentVelocityPitch);
                    midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent() { Command = MPTKCommand.PitchWheelChange, Value = (int)PitchChange << 7, Channel = StreamChannel });
                }
            }

            if (midiStreamPlayer != null && (IsplayingLoopPresets || IsplayingLoopNotes))
            {
                float time = Time.realtimeSinceStartup - LastTimeChange;
                if (time > Frequency)
                {
                    // It's time to generate some notes ;-)
                    LastTimeChange = Time.realtimeSinceStartup;


                    for (int indexNote = 0; indexNote < CountNoteToPlay; indexNote++)
                    {
                        if (IsplayingLoopPresets)
                        {
                            if (++CurrentPreset > EndPreset) CurrentPreset = StartPreset;
                            if (CurrentPreset < StartPreset) CurrentPreset = StartPreset;

                            midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent()
                            {
                                Command = MPTKCommand.PatchChange,
                                Value = CurrentPreset,
                                Channel = StreamChannel,
                            });
	                        IsplayingLoopPresets = false;
                        }

                        if (IsplayingLoopNotes)
                        {
                        	
	                       
	                        
                            //if (CurrentNote < StartNote) CurrentNote = StartNote;
                        }

                        // Play note or chrod or scale without stopping the current (useful for performance test)
                        Play(false);

                    }
                }
            }
        }

        /// <summary>
        /// Play music depending the parameters set
        /// </summary>
        /// <param name="stopCurrent">stop current note playing</param>
	    public void Play(bool stopCurrent)
        {
            if (RandomPlay)
            {
                if (StartNote >= EndNote)
                    CurrentNote = StartNote;
                else
                    CurrentNote = UnityEngine.Random.Range(StartNote, EndNote);
            }

            
                if (stopCurrent)
                    StopOneNote();
                PlayOneNote();
            
        }



        private void PlayScale(){}
        private void PlayOneChord(){}
        private void PlayOneChordFromLib(){}
        private void StopChord(){}

        //! [Example MPTK_PlayEvent]
        /// <summary>
        /// Send the note to the player. Notes are plays in a thread, so call returns immediately.
        /// The note is stopped automatically after the Duration defined.
        /// </summary>
        /// @snippet TestMidiStream.cs Example MPTK_PlayEvent
        private void PlayOneNote()
        {
            //Debug.Log($"{StreamChannel} {midiStreamPlayer.MPTK_ChannelPresetGetName(StreamChannel)}");
            // Start playing a new note
            NotePlaying = new MPTKEvent()
            {
                Command = MPTKCommand.NoteOn,
                Value = CurrentNote, // note to played, ex 60=C5. Use the method from class HelperNoteLabel to convert to string
                Channel = StreamChannel,
                Duration = Convert.ToInt64(NoteDuration * 1000f), // millisecond, -1 to play indefinitely
                Velocity = Velocity, // Sound can vary depending on the velocity
                Delay = Convert.ToInt64(NoteDelay * 1000f),
            };


            midiStreamPlayer.MPTK_PlayEvent(NotePlaying);
        }
        //! [Example MPTK_PlayEvent]

        private void StopOneNote()
        {
            if (NotePlaying != null)
            {
                //Debug.Log("Stop note");
                // Stop the note (method to simulate a real human on a keyboard : 
                // duration is not known when note is triggered)
                midiStreamPlayer.MPTK_StopEvent(NotePlaying);
                NotePlaying = null;
            }
        }
    }
}