using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArticulosScript : MonoBehaviour
{
    public bool isCorrect;
    public Sprite error, comprado;
    public AudioSource source;
    public AudioClip buyClip, victoryClip, lostClip;
    static public ArticulosScript instance;

    private bool fueComprado;


    void Start()
    {
        enabled = true;
        instance = this;
        fueComprado = false;
    }

    private void OnMouseDown()
    {
        if (!fueComprado)
        {
            ComprarArticulosScript.instance.comprarArticulo(gameObject);
            fueComprado = true;
        }
        if(ComprarArticulosScript.instance.obtenerListaArticulosComprados().Count < 5) 
        {
            gameObject.GetComponent<Image>().sprite = comprado;
            PlaySound();


        }
    }



    private void PlaySound()
    {
        source.clip = buyClip;
        source.Play();
    }



}
