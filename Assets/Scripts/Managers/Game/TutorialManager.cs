using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : GameManager
{
    [Header("Tutorial")]
    public PlayerMovement movement;
    public CanvasGroup canvasGroupTutorial;
    public Canvas TutorialCanvas;
    public GameObject[] TutorialPanels;

    public override void Start()
    {
        base.Start();
        sceneName = SceneManager.GetActiveScene().name;
        nextScene = GameData.nextScene;
    }

    #region Anim
    public override IEnumerator StartAnimation()
    {
        float t = 2.5f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            sceneTitle.text = titleName;
            sceneTitle.color = new Color(255, 255, 255, a);
            yield return 0;
        }
        playerInput.enabled = true;
        ToggleTutorial(0);
    }
    private IEnumerator OpenTutorial()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            canvasGroupTutorial.alpha = a;
            yield return 0;
        }
    }
    private IEnumerator CloseTutorial() 
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            canvasGroupTutorial.alpha = a;
            yield return 0;
        }
    }
    #endregion

    public void ToggleTutorial(int index)
    {
        TutorialPanels[index].SetActive(!TutorialPanels[index].activeSelf);
        if (TutorialPanels[index].activeSelf == true)
        {
            playerInput.enabled = false;
            playerInput.InputVector = new Vector2(0, 0);
            StartCoroutine(CloseTutorial());
        }
        else
        {
            playerInput.enabled = true;
            StartCoroutine(OpenTutorial());
        }
    }

    public override void NextLevel()
    {       
        if (string.IsNullOrEmpty(GameData.nextScene))
        {
            fader.FadeTo("LevelSelection");
        }
        else
        {
            GameData.nextScene =  nextScene;
            fader.FadeTo("Loading");
        }
    }

    protected override void UpdatePlayerPrefs()
    {
        PlayerPrefs.SetInt("TutorialFinished", Convert.ToInt32(true));
        GameData.tutorialFinished = true;
    }
}
