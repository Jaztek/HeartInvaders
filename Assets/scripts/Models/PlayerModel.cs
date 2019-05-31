using System.IO;
using UnityEngine;
using MongoDB.Bson;

[System.Serializable]
public class PlayerModel
{
    public ObjectId Id { get; set; }
    public string deviceId;
    public string token;
    public string name;
    public long maxScore;
    public string status;
}