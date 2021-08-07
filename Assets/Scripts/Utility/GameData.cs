using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.Networking;
using System;

public static class GameData
{
    public static string gameVersion;
    //Puzzle
    public static string previousScene;
    public static string levelToLoad;
    public static string nextScene;  
    public static bool tutorialFinished;
    public static int maxLevel = 10;

    //Quiz
    public const float ResolutionDelayTime = 1;

    public static string QuizMateri = "";
    public static string xmlFile = "Questions_Data_";
    public static string persistentXmlPath { get { return Application.persistentDataPath + "/QuestionData/"; }}

    public static string streamingXmlPath
    {
        get
        {
            return Application.streamingAssetsPath;
        }
    }
    //PlayerPrefs
    public static int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels");
    public static string QuizHichcoreSaveKey = "Quiz_Highscore_Value_";
}

[System.Serializable()]
public class DataXML
{
    public Question[] Questions = new Question[0];
    public DataXML() { }
    public static void Write(DataXML data, string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof (DataXML));
        using (Stream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
    }

    public static DataXML Fetch(string file)
    {
        return Fetch(out bool result, file);
    }

    public static DataXML Fetch(out bool result, string file)
    {
        var filepath = Path.Combine(GameData.persistentXmlPath, file);
        var streamPath = Path.Combine(GameData.streamingXmlPath, file);

        #if UNITY_EDITOR || UNITY_STANDALONE
        if (!File.Exists(file))
        {
            result = false;
            return new DataXML();
        }
        XmlSerializer deserializer = new XmlSerializer(typeof(DataXML));
        using (Stream stream = new FileStream(file, FileMode.Open))
        {
            var data = (DataXML)deserializer.Deserialize(stream);

            result = true;
            return data;
        }
        #else
        if (File.Exists(filepath))
        {
            WWW reader = new WWW(filepath);
            while (!reader.isDone) { }
            XmlSerializer deserializer = new XmlSerializer(typeof(DataXML));
            using (StringReader stream = new StringReader(reader.text))
            {
                var data = (DataXML)deserializer.Deserialize(stream);
                result = true;
                return data;
            }
        }
        else
        {
            WWW reader = new WWW(streamPath);        
            while (!reader.isDone) { }
            XmlSerializer deserializer = new XmlSerializer(typeof(DataXML));
            using (StringReader stream = new StringReader(reader.text))
            {
                var data = (DataXML)deserializer.Deserialize(stream);
                result = true;
                return data;
            }
        }
#endif

    }
}