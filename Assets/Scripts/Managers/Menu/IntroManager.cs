using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif


[RequireComponent(typeof(Slider))]
public class IntroManager : MonoBehaviour
{
    [SerializeField] LoadingUI loadingUI = new LoadingUI();
    private IEnumerator IE_LoadAsync;

    private void Start()
    {
        /*if (!Directory.Exists(GameData.persistentXmlPath))
        {
            Directory.CreateDirectory(GameData.persistentXmlPath);
        }
        if (!Directory.Exists(GameData.usedPersistPath))
        {
            Directory.CreateDirectory(GameData.usedPersistPath);
        }*/

#if UNITY_ANDROID
        StartCoroutine(RequestPermissionRoutine());
#endif

        IE_LoadAsync = LoadAsync("MainMenu");
        /*StartCoroutine(CopyPersist(GameData.xmlFile + "3.1.xml"));
        StartCoroutine(CopyPersist(GameData.xmlFile + "3.2.xml"));
        StartCoroutine(CopyPersist(GameData.xmlFile + "Mix.xml"));*/
        StartCoroutine(IE_LoadAsync);       
    }

    IEnumerator RequestPermissionRoutine()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        yield return new WaitForSeconds(2);
    }

    IEnumerator LoadAsync(string scene)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingUI.loadingText.text = (progress * 100).ToString() + "%";
            loadingUI.loadingSlider.value = progress;
            yield return new WaitForSeconds (1);
        }
    }

    /*IEnumerator CopyPersist(string file)
    {
        var streampath = Path.Combine(GameData.streamingXmlPath, file);
        var newPath = Path.Combine(GameData.persistentXmlPath,file);
        WWW www = new WWW(streampath);
        yield return www;

        if (File.Exists(newPath))
        {
            yield return new WaitForSeconds(1);
        }
        else
        {
            while (!www.isDone)
            {
                float progress = www.progress;
                loadingUI.loadingText.text = "Downloading " + file + "... " + progress.ToString() + "%";
                yield return new WaitForSeconds(2);
            }
            File.WriteAllBytes(newPath, www.bytes);
        }
    }*/
}
