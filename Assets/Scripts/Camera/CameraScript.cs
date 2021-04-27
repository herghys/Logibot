using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] cameraPos;
    [SerializeField]
    private Camera mainCam;
    private Transform prevPos, nextPos;

    public int posIndex;
}
