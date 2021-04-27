using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextScene;

    [Header("Controls")]
    [SerializeField] protected InputHandler playerInput;
    [SerializeField] protected Transform ControlCanvas;
    [SerializeField] protected GameObject OnScreenPrefab;

    [Header("Game")]
    [SerializeField] protected int unlockedLevels;
    [SerializeField] protected TMP_Text kunci;
    [SerializeField] protected TMP_Text pola;
    [SerializeField] protected LogicEnd le;

    [Header("Scenes")]
    public SceneFader fader;
    public AnimationCurve curve;
    public string titleName;
    public TMP_Text sceneTitle;
    [SerializeField] protected string sceneName;
    
    [Header("End Indicator")]
    public bool endUnlocked;
    public EndBarrierField endBarrier;
    public CanvasGroup endGameGroup;
    private void OnEnable()
    {
        if (ControlCanvas == null)
            ControlCanvas = GameObject.FindGameObjectWithTag("Control").GetComponent<Transform>();
    }
    private void Awake()
    {
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels");
        if (playerInput == null)
            playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();
        

        playerInput.enabled = false;
        sceneName = SceneManager.GetActiveScene().name.ToString();
        unlockedLevels = GameData.unlockedLevels;
    }

    public virtual void Start()
    {

#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        Instantiate(OnScreenPrefab, ControlCanvas);
#endif
        StartCoroutine(StartAnimation());
        kunci.text = ($"Target: {le.desiredValue}<sub>{le.basis}</sub>");
    }

    public virtual IEnumerator StartAnimation()
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
    }

    #region UI & Toggler
    public void TogglePause(GameObject target)
    {
        ToggleTime();
        target.SetActive(!target.activeSelf);
    }

    private void ToggleTime()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        else if (Time.timeScale == 1f) Time.timeScale = 0f;
    }

    public void TogglePanel(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }

    public void RestartGame()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        fader.FadeTo(sceneName);
    }

    public void GoToScene(string scene)
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        fader.FadeTo(scene);
    }

    public void GoToSceneIndex(int index)
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        fader.FadeToIndex(index);
    }
    #endregion

    #region Final Checker
    public virtual void UpdateFinal(bool end)
    {
        pola.text = le.inputString;
        if (!end)
        {
            endUnlocked = false;
            
            endBarrier.Shield();
        }
        else
        {
            endUnlocked = true;
            
            endBarrier.Shield();
        }
    }
    #endregion

    public virtual void EndGame()
    {
        UpdatePlayerPrefs();
        StartCoroutine(EndGameAnim());
        endGameGroup.blocksRaycasts = true;
        endGameGroup.interactable = true;
    }

    public virtual void NextLevel()
    {
        GameData.nextScene = "Puzzle_ " + nextScene;
        fader.FadeTo("Loading");
    }

    IEnumerator EndGameAnim()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            endGameGroup.alpha = a;
            yield return 0;
        }
    }

    protected virtual void UpdatePlayerPrefs()
    {
        if (unlockedLevels < GameData.maxLevel)
        {
            unlockedLevels += 1;
        }
        GameData.unlockedLevels = unlockedLevels;
        PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
    }
}