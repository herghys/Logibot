using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.Networking;
using System;

public static class GameData
{
    //Puzzle
    public static string previousScene;
    public static string levelToLoad;
    public static string nextScene;  
    public static bool tutorialFinished;
    public static int maxLevel = 10;

    //QUiz
    public const float ResolutionDelayTime = 1;
    public static string QuizMateri;

    //QuizXML
    public static string xmlFile = "Questions_Data_";
    public static string xmlFilePath
    {
        get
        {
            return Application.persistentDataPath + "/Questions/";
        }
    }

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
        var filepath = Path.Combine(GameData.xmlFilePath, file);
        var streampath = Path.Combine(GameData.streamingXmlPath, file);

        #if UNITY_EDITOR //Unity Editor
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
#elif UNITY_STANDALONE //Unity Standalone (Mac Windows Linux)
        if (!File.Exists(streampath))
                {
                    result = false;
                    return new DataXML();
                }
                XmlSerializer deserializer = new XmlSerializer(typeof(DataXML));
                using (Stream stream = new FileStream(streampath, FileMode.Open))
                {
                    var data = (DataXML)deserializer.Deserialize(stream);

                    result = true;
                    return data;
                }
#else //Other Runtime
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
            WWW reader = new WWW(streampath);        
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