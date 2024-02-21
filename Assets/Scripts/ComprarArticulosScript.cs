using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class ComprarArticulosScript : MonoBehaviour
{
    public TMP_Text coinText;
    public AudioSource source;
    public GameObject winScreen, lostScreen;
    public AudioClip victoryClip, lostClip;


    private int coinsRemaining;
    private List<GameObject> articulosComprados;

    static public ComprarArticulosScript instance;

    private int respuestasCorrectas;

    void Start()
    {
        coinsRemaining = 5;
        respuestasCorrectas = 0;
        instance = this;
        articulosComprados = new List<GameObject>();
    }

    void Update()
    {

    }

    public void comprarArticulo(GameObject articulo)
    {
        if (coinsRemaining > 0)
        {
            articulosComprados.Add(articulo);
            coinsRemaining--;
            coinText.text = coinsRemaining.ToString();

            if (coinsRemaining == 0)
            {
                articulosComprados[articulosComprados.Count - 1].GetComponent<Image>().sprite = articulosComprados[articulosComprados.Count - 1].GetComponent<ArticulosScript>().comprado;
                ValidarRespuestas();
            }
        }
    }

    private void ValidarRespuestas()
    {
        foreach (var articulo in articulosComprados)
        {
            if (!articulo.GetComponent<ArticulosScript>().isCorrect)
            {
                articulo.GetComponent<Image>().sprite = articulo.GetComponent<ArticulosScript>().error;
            }
            else
            {
                respuestasCorrectas++;
            }
        }

        if (respuestasCorrectas == 5)
        {
            winScreen.SetActive(true);
            PlaySound(victoryClip);
        }
        else
        {
            lostScreen.SetActive(true);
            PlaySound(lostClip);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public List<GameObject> obtenerListaArticulosComprados() => articulosComprados;
}
