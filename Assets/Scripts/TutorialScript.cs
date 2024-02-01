using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TutorialText;
    void Start()
    {
        StartCoroutine(ShowTutorial());
    }

    private IEnumerator ShowTutorial()
    {
        yield return new WaitForSeconds(5);

        Destroy(TutorialText);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
