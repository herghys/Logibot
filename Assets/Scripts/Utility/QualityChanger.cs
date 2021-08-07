using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityChanger : MonoBehaviour
{
    public TMPro.TMP_Dropdown QualitySettingDropdown;
    void OnEnable()
    {
        QualitySettingDropdown.value = PlayerPrefs.GetInt("Quality");
    }

    public void ChangeQuality()
    {
        AudioManager.Instance.PlaySound("MenuTickSFX");
        var level = QualitySettingDropdown.value;
        QualityManager.Instance.SetQuality((QualityLevels)level);
    }
}
