using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class infoButton : MonoBehaviour
{
    //Funkcja wczytania sceny instrukcji
    public void switchScene()
    {
        SceneManager.LoadSceneAsync("Scenes/Info", LoadSceneMode.Single);
        
    }

}
