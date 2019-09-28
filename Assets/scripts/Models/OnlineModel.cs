using System.Collections.Generic;

[System.Serializable]
public class OnlineModel
{
    public string Id { get; set; }
    public string deviceId;
    public List<FriendModel> listFriends;
}