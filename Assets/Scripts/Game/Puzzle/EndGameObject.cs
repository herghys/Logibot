using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameObject : MonoBehaviour
{
    public GameManager manager;

    private void Awake()
    {
        if (manager == null)
            manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("End");
            manager.EndGame();
        }
    }
}
