using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class nextFrame : MonoBehaviour
{
    readSeq readS;
    spectrumColormap spectrumColor;
    ironColormap ironColor;
    
    //Prze��czenie na nast�pn� klatk� 
    public void next()
    {
        //Importowanie danych aktualnie wy�wietlanego termogramu
        readS = GameObject.FindGameObjectWithTag("TagSeq").GetComponent<readSeq>();
        int flagBuff;
        flagBuff = readS.flag;
        string path = readS.pathBuff;
        int nr_klatki = readS.nr_klatkiBuff;
        int l_klatek = readS.l_klatek;

        //Zmiana klatki dla skali szaro�ci
        if(flagBuff == 1)
        {
            if (nr_klatki < (l_klatek - 1))
            {
                nr_klatki = nr_klatki + 1;
                readS.getImage(path, nr_klatki);
            }
        }
        //zmiana klatki dla mapy spectrum
        if(flagBuff == 2)
        {
            if (nr_klatki < (l_klatek - 1))
            {
                nr_klatki = nr_klatki + 1;
                readS.getImage(path, nr_klatki);
                spectrumColor = GameObject.FindGameObjectWithTag("TagSpect").GetComponent<spectrumColormap>();
                spectrumColor.setMap();
            }
        }
        //zmiana klatki dla mapy iron
        if (flagBuff == 3)
        {
            if (nr_klatki < (l_klatek - 1))
            {
                nr_klatki = nr_klatki + 1;
                readS.getImage(path, nr_klatki);
                ironColor = GameObject.FindGameObjectWithTag("TagIron").GetComponent<ironColormap>();
                ironColor.setMapIron();
            }
        }
    }
}
