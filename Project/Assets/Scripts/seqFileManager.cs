using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;

//biblioteki obs³ugi aplikacji na HoloLens
#if !UNITY_EDITOR && UNITY_WSA_10_0
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
#endif



//Skrypt uruchomienia menad¿era plików dla plików seq
public class seqFileManager : MonoBehaviour
{
    public string path;
    public int nr_klatki = 1;
    public Text textLoad;
    public Text textPath;
    readSeq readS;

    public void OpenFileBrowser()
    {
        //Czêœæ kodu odpowiadaj¹ca za uruchomienie menad¿era plików za pomoc¹ edytora unity razem z bibliotek¹ Unityeditor
        
        path = EditorUtility.OpenFilePanel("Overwrite with seq", "", "seq");
        readS = GameObject.FindGameObjectWithTag("TagSeq").GetComponent<readSeq>();
        readS.getImage(path, nr_klatki);
        


        //Czêœæ kodu odpowiadaj¹ca za otworzenie przyk³adowego pliku za pomoc¹ HoloLens
        /*
#if !UNITY_EDITOR && UNITY_WSA_10_0
                        UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
                        {
                            var filepicker = new FileOpenPicker(); 
                            filepicker.FileTypeFilter.Add(".seq");
                            filepicker.FileTypeFilter.Add(".raw");

                            var file = await filepicker.PickSingleFileAsync();
                            UnityEngine.WSA.Application.InvokeOnAppThread(() => 
                            {

                                path = (file != null) ? file.Path : "No data";
                                readS = GameObject.FindGameObjectWithTag("TagSeq").GetComponent<readSeq>();
                                readS.getImage(path, nr_klatki);
                                textLoad.text="WCZYTANO PLIK SEQ"; 
                                textPath.text = path;
                                

                            }, false);
                        }, false);
#endif
        */


    }
}


