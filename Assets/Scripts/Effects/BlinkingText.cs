using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    //public AnimationCurve curve;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Sin(Time.time * 4f));
    }

}
