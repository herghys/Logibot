using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI loadingText;

    [SerializeField] string sceneToLoad;

    int qualityPrefs;
    IEnumerator IE_LoadAsync;
    AsyncOperation asyncLoad;

    private void OnEnable()
    {
        SceneManager.LoadScene("Persist", LoadSceneMode.Additive);
    }
    private void Awake()
    {
        Debug.Log("Masuk Intro");
        qualityPrefs = PlayerPrefs.GetInt("Quality");
    }

    void Start()
    {
        //Application.version + "-" + Application.platform;
        GameData.gameVersion = string.Format("Game Ver. {0} ({1})", Application.version.ToString(), Application.platform.ToString());
        Debug.Log(GameData.gameVersion);
        IE_LoadAsync = LoadScene("MainMenu");
        StartCoroutine(IE_LoadAsync);
        SceneManager.UnloadSceneAsync("Persist");
    }

    private IEnumerator LoadScene(string scene)
    {
        asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            slider.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            if (asyncLoad.progress >= 0.9f)
            {               
                loadingText.text = "Tap to Continue";
            }
            yield return null;
        }     
    }

    public void Tap()
    {
        if (asyncLoad.progress >= 0.9f)
        {
            QualitySettings.SetQualityLevel(qualityPrefs);
            asyncLoad.allowSceneActivation = true;
        }
    }
}
