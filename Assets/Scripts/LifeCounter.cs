using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public TMP_Text lifesText;

    static public int Lifes;
    
    static private int IngredientCounter;
    static private int IngredientsMax;

    static public LifeCounter instance;
    void Start()
    {
        Lifes = 3;
        IngredientsMax = 7;
        IngredientCounter = 0;

        instance = this;
    }

    private void Update()
    {
        lifesText.text="Vidas restantes: "+Lifes.ToString();

    }

    public int LostLife()
    {
        
        Lifes--;
        if (Lifes == 1)
        {
            StartCoroutine(TwinkleText());
        }

        return Lifes;
    }

    static public bool AddIngredient()
    {
        IngredientCounter++;

        return IngredientCounter >= IngredientsMax;
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
