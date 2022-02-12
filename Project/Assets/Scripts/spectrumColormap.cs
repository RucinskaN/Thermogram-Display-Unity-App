using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Drawing; 
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


//Skrypt na³o¿enia mapy kolorów spectrum
public class spectrumColormap : MonoBehaviour
{
    readSeq readS;
    createTexture createTex;
    
    public void setMap()
    {
        //Importowanie danych aktualnie wyœwietlanego termogramu
        readS = GameObject.FindGameObjectWithTag("TagSeq").GetComponent<readSeq>();
        int[,] macierzGrey;
        macierzGrey = readS.macierzGreyInt;
        Bitmap bmpSpectrum = new Bitmap(640,480);
        getSpectrum(macierzGrey, bmpSpectrum);
        int flagBuff = 2;
        readS.flag = flagBuff;

        //Wywo³anie skryptu zmieniaj¹cego bitmapê w texturê 
        createTex = GameObject.FindGameObjectWithTag("TagTex").GetComponent<createTexture>();
        createTex.create(bmpSpectrum);
    }
    void getSpectrum(int[,] macierz, Bitmap bmpSpectrum)
    {
        //Definicja mapy kolorów
        int t = 0;
        int[] r = new int[256];
        int[] g = new int[256];
        int[] b = new int[256];

        for (int bb = 0; bb < 51; bb++)
        {
            r[bb] = 255 - t;
            g[bb] = 0;
            b[bb] = 255;
            t = t + 5;
        }
        t = 0;
        for (int bb = 51; bb < 102; bb++)
        {
            r[bb] = 0;
            g[bb] = t;
            b[bb] = 255;
            t = t + 5;
        }
        t = 0;
        for (int bb = 102; bb < 153; bb++)
        {
            r[bb] = 0;
            g[bb] = 255;
            b[bb] = 255 - t;
            t = t + 5;
        }
        t = 0;
        for (int bb = 153; bb < 204; bb++)
        {
            r[bb] = t;
            g[bb] = 255;
            b[bb] = 0;
            t = t + 5;
        }
        t = 0;
        for (int bb = 204; bb < 256; bb++)
        {
            r[bb] = 255;
            g[bb] = 255 - t;
            b[bb] = 0;
            t = t + 5;
        }

        //Na³o¿enie mapy kolorów
        int index;
        for( int nr_y = 0; nr_y < 480; nr_y++)
        {
            for(int nr_x = 0; nr_x < 640; nr_x++)
            {
                index = macierz[nr_y, nr_x];
                System.Drawing.Color c = System.Drawing.Color.FromArgb(255, r[index], g[index], b[index]);
                bmpSpectrum.SetPixel(nr_x, nr_y, c);
            }
        }
    }
}
