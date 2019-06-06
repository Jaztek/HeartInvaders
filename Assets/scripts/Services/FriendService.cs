using MongoDB.Driver;
using System.Threading.Tasks;
public class FriendService
{
    public static void addFriend(string name)
    {
        PlayerModel friendPlayerModel = PlayerService.getUserByNick(name);
        if (friendPlayerModel != null)
        {
            if (LoadSaveService.game.onlineModel.listFriends.Find(f => f.deviceId.Equals(friendPlayerModel.deviceId)) == null)
            {
                Task.Run(() =>
                {
                    FriendModel friend = new FriendModel();
                    friend.deviceId = friendPlayerModel.deviceId;
                    friend.status = "Pending";
                    LoadSaveService.game.onlineModel.listFriends.Add(friend);

                    QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
                    QueryMaster.onlineModel.Save(LoadSaveService.game.onlineModel);

                    FriendModel me = new FriendModel();
                    me.deviceId = LoadSaveService.game.playerModel.deviceId;
                    me.status = "Request";

                    QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
                    var where = new QueryDocument("deviceId", friend.deviceId);
                    OnlineModel friendOnlineModel = QueryMaster.onlineModel.FindOne(where);
                    friendOnlineModel.listFriends.Add(me);
                    QueryMaster.onlineModel.Save(friendOnlineModel);

                    FirebaseController.sendMessageFriendTo(friendPlayerModel.token);
                });
            }
            else
            {
                UnityEngine.Debug.Log(name + " ya es tu amigo");
            }
        }
        else
        {
            UnityEngine.Debug.Log("No existe el jugador " + name);
        }
    }

    public static void removeFriend(string deviceId)
    {
        Task.Run(() =>
        {

            OnlineModel oldFriendOnlineModel = getFriends(deviceId);
            FriendModel me = oldFriendOnlineModel.listFriends.Find(f3 => f3.deviceId.Equals(LoadSaveService.game.playerModel.deviceId));

            oldFriendOnlineModel.listFriends.Remove(me);

            QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
            QueryMaster.onlineModel.Save(oldFriendOnlineModel);


            FriendModel oldFriend = LoadSaveService.game.onlineModel.listFriends.Find(f => f.deviceId.Equals(deviceId));
            LoadSaveService.game.onlineModel.listFriends.Remove(oldFriend);
            QueryMaster.onlineModel.Save(LoadSaveService.game.onlineModel);
        });
    }

    public static OnlineModel getFriends(string deviceId)
    {
        if (!QueryMaster.isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
        var where = new QueryDocument("deviceId", deviceId);
        return QueryMaster.onlineModel.FindOne(where);
    }
    public static void saveFriends()
    {
        Task.Run(() =>
        {
            if (!QueryMaster.isOnline())
            {
                UnityEngine.Debug.LogError("Sin conexión a BBDD");
            }
            else
            {
                QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
                QueryMaster.onlineModel.Save(LoadSaveService.game.onlineModel);
            }
        });
    }
}
