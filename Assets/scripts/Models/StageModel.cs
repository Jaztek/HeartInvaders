using System.IO;
using UnityEngine;

[System.Serializable]
public class StagesList
{
    public StageModel[] stagesList;
}

[System.Serializable]
public class StageModel
{
    public int stage;
    public float fireRate;
    public long maxScore;
    public string music;
    public BulletModel[] bullets;
}

[System.Serializable]
public class BulletModel
{
    public string path;
    public int probabilidad;
}