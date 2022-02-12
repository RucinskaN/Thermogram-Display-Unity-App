using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;

//biblioteki obs�ugi aplikacji na HoloLens
#if !UNITY_EDITOR && UNITY_WSA_10_0
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.AccessCache;
using System.Threading.Tasks;
#endif

//Skrypt uruchomienia menad�era plik�w dla obraz�w RGB
public class fileManager : MonoBehaviour
{
    public RawImage rawImage;
    public string path;

    //Cz�� kodu odpowiadaj�ca za uruchomienie menad�era plik�w na urz�dzeniu
    /*
     public void OpenFileBrowser()
     {
       #if !UNITY_EDITOR && UNITY_WSA_10_0
                 UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
                 {
                     var filepicker = new FileOpenPicker();
                     filepicker.FileTypeFilter.Add(".jpg");
                     filepicker.FileTypeFilter.Add(".jpeg");
                     filepicker.FileTypeFilter.Add(".png");

                     var file = await filepicker.PickSingleFileAsync();
                     UnityEngine.WSA.Application.InvokeOnAppThread(() => 
                     {
                         path = (file != null) ? file.Path : "No data";

                         StartCoroutine(LoadImage(path));

                     }, false);
                 }, false);

         #endif
     }

     IEnumerator LoadImage(string path)
     {
         WWW www = new WWW("file:///" + path);
         rawImage.texture = www.texture;
         int wth = rawImage.texture.width;
         int hgt = rawImage.texture.height;
         rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(wth, hgt);
         yield return www;
     }
    */


    //Cz�� kodu, kt�ra wraz z bibliotek� UnityEditor umo�liwia na uruchomienie aplikacji w unity
    
    public void OpenFileBrowser()
    {

        path = EditorUtility.OpenFilePanel("Wczytaj obraz", "", "Formats; *.png; *.jpg; *jpeg");

        GetImage();

    }

    void GetImage()
    {
        if (path != null)
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        WWW www = new WWW("file:///" + path);
        rawImage.texture = www.texture;
        int wth = rawImage.texture.width;
        int hgt = rawImage.texture.height;
        rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(wth, hgt);
    }
}