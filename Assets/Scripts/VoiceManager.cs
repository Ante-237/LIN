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
    [SerializeField] private TextMeshProUGUI HintText;
    [SerializeField, Range(0, 10f)] private float StartTime = 3.0f;
    [SerializeField] private List<GameObject> ObjectList = new List<GameObject>();
    [SerializeField] private List<string> hintString = new List<string>();
    [SerializeField] private Transform SpawnPoint;



    private string RepeatText = "What do you see?";


    // text to voice real time translation.
    [SerializeField] private TTSSpeaker TTSSpeaker;


    // a to open mic
    // b to for text to speech
    // show confidence
    // show what's being said by the player. 

    private int currentIndex = 0;
    private GameObject currentObject;

    private void Start()
    {
        AppVoice.TranscriptionEvents.OnFullTranscription.AddListener(UserVoiceInput);
        //AppVoice.VoiceEvents.
        StartCoroutine(InitCallBack());
        SpawnObject();
    }

    private void SpawnObject()
    {
       currentObject =  Instantiate(ObjectList[currentIndex], SpawnPoint.position, Quaternion.identity);
    }

    IEnumerator NextLevelSequence()
    {
        yield return new WaitForSeconds(StartTime);
        if(currentIndex < ObjectList.Count)
        {
            currentIndex++;
            if(currentObject != null)
            {
                Destroy(currentObject);
            }
            SpawnObject();
        }
    }



    public void ResponseCheck(string[] arg)
    {
       foreach(string s in arg)
        {
            if(s == "object_plunger" )
            {
                if( currentIndex == 0)
                {
                    StartCoroutine(NextLevelSequence());
                }
            }

            else if(s == "object_camera")
            {
                if (currentIndex == 1)
                {
                    StartCoroutine(NextLevelSequence());
                }
            }

            else if(s == "object_streetrat")
            {
                if (currentIndex == 2)
                {
                    StartCoroutine(NextLevelSequence());
                }
            }

            else if(s == "woodenaxe")
            {
                if (currentIndex == 3)
                {
                    StartCoroutine(NextLevelSequence());
                }
            }
            else
            {
                // surely wrong output.
                WrongResponseText();
                TranscriptText.text = "<color=red>wrong response</color>";
            }
        }
    }

    IEnumerator InitCallBack()
    {
        yield return new WaitForSeconds(StartTime);
        ReapeatText();
    }

    private void ReapeatText()
    {
        TTSSpeaker.SpeakQueued(RepeatText);
    }

    private void WrongResponseText()
    {
        TTSSpeaker.SpeakQueued("Wrong response");
    }

    void UserVoiceInput(string arg0)
    {
        TranscriptText.text = arg0;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            AppVoice.Activate();
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            // show hint or update current hint.
            HintText.text = hintString[currentIndex];
        }
    }
}
