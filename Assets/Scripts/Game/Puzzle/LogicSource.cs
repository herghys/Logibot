using UnityEngine;

public class LogicSource : LogicalObject
{
    [Header("Manager")]
    public LogicManager logicManager;

    private void Awake()
    {
        if (logicManager == null) 
            logicManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LogicManager>();
    }

    private void Start()
    {
        ChangeRender();
        logicManager.UpdateLogic();
    }
    #region Collider
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.CompareTag("Player") && interactable)
        {
            ChangeSource();
            logicManager.UpdateLogic();
            ChangeRender();
        }
    }
    #endregion
}