using UnityEngine;
using TMPro;

public class GetVersion : MonoBehaviour
{
    public TextMeshProUGUI gameVer;
    void Awake()
    {
        gameVer.text = GameData.gameVersion;
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
