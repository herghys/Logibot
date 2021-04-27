using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct LoadingUI
{
    [Header("Slider")]
    public Slider loadingSlider;
    public Gradient colorGradient;
    public Image fill;
    public TMP_Text loadingText;

    public SceneFader fader;

}
public class LoadingScript : MonoBehaviour
{
    [SerializeField] LoadingUI loadingUI = new LoadingUI();

    [SerializeField]
    private string sceneToLoad;
    private void Awake()
    {
        if (GameData.tutorialFinished && !string.IsNullOrEmpty(GameData.nextScene))
        {
            sceneToLoad = GameData.nextScene;
        }
        else
            sceneToLoad = GameData.levelToLoad;
    }
    void Start()
    {
        StartCoroutine(LoadAsync(sceneToLoad));
    }

    IEnumerator LoadAsync (string scene)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingUI.loadingSlider.value = progress;
            yield return null;
        }
    }
}
