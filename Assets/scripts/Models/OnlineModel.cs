using MongoDB.Bson;
using System.Collections.Generic;

[System.Serializable]
public class OnlineModel
{
    public ObjectId Id { get; set; }
    public string deviceId;
    public List<FriendModel> listFriends;
}