using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }
    #region Methods To Call
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    public void FadeToIndex(int sceneIndex)
    {
        StartCoroutine(FadeOutIndex(sceneIndex));
    }

    public void AdditiveFadeTo(string scene)
    {
        StartCoroutine(FadeoutAdditive(scene));
    }
    public void AdditiveFadeToIndex(int sceneIndex)
    {
        StartCoroutine(FadeOutAdditiveIndex(sceneIndex));
    }
    #endregion
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    #region Fadeout Normal
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }

    IEnumerator FadeOutIndex(int sceneIndex)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion

    #region Fadeout Additive
    IEnumerator FadeoutAdditive(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    IEnumerator FadeOutAdditiveIndex(int sceneIndex)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }
    #endregion
}