using Meta.WitAi.TTS.Utilities;
using Oculus.Interaction;
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
    [SerializeField] private GameObject CorrectPrefab;
    [SerializeField] private GameObject WrongPrefab;



    private string RepeatText = "What do you see?";
    private string wrongText = "Wrong response, try again.";


    // text to voice real time translation.
    [SerializeField] private TTSSpeaker TTSSpeaker;
    [SerializeField] private TTSSpeaker WrongTextSpeaker;


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
        AppVoice.Deactivate();
        StartCoroutine(PopUpState(true));
        yield return new WaitForSeconds(StartTime);
        ReapeatText();
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

    bool responseState = false;


    public void ResponseCheck(string[] arg)
    {
       foreach(string s in arg)
        {
            if(s == "object_plunger" )
            {
                if( currentIndex == 0)
                {
                    StartCoroutine(NextLevelSequence());
                    break;
                }
            }
            else if(s == "object_camera")
            {
                if (currentIndex == 1)
                {
                    StartCoroutine(NextLevelSequence());
                    break;
                }
            }
            else if(s == "object_streetrat")
            {
                if (currentIndex == 2)
                {
                    StartCoroutine(NextLevelSequence());
                    break;
                }
            }
            else if(s == "woodenaxe")
            {
                if (currentIndex == 3)
                {
                    StartCoroutine(NextLevelSequence());
                    break;
                }
            }
            else
            {
                // surely wrong output.
                responseState = true;
            
            }
        }

     
            // WrongResponseText();
            // TranscriptText.text = "<color=red>wrong response</color>";
            // responseState = false;
            // StartCoroutine(PopUpState(false));
    }

    IEnumerator PopUpState(bool state)
    {
        CorrectPrefab.SetActive(state);
        WrongPrefab.SetActive(!state);
        yield return new WaitForSeconds(2.0f);
        CorrectPrefab.SetActive(false);
        WrongPrefab.SetActive(false) ; 
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
        WrongTextSpeaker.SpeakQueued(wrongText);
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
            TranscriptText.text = "Speak";
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            // show hint or update current hint.
            HintText.text = hintString[currentIndex];
        }
    }
}
