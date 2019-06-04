using System.IO;
using UnityEngine;
using MongoDB.Bson;

[System.Serializable]
public class FriendModel
{
    public ObjectId Id { get; set; }
    public string deviceId;
    public string status;
}