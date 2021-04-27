using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetVersion : MonoBehaviour
{
    public Text gameVer;
    public Text unityVer;
    public SceneFader sceneFade;
    void Start()
    {
        gameVer.text = "Game Version " + Application.version;
        unityVer.text = "Made with Unity " + Application.unityVersion;
    }

    // Update is called once per frame
    public void BackToMenu()
    {
        sceneFade.FadeTo("MainMenu");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneFade.FadeTo("MainMenu");
        }
    }
    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/herghys");
    }

    public void Steam()
    {
        Application.OpenURL("https://steamcommunity.com/id/herghys");
    }
}
