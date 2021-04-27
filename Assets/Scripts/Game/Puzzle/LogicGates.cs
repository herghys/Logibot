using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum LogicGateType { Buffer, AND, OR, NOT, NAND, NOR, XOR, XNOR }
public class LogicGates : LogicalObject
{
    [Header("LogicGate")]
    public LogicGateType type;
    public LogicalObject[] inputs;
    public bool[] inputsBool;

    private void Awake()
    {
        inputsBool = new bool[inputs.Length];
        LogicGate();
    }

    private void Start()
    {
        ChangeRender();
    }

    public void LogicGate()
    {
        for (int i = 0; i < inputsBool.Length; i++)
        {
            inputsBool[i] = inputs[i].outputValue;
        }
        switch (type)
        {
            case LogicGateType.Buffer:
                //outputValue = inputs[0].outputValue;
                outputValue = ANDLogic();
                break;
            case LogicGateType.AND:
                outputValue = ANDLogic();
                break;
            case LogicGateType.OR:
                outputValue = ORLogic();
                break;
            case LogicGateType.NOT:
                outputValue = !inputs[0].outputValue;
                break;
            case LogicGateType.NAND:
                outputValue = !ANDLogic();
                break;
            case LogicGateType.NOR:
                outputValue = !ORLogic();
                break;
            case LogicGateType.XOR:
                outputValue = XORLogic();
                break;
            case LogicGateType.XNOR:
                outputValue = !XORLogic();
                break;
        }
        outputByte = Convert.ToByte(outputValue);
    }
    private bool ORLogic()
    {
        // return inputsBool.Contains(true);
        return inputsBool.Any(bools => bools == true);
        //name => name.Length > 5
/*        if (inputsBool.Any(bools => bools = true))
        {
            return true;
        }
        else return false;*/
    }

    private bool ANDLogic()
    {
        if (inputsBool.All(bools => bools == true))
        {
            return true;
        }
        else return false;
    }

    private bool XORLogic()
    {
        if (inputsBool.All(bools => bools == true) || inputsBool.All(bools => bools == false))
        {
            return false;
        }
        else return true;
    }

    public override void ChangeRender()
    {
        if (type == LogicGateType.Buffer)
        {
            if (outputValue) renderToChange.material = matOn;
            else renderToChange.material = matOff;
        }
        else base.ChangeRender();
    }
}
