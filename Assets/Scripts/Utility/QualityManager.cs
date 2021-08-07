using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public enum QualityLevels { Low, Medium, High }

[Serializable]
public class Quality
{
    [SerializeField] string name = string.Empty;
    public string Name { get { return name; } }

    [SerializeField] QualityLevels qualityLevel = QualityLevels.Medium;
    public QualityLevels QualityLevel { get { return qualityLevel; } }

    [SerializeField] RenderPipelineAsset qualitypipeline = null;
    public RenderPipelineAsset qualityPipeline { get { return qualitypipeline; } }
}
public class QualityManager : MonoBehaviour
{
    public static QualityManager Instance = null;

    [SerializeField] Quality[] qualities;
    //[SerializeField] int qualityPrefs;

    private void Awake()
    {
        //qualityPrefs = PlayerPrefs.GetInt("Quality");
        if (Instance != null)
        { Destroy(gameObject); }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        QualitySettings.SetQualityLevel(2);
    }

    private void SetPrefs(int qualityLevel)
    {
        PlayerPrefs.SetInt("UnityGraphicsQuality", qualityLevel);
        PlayerPrefs.SetInt("Quality", qualityLevel);
    }

    public void SetQuality(QualityLevels level)
    {
        var quality = (int)GetQualityLevels(level).QualityLevel;
        QualitySettings.SetQualityLevel(quality, true);

        SetPrefs(quality);
    }

    #region Getters
    Quality GetQualityLevels(QualityLevels level)
    {
        foreach (var quality in qualities)
        {
            if (quality.QualityLevel == level)
            {

                return quality;
            }
        }
        return null;
    }
    #endregion
}
