using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[Serializable]
public struct UIMenuElements
{
    public Animator mainAnimator;
    public SceneFader sceneFader;
    public CanvasGroup mainCanvasGroup;
    public Animation anim;
    public TMP_Dropdown QualitySettingDropdown;
}

[Serializable]
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] UIMenuElements uiMenuElements = new UIMenuElements();

    private int unlockedLevel;
    private int uiStateParaHash = 0;

    [SerializeField] bool musicPlayed;

    private IEnumerator IE_Menu, IE_PostFX;

    private void Awake()
    {
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevels");
        uiStateParaHash = Animator.StringToHash("MenuState");
        uiMenuElements.QualitySettingDropdown.value = PlayerPrefs.GetInt("Quality");

        musicPlayed = AudioManager.Instance.GetMusicState("MainMusic");
    }

    void Start()
    {
        if(!musicPlayed) AudioManager.Instance.PlaySound("MainMusic");

        IE_Menu = OpenMainMenu();
        CheckUnlockedLevel(unlockedLevel);
       
        StartCoroutine(IE_Menu);
    }

    private void CheckUnlockedLevel(int levels)
    {
        if (levels != 0)
        {
            GameData.unlockedLevels = levels;
        }
        else
        {
            GameData.unlockedLevels = 1;
            PlayerPrefs.SetInt("UnlockedLevels", 1);
        }
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        Application.Quit();
    }
    public void ChangeQuality()
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        var level = uiMenuElements.QualitySettingDropdown.value;
        QualityManager.Instance.SetQuality((QualityLevels)level);
    }

    #region Menu Animation
    IEnumerator OpenMainMenu()
    {
        uiMenuElements.mainAnimator.SetInteger(uiStateParaHash, 0);
        while (uiMenuElements.anim.isPlaying)
        {
            uiMenuElements.mainCanvasGroup.interactable = false;
        }
        uiMenuElements.mainCanvasGroup.interactable = true;
        yield return new WaitForSeconds(3);
    }

    IEnumerator CloseMainMenu()
    {
        do
        {
            yield return null;
        } while (uiMenuElements.anim.isPlaying);
    }
    #endregion

    #region Panel Opener
    public void PanelOpener(GameObject targetObject)
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        targetObject.SetActive(!targetObject.activeSelf);
    }

    public void PanelOpener(GameObject targetObject, bool isStart)
    {
        targetObject.SetActive(!targetObject.activeSelf);
    }
    #endregion

    #region SceneFader
    public void NextScene(string sceneName)
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        IE_Menu = CloseMainMenu();
        StartCoroutine(IE_Menu);
        uiMenuElements.sceneFader.FadeTo(sceneName);
    }
    public void NextSceneIndex(int sceneIndex)
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        IE_Menu = CloseMainMenu();
        StartCoroutine(IE_Menu);
        uiMenuElements.sceneFader.FadeToIndex(sceneIndex);
    }
    #endregion

    private void OnDisable()
    {
        
    }
}
