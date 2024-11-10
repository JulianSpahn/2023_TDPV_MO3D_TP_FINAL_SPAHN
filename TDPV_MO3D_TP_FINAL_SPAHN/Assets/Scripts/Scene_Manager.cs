using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    //funciones para los botones que nos llevaran a las diferentes escenas
    public void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(2); 
    }

    public void GoToWinnerScene()
    {
        SceneManager.LoadScene(3); 
    }

    public void GoToLosserScene()
    {
        SceneManager.LoadScene(4);
    }

    public void ShowRules()
    {
        SceneManager.LoadScene(5);
    }

    public void ShowControls()
    {
        SceneManager.LoadScene(6);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0); 
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
