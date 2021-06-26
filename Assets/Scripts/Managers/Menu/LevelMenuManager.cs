using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] int totalLevel;
    [SerializeField] WorkInProgress wip;

    private int unlockedLevels;
    private bool tutorialFinished;
    public Button[] levelButtons;
    public SceneFader fader;

    [SerializeField] bool musicPlayed;

    #region Unity Defaults
    private void Awake()
    {
        musicPlayed = AudioManager.Instance.GetMusicState("MainMusic");
        unlockedLevels = GameData.unlockedLevels;
        tutorialFinished = Convert.ToBoolean(PlayerPrefs.GetInt("TutorialFinished"));

        GameData.tutorialFinished = tutorialFinished;
        GameData.previousScene = SceneManager.GetActiveScene().name;

        if (levelButtons == null || levelButtons.Length == 0)
        {
            levelButtons = GameObject.FindGameObjectsWithTag("LevelButton")
            .Select(obj => obj.GetComponent<Button>()).ToArray();
        }

        wip = GetComponent<WorkInProgress>();
    }

    private void Start()
    {
        if (!musicPlayed) AudioManager.Instance.PlaySound("MainMusic");
        totalLevel = levelButtons.Length;
        LevelsUnlocker(unlockedLevels);
    }
    #endregion

    #region Level Manager
    private void LevelsUnlocker(int levels)
    {
        if (levels < GameData.maxLevel)
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (i + 1 > levels)
                {
                    levelButtons[i].interactable = false;
                }
            }
        }
        else if(levels > GameData.maxLevel)
        {
            unlockedLevels = GameData.maxLevel;
        }
        
    }
    #endregion

    #region Panel Opener
    public void PanelOpener(CanvasGroup targetGroup)
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        if (targetGroup.alpha == 0)
        {
            targetGroup.alpha = 1;
            targetGroup.interactable = true;
            targetGroup.blocksRaycasts = true;
        }
        else
        {
            targetGroup.alpha = 0;
            targetGroup.interactable = false;
            targetGroup.blocksRaycasts = false;
        }
    }
    #endregion

    #region GoTo
    public void GoToLevelNormal(string levelName)
    {
        GameData.nextScene = "Puzzle_ " + levelName;
        //Check Tutorial
        if (!tutorialFinished)
        {
            //Loading
            GameData.levelToLoad = "Puzzle_Tutorial";
            fader.FadeTo("Loading");
        }
        else
        {
            GameData.levelToLoad = "Puzzle_ " + levelName;
            fader.FadeTo("Puzzle_ " + levelName);
        }

    }

    public void GoToTutorial()
    {
        GameData.levelToLoad = "Puzzle_Tutorial";
        fader.FadeTo("Loading");
    }

    public void GoToLevelQuiz(string quizMateri)
    {
        AudioManager.Instance.StopAll();
        GameData.QuizMateri = quizMateri;

        //Debug.Log(GameData.xmlFile + GameData.QuizMateri + ".xml");

        fader.FadeTo("QuizGame");
    }

    public void GoToMenu(string scene)
    {
        fader.FadeTo(scene);
    }

    public void WorkInProgress()
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        wip.ShowWIP();
    }
    #endregion
}
