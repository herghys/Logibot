using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollowCamera : MonoBehaviour
{
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Vector3 target = new Vector3(player.transform.position.x, 
            transform.position.y, 
            player.transform.position.z);
        transform.LookAt(target);
    }
}
