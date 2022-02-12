using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class backButton : MonoBehaviour
{
    //Obs³uga przycisku powrotu do g³ównej sceny
    public void switchScene()
    {
        SceneManager.LoadSceneAsync("Scenes/Main", LoadSceneMode.Single);
    }
}
