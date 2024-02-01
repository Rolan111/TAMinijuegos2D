using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinControllerScript : MonoBehaviour
{
    public GameObject planets;

    private int pointsToWin;
    private int currentPoints;

    void Start()
    {
        pointsToWin = planets.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPoints>=pointsToWin) 
        {
            
        }
    }


    public void AddPoints()
    {
        currentPoints++;
    }

    public bool ValidateWin() => currentPoints >= pointsToWin;

    public int GetCurrentPoints()=> currentPoints;
    public int GetPointsToWin() => pointsToWin;
}
