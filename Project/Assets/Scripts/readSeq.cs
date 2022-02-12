using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

//Skrypt odczytuje sekwencjê seq oraz wyœwietla obraz w skali szaroœci 
public class readSeq : MonoBehaviour
{
    public Text text_Lklatek;
    public Text text_klatka;
    public int[,] macierzGreyInt = new int[480, 640];
    public string pathBuff;
    public int nr_klatkiBuff;
    public int l_klatek;
    public int flag;
    createTexture createTex;

    public void getImage(string path, int nr_klatki)
    {

        //ZMIENNE
        pathBuff = path;
        nr_klatkiBuff = nr_klatki;
        int l_bajtow;
        int offset = 2781;
        int[,] macierz2D = new int[480, 640];

        //Odczytanie pliku
        using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            l_bajtow = (int)fs.Length;
            l_klatek = (l_bajtow - 617180) / 617052 + 1;

            //macierz przechowania danych
            byte[] bytes = new byte[l_bajtow];

            //Ustawienie offset dla poszczególnych klatek
            if(nr_klatki > 1)
            {
                offset = (nr_klatki - 2) * 617052 + 617180 + 2653;
            }

            //Zapisanie sekwencji do macierzy bytes
            using (MemoryStream ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                bytes = ms.ToArray();
            }

            //Odczytanie sygna³u 
            for(int nr_y = 0; nr_y < 480; nr_y++)
            {
                for(int nr_x = 0; nr_x < 640; nr_x++)
                {
                    int wartosc = getInt(bytes[offset], bytes[offset + 1]);
                    macierz2D[nr_y, nr_x] = wartosc;
                    offset = offset + 2;
                }
            }
            text_klatka.text = $"Wyœwietlana klatka: {nr_klatki}";
            text_Lklatek.text = $"Liczba klatek: {l_klatek - 1}";
            flag = 1;
            Array.Clear(bytes, 0, l_bajtow);
        }
        //Znalezeienie najmniejszego i najwiêkszego elementu w macierz2D
        int w_minimalna = macierz2D[0, 0];
        int w_maksymalna = macierz2D[0, 0];
        for (int y = 0; y < 480; y++)
        {
            for (int x = 0; x < 640; x++)
            {
                if (w_minimalna > macierz2D[y, x])
                {
                    w_minimalna = macierz2D[y, x];
                }
                if(w_maksymalna < macierz2D[y, x])
                {
                    w_maksymalna = macierz2D[y, x];
                }
            }
        }

        //Wyœwietlenie obrazu w skali szaroœci
        double[,] macierzGrey = new double[480, 640];
        getGrey(macierz2D, macierzGrey, w_minimalna, w_maksymalna);

        //Zamiana macierzy double na int
        
        for (int i = 0; i < 480; i++)
        {
            for (int j = 0; j < 640; j++)
            {
                macierzGreyInt[i, j] = (int)macierzGrey[i, j];
                //Debug.Log($"macier zgrey int - {macierzGreyBuff[i,j]}");
            }
        }

        //Zamiana macierz -> bitmapa
        
        Bitmap bitmap;
        bitmap = toBitmap(macierzGreyInt);

        //Wywo³anie funkcji wyœwietlenia bitmapy
        createTex = GameObject.FindGameObjectWithTag("TagTex").GetComponent<createTexture>();
        createTex.create(bitmap);
    }

    public int getInt(double bajt1, double bajt2)
    {
        double lsb = bajt1;
        double msb = bajt2;
        if(lsb < 0)
        {
            lsb = lsb + 256;
        }
        if(msb < 0)
        {
            msb = msb + 256;
        }
        int wynik = ((int)lsb << 8) + (int)msb;
        return wynik;
    }

    public void getGrey(int[,] macierz2D, double[,] macierzGrey, int min, int max)
    {
        double a;
        double b;

        //Rozwi¹zanie uk³adu y = ax+b ¿eby uzyskaæ macierz szaroœci
        a = 255 / ((double)max - (double)min);
        b = (-a) * (double)min;

        //Tworzenie macierzy double w skali szaroœci
        for (int i = 0; i < 480; i++)
        {
            for (int j = 0; j < 640; j++)
            {
                macierzGrey[i, j] = (a * (double)macierz2D[i, j]) + b;
            }
        }
    }

    //Funkcja tworzenia bitmapy dla edytora Unity
    
    public Bitmap toBitmap(int[,] image)
    {
        int width = image.GetLength(0);
        int height = image.GetLength(1);
        Bitmap bmp = new Bitmap(height, width);
        BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, height, width), 
            ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        unsafe
        {
            byte* address = (byte*)bitmapData.Scan0;

            for (int i = 0; i < bitmapData.Height; i++)
            {
                for (int j = 0; j < bitmapData.Width; j++)
                {
                    address[0] = (byte)image[i, j];
                    address[1] = (byte)image[i, j];
                    address[2] = (byte)image[i, j];
                    address[3] = (byte)255;
                    
                    address += 4;
                }

                address += (bitmapData.Stride - (bitmapData.Width * 4));
            }
        }
        bmp.UnlockBits(bitmapData);

        return bmp;
    }

}
