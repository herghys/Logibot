using UnityEngine;
using UnityEngine.SceneManagement;
public class Persist : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Intro");
    }
}