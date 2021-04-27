using UnityEngine;
public class LogicManager : MonoBehaviour
{
    [SerializeField] private LogicGates[] logicGates;
    [SerializeField] private GameObject[] gateObjects;
    [SerializeField] private LogicEnd logicEnd;

    //public GameManager manager;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        gateObjects = GameObject.FindGameObjectsWithTag("LogicGates");
        if (gateObjects.Length != logicGates.Length)
        {
            logicGates = new LogicGates[gateObjects.Length];
            for (int i = 0; i < gateObjects.Length; i++)
            {
                logicGates[i] = gateObjects[i].GetComponent<LogicGates>();
            }
        }
    }

    public void UpdateLogic()
    {
        foreach (var item in logicGates)
        {
            item.LogicGate();
            item.ChangeRender();
        }
        logicEnd.UpdateEnd();
    }
}