using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using System.IO;

public class loadImg : MonoBehaviour
{
    public RawImage rawImage;
    // wczytanie obrazu instrukcji 
    void Start()
    {
        var texture = Resources.Load<Texture2D>("instrukcja/instrukcjaImg");
        rawImage.texture = texture;
    }

}
