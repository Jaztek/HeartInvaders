using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public static class StageService
{

    public static string getText()
    {
        try
        {
            string path = Application.streamingAssetsPath + "/stageParams.json";

            string jsonString = File.ReadAllText(path);

            JsonUtility.FromJson<StagesList>(jsonString);
            return "noice";

        }
        catch (System.Exception e)
        {
            return e.Message;
        }
    }

    public static StagesList getStages()
    {
        string filePath = Application.streamingAssetsPath + "/stageParams.json";
        string dataAsJson;
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityWebRequest reader = new UnityWebRequest (filePath);
            while (!reader.isDone)
            {
            }
            dataAsJson = reader.downloadHandler.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(filePath);
        }
        return JsonUtility.FromJson<StagesList>(dataAsJson);
    }

}