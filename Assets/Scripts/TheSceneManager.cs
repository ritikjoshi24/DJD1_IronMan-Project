using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheSceneManager : MonoBehaviour
{
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    public void LoadControls()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(sceneBuildIndex: 5);
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }

    public void LoadLoseScreen() 
    {
        SceneManager.LoadScene(sceneBuildIndex: 4);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("I quit");
    }
}
