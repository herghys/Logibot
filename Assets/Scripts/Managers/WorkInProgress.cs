using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkInProgress : MonoBehaviour
{
    [SerializeField] CanvasGroup wipGroup;
    [SerializeField] AnimationCurve curve;

    public void ShowWIP()
    {
        StartCoroutine(Show());
    }
    private IEnumerator Show()
    {
        float t = 0f;
        wipGroup.blocksRaycasts = true;
        while (t < 1)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            wipGroup.alpha = a;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            wipGroup.alpha = a;
        }
        wipGroup.blocksRaycasts = false;
        yield return null;
    }
}
