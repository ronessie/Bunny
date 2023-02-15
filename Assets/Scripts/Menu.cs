using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void ToGame(string game)
    {
        SceneManager.LoadScene(game);
    }

    public void ToSeteings(string settings)
    {
        SceneManager.LoadScene(settings);
    }

    public void Home(string menu)
    {
        SceneManager.LoadScene(menu);
    }
}
