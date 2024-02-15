using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public List<GameObject> answersObjects;
    public TMP_Text questionText;
    public GameObject correctScreen, wrongScreen, winScreen;
    public AudioSource source;
    public AudioClip correctClip, wrongClip, victoryClip;

    Entities entities;

    private List<Entities.Question> questionsList = new List<Entities.Question>();

    private int counter = 0;

    void Start()
    {

        entities = gameObject.GetComponent<Entities>();
        questionsList = entities.CreateQuestionList();
        LoadQuestion();
        LoadAnswers();
    }

    void Update()
    {
    }

    private void LoadQuestion()
    {
        questionText.text = questionsList[counter].question;
    }

    public delegate void AnswerValidationDelegate(BaseEventData eventData, bool isCorrect);


    private void LoadAnswers()
    {
        for (int i = 0; i < answersObjects.Count; i++)
        {
            answersObjects[i].GetComponentInChildren<TMP_Text>().text = questionsList[counter].answers[i].answer;
            bool isCorrect = questionsList[counter].answers[i].isCorrect;
            GameObject answer = answersObjects[i]; 

            EventTrigger eventTrigger = answersObjects[i].gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry clickEvent = new EventTrigger.Entry()
            {
                eventID = EventTriggerType.PointerClick
            };
            clickEvent.callback.AddListener((eventData) => ValidateAnswer(eventData, isCorrect, answer));

            eventTrigger.triggers.Add(clickEvent);

        }
    }


    private void ValidateAnswer(BaseEventData eventData, bool isCorrect, GameObject gameObject)
    {
        if (isCorrect)
        {
            gameObject.GetComponentInChildren<Image>().color = Color.green;
            correctScreen.SetActive(true);
        }
        else
        {
            gameObject.GetComponentInChildren<Image>().color = Color.red;
            wrongScreen.SetActive(true);
        }

        StartCoroutine(ShowCorrectIncorrectImage(isCorrect, gameObject));

    }

    private IEnumerator ShowCorrectIncorrectImage(bool isCorrect, GameObject gameObject)
    {

        PlaySound(isCorrect);
        yield return new WaitForSeconds(1);
        gameObject.GetComponentInChildren <Image>().color = Color.white;

        if (isCorrect)
        {
            correctScreen.SetActive(false);
            counter++;

            if (counter == questionsList.Count)
            {
                gameObject.transform.Find("Question").gameObject.SetActive(false);
                gameObject.transform.Find("Answers").gameObject.SetActive(false);
                winScreen.gameObject.SetActive(true);
                PlayVictorySound();
            }
            else
            {
                
                RemoveComponents();
                LoadQuestion();
                LoadAnswers();
            }


        }
        else
        {
            wrongScreen.SetActive(false);
        }

    }


    private void RemoveComponents()
    {
        for (int i = 0; i < answersObjects.Count; i++)
        {

            EventTrigger eventTrigger = answersObjects[i].gameObject.GetComponent<EventTrigger>();
            Destroy(eventTrigger);

        }
    }

    private void PlaySound(bool isCorrect)
    {
        if (isCorrect)
        {
            source.clip = correctClip;
        }
        else
        {
            source.clip = wrongClip;
        }
        source.Play();
    }

    private void PlayVictorySound()
    {
        source.clip = victoryClip;
        source.Play();
    }

}
