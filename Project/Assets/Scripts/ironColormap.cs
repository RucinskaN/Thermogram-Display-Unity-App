using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

//Skrypt na³o¿enia mapy kolorów iron
public class ironColormap : MonoBehaviour
{
    readSeq readS;
    createTexture createTex;

    public void setMapIron()
    {
        //Importowanie danych aktualnie wyœwietlanego termogramu
        readS = GameObject.FindGameObjectWithTag("TagSeq").GetComponent<readSeq>();
        int[,] macierzGrey;
        macierzGrey = readS.macierzGreyInt;
        Bitmap bmpIron = new Bitmap(640, 480);
        getIron(macierzGrey, bmpIron);
        int flagBuff = 3;
        readS.flag = flagBuff;

        //Wywo³anie skryptu zmieniaj¹cego bitmapê w texturê 
        createTex = GameObject.FindGameObjectWithTag("TagTex").GetComponent<createTexture>();
        createTex.create(bmpIron);
    }

    void getIron(int[,] macierz, Bitmap bmpIron)
    {
        //Definicja mapy kolorów
        int t = 0;
        int[] r = new int[256];
        int[] g = new int[256];
        int[] b = new int[256];

        for(int bb = 0; bb < 128; bb++)
        {
            r[bb] = t;
            g[bb] = 0;
            b[bb] = g[bb];
            t = t + 2;
        }
        t = 0;
        for(int bb = 128; bb < 256; bb++)
        {
            r[bb] = 255;
            g[bb] = t;
            b[bb] = 0;
            t = t + 2;
        }
        t = 0;
        for(int bb = 191; bb < 255; bb++)
        {
            b[bb] = t;
            t = t + 4;
        }
        b[255] = 255;

        //Na³o¿enie mapy
        int index;
        for(int nr_y = 0; nr_y < 480; nr_y++)
        {
            for(int nr_x = 0; nr_x < 640; nr_x++)
            {
                index = macierz[nr_y, nr_x];
                System.Drawing.Color c = System.Drawing.Color.FromArgb(255, r[index], g[index], b[index]);
                bmpIron.SetPixel(nr_x, nr_y, c);
            }
        }
    }

}
