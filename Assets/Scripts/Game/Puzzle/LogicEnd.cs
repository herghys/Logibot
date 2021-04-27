using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BaseType { Dec, Bin, Oct, Hex }
public class LogicEnd : LogicalObject
{
    [Header("Logic End")]
    public BaseType baseType = BaseType.Bin;
    public string basis;
    [SerializeField] public string desiredValue;
    [SerializeField] private string testValue;
    public string inputString;

    public TMP_Text text = null;
    public LogicGates[] inputs;

    public GameManager gm;

    private void Awake()
    {
        ConvertDesiredValue();
    }

    private void Start()
    {
        UpdateEnd();
    }

    void ConvertDesiredValue()
    {
        long value = 0;
        switch (baseType)
        {
            case BaseType.Dec:
                basis = "DEC";
                text.text = desiredValue + "<sub>Dec</sub>";
                
                value = long.Parse(desiredValue);
                break;
            case BaseType.Bin:
                basis = "BIN";
                text.text = desiredValue + "<sub>Bin</sub>";
                value = Convert.ToInt64(desiredValue, 2);
                
                break;
            case BaseType.Oct:
                basis = "OCT";
                text.text = desiredValue + "<sub>Oct</sub>";
                value = Convert.ToInt64(desiredValue, 8);
                
                break;
            case BaseType.Hex:
                text.text = desiredValue + "<sub>Hex</sub>";
                value = Convert.ToInt64(desiredValue, 16);
                basis = "HEX";
                break;
        }

        testValue = (baseType == BaseType.Bin) ? desiredValue.ToString() : Convert.ToString(value, 2);
        
    }

    private void CheckInput()
    {
        inputString = string.Empty;
        foreach (var item in inputs)
        {
            inputString += (item.outputString);
        }

    }

    public void UpdateEnd()
    {
        CheckInput();
        if (testValue == inputString)
        {
            renderToChange.material = matOn;
            gm.UpdateFinal(true);
        }
        else
        {
            renderToChange.material = matOff;
            gm.UpdateFinal(false);
        }
    }
}
