using Oculus.Voice;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    [SerializeField] private AppVoiceExperience AppVoice;
    [SerializeField] private TextMeshProUGUI TranscriptText;


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
