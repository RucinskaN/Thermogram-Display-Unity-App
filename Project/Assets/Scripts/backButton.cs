using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class backButton : MonoBehaviour
{
    //Obs�uga przycisku powrotu do g��wnej sceny
    public void switchScene()
    {
        SceneManager.LoadSceneAsync("Scenes/Main", LoadSceneMode.Single);
    }
}
