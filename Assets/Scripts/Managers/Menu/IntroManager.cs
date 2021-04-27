using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] LoadingUI loadingUI = new LoadingUI();
    private IEnumerator IE_LoadAsync;

    private void Start()
    {
        AudioManager.Instance.PlaySound("MainMusic");
        IE_LoadAsync = LoadAsync("MainMenu");
        StartCoroutine(IE_LoadAsync);
    }

    IEnumerator LoadAsync(string scene)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingUI.loadingText.text = (progress * 100).ToString() + "%";
            loadingUI.loadingSlider.value = progress;
            yield return null;
        }
    }
}
