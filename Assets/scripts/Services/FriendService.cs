using MongoDB.Driver;
using System.Threading.Tasks;
public class FriendService
{
    public static void requestFriend(string name)
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
                    QueryMaster.onlineModel.InsertOne(LoadSaveService.game.onlineModel);

                    FriendModel me = new FriendModel();
                    me.deviceId = LoadSaveService.game.playerModel.deviceId;
                    me.status = "Request";

                    QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
                    OnlineModel friendOnlineModel = QueryMaster.onlineModel.Find(user => user.deviceId.Equals(friend.deviceId)).SingleOrDefault();
                    friendOnlineModel.listFriends.Add(me);
                    QueryMaster.onlineModel.InsertOne(friendOnlineModel);

                    FirebaseController.sendMessageRequestFriend(friendPlayerModel.token);
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

    public static void rejectedFriend(string deviceId)
    {
        Task.Run(() =>
        {

            OnlineModel oldFriendOnlineModel = getFriends(deviceId);
            FriendModel me = oldFriendOnlineModel.listFriends.Find(f3 => f3.deviceId.Equals(LoadSaveService.game.playerModel.deviceId));

            oldFriendOnlineModel.listFriends.Remove(me);

            QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
            QueryMaster.onlineModel.InsertOne(oldFriendOnlineModel);


            FriendModel oldFriend = LoadSaveService.game.onlineModel.listFriends.Find(f => f.deviceId.Equals(deviceId));
            LoadSaveService.game.onlineModel.listFriends.Remove(oldFriend);
            QueryMaster.onlineModel.InsertOne(LoadSaveService.game.onlineModel);

            PlayerModel oldFriendPlayerModel = PlayerService.getUserById(oldFriend.deviceId);
            FirebaseController.sendMessageRejectedFriend(oldFriendPlayerModel.token);
        });
    }

    public static void acceptFriend(string deviceId)
    {
        Task.Run(() =>
        {

            OnlineModel newFriendOnlineModel = getFriends(deviceId);
            FriendModel me = newFriendOnlineModel.listFriends.Find(f3 => f3.deviceId.Equals(LoadSaveService.game.playerModel.deviceId));

            newFriendOnlineModel.listFriends.Remove(me);

            me.status = "OK";
            newFriendOnlineModel.listFriends.Add(me);

            QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
            QueryMaster.onlineModel.InsertOne(newFriendOnlineModel);


            FriendModel newFriend = LoadSaveService.game.onlineModel.listFriends.Find(f => f.deviceId.Equals(deviceId));
            LoadSaveService.game.onlineModel.listFriends.Remove(newFriend);

            newFriend.status = "OK";
            LoadSaveService.game.onlineModel.listFriends.Add(newFriend);

            QueryMaster.onlineModel.InsertOne(LoadSaveService.game.onlineModel);

            PlayerModel newFriendPlayerModel = PlayerService.getUserById(newFriend.deviceId);
            FirebaseController.sendMessageAcceptFriend(newFriendPlayerModel.token);
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
        return QueryMaster.onlineModel.Find(user => user.deviceId.Equals(deviceId)).SingleOrDefault();
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
                QueryMaster.onlineModel.InsertOne(LoadSaveService.game.onlineModel);
            }
        });
    }
}
