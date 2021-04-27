using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LogicalObject : MonoBehaviour
{
    [Header("Logical Object")]
    public bool outputValue;
    [HideInInspector] public byte outputByte;
    [HideInInspector] public string outputString;

    [SerializeField] protected bool interactable = false;

    [Header("Render")]
    public TMP_Text outputText;
    public Material matOff;
    public Material matOn;
    [SerializeField] public  MeshRenderer renderToChange;
    public void ChangeSource()
    {
        outputValue = !outputValue;
        outputByte = Convert.ToByte(outputValue);
    }

    public virtual void ChangeRender()
    {
        if (outputValue) renderToChange.material = matOn;
        else renderToChange.material = matOff;
        outputString = outputText.text = outputByte.ToString();
    }
}