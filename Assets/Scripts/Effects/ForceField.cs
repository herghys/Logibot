using System;
using System.Collections;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public MeshRenderer fieldRender;
    public AnimationCurve curve;
    public float shieldTime;

    private void Awake()
    {
        if (fieldRender == null)
        {
            fieldRender = GetComponent<MeshRenderer>();
        }
    }
}