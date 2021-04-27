using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractingCollider : MonoBehaviour
{
    public string warningText;
    public GameObject warningObject;
    public TMP_Text warning;

    private void ToggleWarning(bool warningStatus)
    {
        warning.text = warningText;
        warningObject.SetActive(warningStatus);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
        {
            Debug.Log(collision.collider.name);
            ToggleWarning(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleWarning(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleWarning(false);
        }
    }
}
