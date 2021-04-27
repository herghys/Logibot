using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public static FPSCounter Instance = null;
    [SerializeField] private float timer, refresh, avgFramerate;
    [SerializeField] private bool shoFPS;
    [SerializeField] private Text text;
    private void Awake()
    {
        if (Instance != null)
        { Destroy(gameObject); }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        text.text = avgFramerate.ToString();
    }
}
