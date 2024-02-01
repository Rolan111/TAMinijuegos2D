using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject correctForm;
    public GameObject winScreen;
    public GameObject wrongCorrectImage;

    public AudioSource source;
    public AudioClip correctClip, wrongClip, victoryClip;

    private bool isMoving;
    private bool isFinished;

    private float startPosX;
    private float startPosY;

    private float minDistanceToAttachPlanet = 0.5f;

    private WinControllerScript WinController;


    private Vector3 resetPosition;

    void Start()
    {
        resetPosition = this.transform.localPosition;
        WinController = GameObject.Find("WinController").GetComponent<WinControllerScript>();
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

                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            }
        }
    }

    private void OnMouseDown()
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

    private void OnMouseUp()
    {
        isMoving = false;
        float distanceSiluetPlanetX = Mathf.Abs(this.transform.position.x - correctForm.transform.position.x);
        float distanceSiluetPlanetY = Mathf.Abs(this.transform.position.y - correctForm.transform.position.y);


        if (distanceSiluetPlanetX <= minDistanceToAttachPlanet && distanceSiluetPlanetY <= minDistanceToAttachPlanet)
        {
            this.transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
            isFinished = true;
            WinController.AddPoints();

            wrongCorrectImage.gameObject.transform.Find("Correct").gameObject.SetActive(true);
            StartCoroutine(HideCorrectWrongIcon("Correct"));

            if (WinController.ValidateWin())
            {
                PlayVictorySound();
                winScreen.SetActive(true);
            }
            else
            {
                PlayCorrectSound();
            }

        }

        else
        {
            this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
            wrongCorrectImage.gameObject.transform.Find("Wrong").gameObject.SetActive(true);
            StartCoroutine(HideCorrectWrongIcon("Wrong"));
            PlayWrongSound();
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
    }

    private void PlayVictorySound()
    {
        source.clip = victoryClip;
        source.Play();
    }
}
