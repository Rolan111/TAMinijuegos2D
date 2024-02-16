using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public TMP_Text lifesText;

    static private int Lifes;
    static private int IngredientCounter;
    static private int IngredientsMax;
    // Start is called before the first frame update
    void Start()
    {
        Lifes = 3;
        IngredientsMax = 7;
        IngredientCounter = 0;
    }

    private void Update()
    {
        lifesText.text="Vidas restantes: "+Lifes.ToString();

    }

    static public int LostLife()
    {
        
        Lifes--;

        return Lifes;
    }

    static public bool AddIngredient()
    {
        IngredientCounter++;

        return IngredientCounter >= IngredientsMax;
    }
}
