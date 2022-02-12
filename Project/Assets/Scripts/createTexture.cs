using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

//Skrypt umieszczenia bitmapy w aplikacji
public class createTexture : MonoBehaviour
{
    public RawImage rawImage;

    public void create(Bitmap bmp)
    {
        //Zamiana bitmap -> Texture2D
        MemoryStream _ms = new MemoryStream();
        bmp.Save(_ms, ImageFormat.Png);
        var _buffer = new byte[_ms.Length];
        _ms.Position = 0;
        _ms.Read(_buffer, 0, _buffer.Length);
        Texture2D thisTexture = new Texture2D(1, 1);
        thisTexture.LoadImage(_buffer);
        int wth = thisTexture.width;
        int hgt = thisTexture.height;

        //Umieszczenie obrazu 
        rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(wth, hgt);
        rawImage.texture = thisTexture;
    }
}


