using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    /// <summary>
    /// exit the game
    /// </summary>
    public void quit()
    {
        Application.Quit();
    }
    
    /// <summary>
    /// load the scene with the name sceneName
    /// </summary>
    /// <param name="sceneName"> level name to load</param>
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    /// <summary>
    /// load the scene with the index sceneIndex
    /// </summary>
    /// <param name="sceneIndex">level index to load</param>
    public void loadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    
    
}

