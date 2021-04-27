using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public SceneFader sceneFader;
    public float delay = 0.01f;
    
    string story;
    Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        txt.text = "";

        StartCoroutine(PlayText());
    }
    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(delay);
        }

        sceneFader.FadeTo("Level1");
    }
}
