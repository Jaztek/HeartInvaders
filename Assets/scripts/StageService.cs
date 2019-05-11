using System.IO;
using UnityEngine;

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
            WWW reader = new WWW(filePath);
            while (!reader.isDone)
            {
            }
            dataAsJson = reader.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(filePath);
        }
        return JsonUtility.FromJson<StagesList>(dataAsJson);
    }

}