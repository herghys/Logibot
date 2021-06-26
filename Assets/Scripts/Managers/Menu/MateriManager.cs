using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MateriManager : MonoBehaviour
{
    public SceneFader fader;
    public AnimationCurve curve;
    public VideoPlayer[] videos;

    bool musicPlayed;

    private void Awake()
    {
        if(AudioManager.Instance.GetMusicState("MainMusic")) AudioManager.Instance.ToggleMusic("MainMusic", true);

        musicPlayed = AudioManager.Instance.GetMusicState("MateriMusic");
        AudioManager.Instance.PlaySound("MateriMusic");
    }

    public void GoToScene(string scene)
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        fader.FadeTo(scene);
    }

    public void PanelOpener(CanvasGroup target)
    {
        if (target.alpha == 0)
            StartCoroutine(OpenAnim(target, 0));
        //target.alpha = 1;
        else
            StartCoroutine(CloseAnim(target, 1));
        //target.alpha = 0;
        AudioManager.Instance.PlaySound("MenuTickSFX");
        target.interactable = !target.interactable;
        target.blocksRaycasts = !target.blocksRaycasts;
    }

    IEnumerator OpenAnim(CanvasGroup target, float time)
    {
        //float t = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * 3;
            float a = curve.Evaluate(time);
            target.alpha = a;
            yield return 0;
        }
    }

    IEnumerator CloseAnim(CanvasGroup target, float time)
    {
        //float t = 1f;
        while (time > 0f)
        {
            time -= Time.deltaTime * 3;
            float a = curve.Evaluate(time);
            target.alpha = a;
            yield return 0;
        }
    }

    public void VideoStopper()
    {
        foreach (var item in videos)
        {
            item.Stop();
        }
    }

    private void OnDisable()
    {
            AudioManager.Instance.StopSound("MateriMusic");
    }
}
