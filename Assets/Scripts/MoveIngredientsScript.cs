using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject bowl;
    public GameObject winScreen;
    public GameObject lostScreen;
    public GameObject wrongCorrectImage;
    public bool isCorrect;

    public Sprite spriteHover;

    private Sprite spriteBase;


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
        spriteBase = GetComponent<SpriteRenderer>().sprite;
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
        if (LifeCounter.Lifes > 0 && !isFinished)
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


    private void OnMouseEnter()
    {
        if (!isFinished)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteHover;
        }
    }

    private void OnMouseExit()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteBase;
    }

    private void OnMouseUp()
    {
        if (!isFinished)
        {
            isMoving = false;
            float distanceIngredientX = Mathf.Abs(this.transform.position.x - bowl.transform.position.x);
            float distanceIngredientY = Mathf.Abs(this.transform.position.y - bowl.transform.position.y);

            if (distanceIngredientX <= minDistanceToAttachIngredient && distanceIngredientY <= minDistanceToAttachIngredient)
            {
                if (isCorrect)
                {
                    isFinished = true;
                    this.gameObject.GetComponentInChildren<TMP_Text>().enabled = false;
                    wrongCorrectImage.gameObject.transform.Find("Correct").gameObject.SetActive(true);
                    StartCoroutine(HideCorrectWrongIcon("Correct"));
                    //this.gameObject.transform.localScale = Vector3.zero;
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
        }
        else
        {
            int lifes = LifeCounter.instance.LostLife();
            if (lifes <= 0)
            {
                lostScreen.SetActive(true);
                PlayLostSound();

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
