using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public string SceneName;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneName);
    }
}
