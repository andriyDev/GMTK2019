﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayPressed()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
