using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{
    public int tutorialContext;
    public TutorialManager tManager;

    private void Awake()
    {
        if (tManager == null)
            tManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<TutorialManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tManager.ToggleTutorial(tutorialContext);
            Destroy(gameObject);
        }
    }
}
