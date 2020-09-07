using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadMainMenu() 
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void LoadSinglePlayerMenu() 
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void LoadMultiplayerMenu() 
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void LoadOptionsMenu()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void ExitToDesktop() 
    {
        Application.Quit();
    }
}
