using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Uruchomienie filmu poklatkowego z³o¿onego ze wszystkich klatek danej sekwencji
public class allFrames : MonoBehaviour
{
    readSeq readS;
    spectrumColormap spectrumColor;
    ironColormap ironColor;

    public void play()
    {
        StartCoroutine(startPlay());
    }
    IEnumerator startPlay()
    {
        readS = GameObject.FindGameObjectWithTag("TagSeq").GetComponent<readSeq>();
        int flagBuff;
        flagBuff = readS.flag;
        string path = readS.pathBuff;
        int l_klatek = readS.l_klatek;
        //uruchomienie dla skali szaroœci
        if (flagBuff == 1)
        {
            for (int klatka = 1; klatka < l_klatek ; klatka++)
            {
                readS.getImage(path, klatka);
                yield return new WaitForSeconds(0.5f);

            }
        }
        //uruchomienie dla mapy spectrum
        if(flagBuff == 2)
        {
            for(int klatka = 1; klatka < l_klatek; klatka++)
            {
                readS.getImage(path, klatka);
                spectrumColor = GameObject.FindGameObjectWithTag("TagSpect").GetComponent<spectrumColormap>();
                spectrumColor.setMap();
                yield return new WaitForSeconds(0.5f);
            }
        }
        //uruchomienie dla mapy iron
        if(flagBuff == 3)
        {
            for(int klatka = 1; klatka < l_klatek; klatka++)
            {
                readS.getImage(path, klatka);
                ironColor = GameObject.FindGameObjectWithTag("TagIron").GetComponent<ironColormap>();
                ironColor.setMapIron();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
