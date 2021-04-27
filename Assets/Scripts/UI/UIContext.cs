using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContext : MonoBehaviour
{
    public GameObject self;

    public void ContextTarget(GameObject target)
    {
        target.SetActive(true);
        self.SetActive(false);
    }
}
