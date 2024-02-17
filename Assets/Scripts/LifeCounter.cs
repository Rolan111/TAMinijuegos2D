using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public TMP_Text lifesText;

    static public int Lifes;

    static private int CardsCounter;
    static private int CardsMax;

    static public LifeCounter instance;
    void Start()
    {
        Lifes = 3;
        CardsMax = 6;
        CardsCounter = 0;

        instance = this;
    }

    private void Update()
    {
        lifesText.text = "Vidas restantes: " + Lifes.ToString();

    }

    public void LostLife()
    {

        Lifes--;
        if (Lifes == 1)
        {
            StartCoroutine(TwinkleText());
        }

    }

    public int GetLifesRemaining()
    {
        return Lifes;
    }

    public void Match()
    {
        CardsCounter += 1;
    }

    public bool CheckWin()
    {
        Debug.Log(CardsCounter);
        return CardsCounter >= CardsMax;
    }

    private IEnumerator TwinkleText()
    {
        while (true)
        {
            lifesText.enabled = !lifesText.enabled;
            yield return new WaitForSeconds(0.3f);
        }

    }

    public void StartBlinkText()
    {
        StartCoroutine(TwinkleText());
    }
}
