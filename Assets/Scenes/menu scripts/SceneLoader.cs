using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour
{
    public int sceneBuildIndex; // The Build Index of the scene to load

    public void LoadSceneByIndex()
    {
        Debug.Log($"Loading Scene with Build Index: {sceneBuildIndex}");
        SceneManager.LoadScene(sceneBuildIndex);
    }
}