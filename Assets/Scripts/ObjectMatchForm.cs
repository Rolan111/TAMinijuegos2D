using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class ObjectMatchForm : MonoBehaviour
{
    [SerializeField] private int matchId;
    [SerializeField] private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public int Get_ID() => matchId;


    public BoxCollider2D GetBoxCollider2D() => boxCollider;

}