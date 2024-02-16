using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject bowl;
    public GameObject winScreen;
    public GameObject lostScreen;
    public GameObject wrongCorrectImage;
    public bool isCorrect;

    public AudioSource source;
    public AudioClip correctClip, wrongClip, victoryClip, lostClip;

    private bool isMoving;
    private bool isFinished;

    private float startPosX;
    private float startPosY;

    private float minDistanceToAttachIngredient = 2.5f;


    private Vector3 resetPosition;

    void Start()
    {
        resetPosition = this.transform.localPosition;
    }

    void Update()
    {
        if (!isFinished)
        {
            if (isMoving)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);


                this.gameObject.transform.localPosition = new Vector3((mousePos.x - startPosX), (mousePos.y - startPosY), this.gameObject.transform.localPosition.z);
            }
        }
    }

    private void OnMouseDown()
    {
        if(LifeCounter.Lifes>0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                startPosX = mousePos.x - this.transform.localPosition.x;
                startPosY = mousePos.y - this.transform.localPosition.y;

                isMoving = true;
            }
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;
        float distanceIngredientX = Mathf.Abs(this.transform.position.x - bowl.transform.position.x);
        float distanceIngredientY = Mathf.Abs(this.transform.position.y - bowl.transform.position.y);

        if (distanceIngredientX <= minDistanceToAttachIngredient && distanceIngredientY <= minDistanceToAttachIngredient)
        {
            if(isCorrect)
            {
                wrongCorrectImage.gameObject.transform.Find("Correct").gameObject.SetActive(true);
                StartCoroutine(HideCorrectWrongIcon("Correct"));
                this.gameObject.transform.localScale = Vector3.zero;
                PlayCorrectSound();
            }
            else
            {
                this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
                wrongCorrectImage.gameObject.transform.Find("Wrong").gameObject.SetActive(true);
                StartCoroutine(HideCorrectWrongIcon("Wrong"));
                PlayWrongSound();
            }


        }

        else
        {
            this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
            //wrongCorrectImage.gameObject.transform.Find("Wrong").gameObject.SetActive(true);
            //StartCoroutine(HideCorrectWrongIcon("Wrong"));
            //PlayWrongSound();
        }
    }

    private void PlayCorrectSound()
    {
        source.clip = correctClip;
        source.Play();
    }

    private void PlayWrongSound()
    {
        source.clip = wrongClip;
        source.Play();
    }

    private IEnumerator HideCorrectWrongIcon(String name)
    {
        yield return new WaitForSeconds(1);
        wrongCorrectImage.gameObject.transform.Find(name).gameObject.SetActive(false);
        if (name == "Correct")
        {
            bool win = LifeCounter.AddIngredient();
            if (win)
            {
                winScreen.SetActive(true);
                PlayVictorySound();
            }
            this.gameObject.SetActive(false);
        }
        else
        {
            int lifes = LifeCounter.instance.LostLife();
            if (lifes <= 0)
            {
                lostScreen.SetActive(true);
                PlayLostSound();
                Debug.Log("LOST");
            }
            
        }
    }

    private void PlayLostSound()
    {
        source.clip = lostClip;
        source.Play();
    }

    private void PlayVictorySound()
    {
        source.clip = victoryClip;
        source.Play();
    }
}
