using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadSceneByName(sbyte name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
}
