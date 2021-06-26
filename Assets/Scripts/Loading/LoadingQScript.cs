using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingQScript : MonoBehaviour
{
    [SerializeField] LoadingUI ui;
    private void Awake()
    {
        ui.loadingSlider.value = 0;
    }

    private void Start()
    {
        string materi = GameData.QuizMateri;
        Debug.Log(materi);

        //StartCoroutine(LevelQuiz(materi));
    }

    /*IEnumerator LevelQuiz(string quizMateri)
    {
        //Get Data in Persistent Path
        var pathCheck = Path.Combine(GameData.streamingXmlPath, GameData.xmlFile + quizMateri + ".xml");
        var newFilePath = Path.Combine(GameData.usedPersistPath, GameData.xmlFileUse + ".xml");

        WWW www = new WWW(pathCheck);
        yield return www;

        if (File.Exists(newFilePath))
        {
            File.Delete(newFilePath);
        }
        while (!www.isDone)
        {
            ui.loadingSlider.value = Mathf.Clamp01(www.progress / 0.9f);
            yield return null;
        }
        File.WriteAllBytes(newFilePath, www.bytes);
        SceneManager.LoadScene("QuizGame");
    }*/
}
