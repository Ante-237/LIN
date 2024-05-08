using Meta.WitAi.TTS.Utilities;
using Oculus.Voice;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    [SerializeField] private AppVoiceExperience AppVoice;
    [SerializeField] private TextMeshProUGUI TranscriptText;
    [SerializeField] private TextMeshProUGUI DebugText;

    // text to voice real time translation.
    [SerializeField] private TTSSpeaker TTSSpeaker;


    // a to open mic
    // b to for text to speech
    // show confidence
    // show what's being said by the player. 

    private void Start()
    {
        AppVoice.TranscriptionEvents.OnFullTranscription.AddListener(UserVoiceInput);
        AppVoice.TranscriptionEvents.OnPartialTranscription.AddListener(UserVoiceInputPartial);

        //AppVoice.VoiceEvents.
    }

    public void OutputText(string[] text)
    {
        foreach(string s in text)
        {
            DebugText.text = s;
        }
    }

    public void WrongOutput(string text)
    {
        DebugText.text = text;
    }

    void UserVoiceInput(string arg0)
    {
        TranscriptText.text = arg0;
    }

    void UserVoiceInputPartial(string arg0)
    {
        TranscriptText.text = arg0 ;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            AppVoice.Activate();
        }
    }


}
