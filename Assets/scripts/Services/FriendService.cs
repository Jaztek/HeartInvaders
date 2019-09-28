
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public class FriendService
{
    public const string FriendsTablePath = "Friends/";


    public static void saveFriends()
    {
        Task.Run(() =>
        {
            Debug.Log(LoadSaveService.game.playerModel.name);
            PlayerModel player = LoadSaveService.game.playerModel;

            string friendPath = FriendsTablePath + player.deviceId;
            DBStruct<OnlineModel> dbFriends = new DBStruct<OnlineModel>(friendPath, CommonData.app);
            dbFriends.Initialize(LoadSaveService.game.onlineModel);
            dbFriends.PushData();
        });
    }

    public static Task getFriends(string deviceId)
    {
        OnlineModel onlineModelFriend = null;
        return FirebaseDatabase.DefaultInstance.RootReference.Child("Friends").OrderByChild("deviceId").EqualTo(deviceId)
               .GetValueAsync().ContinueWith(taski =>
               {
                   if (taski.IsFaulted)
                   {
                       // Handle the error...
                   }
                   if (taski.IsCompleted)
                   {
                       DataSnapshot snapshot = taski.Result;
                       Debug.Log("busqueda ->");

                       foreach (DataSnapshot children in snapshot.Children)
                       {
                           Debug.Log(children.GetRawJsonValue());
                           onlineModelFriend = JsonUtility.FromJson<OnlineModel>(children.GetRawJsonValue());
                       }
                       /*
                       if (onlineModelFriend != null)
                       {
                           onlineModelFriend.listFriends.Add(friend);

                           DBStruct<OnlineModel> dbFriendFriend = new DBStruct<OnlineModel>(friendFrindPath, CommonData.app);
                           string friendFrindPath = FriendsTablePath + friendPlayerModel.deviceId;
                           dbFriend.Initialize(onlineModelFriend);
                           dbFriend.PushData();
                       }
                        */
                   };
                   return onlineModelFriend;
               });
    }
    /*

public static void requestFriend(string name)
{
    PlayerModel friendPlayerModel = PlayerService.getPlayerByNick(name);
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
                //LoadSaveService.game.onlineModel

                string friendPath = FriendsTablePath + LoadSaveService.game.playerModel.deviceId;
                DBStruct<OnlineModel> dbFriend = new DBStruct<OnlineModel>(friendPath, CommonData.app);
                dbFriend.Initialize(LoadSaveService.game.onlineModel);
                dbFriend.PushData();


                FriendModel me = new FriendModel();
                me.deviceId = LoadSaveService.game.playerModel.deviceId;
                me.status = "Request";

                //List<PlayerModel> users = new List<PlayerModel>();
                FirebaseDatabase.DefaultInstance.RootReference.Child("Friends").OrderByChild("deviceId").EqualTo(friendPlayerModel.deviceId)
                .GetValueAsync().ContinueWith(taski =>
                {
                    if (taski.IsFaulted)
                    {
                        // Handle the error...
                    }
                    if (taski.IsCompleted)
                    {
                        DataSnapshot snapshot = taski.Result;
                        Debug.Log("busqueda ->");

                        OnlineModel onlineModelFriend = null;
                        foreach (DataSnapshot children in snapshot.Children)
                        {
                            Debug.Log(children.GetRawJsonValue());
                            onlineModelFriend = JsonUtility.FromJson<OnlineModel>(children.GetRawJsonValue());
                        }
                        if (onlineModelFriend != null)
                        {
                            onlineModelFriend.listFriends.Add(friend);

                            string friendFrindPath = FriendsTablePath + friendPlayerModel.deviceId;
                            DBStruct<OnlineModel> dbFriendFriend = new DBStruct<OnlineModel>(friendFrindPath, CommonData.app);
                            dbFriend.Initialize(onlineModelFriend);
                            dbFriend.PushData();
                        }
                    };

                });


                /*
                    string friendFrindPath = FriendsTablePath + friendPlayerModel.deviceId;
                    DBStruct<OnlineModel> dbFriendFriend = new DBStruct<OnlineModel>(friendFrindPath, CommonData.app);
                    dbFriend.Initialize(LoadSaveService.game.onlineModel);
                    dbFriend.PushData();

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
 */
    /*

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
        */


    /*
    if (!QueryMaster.isOnline())
    {
        UnityEngine.Debug.LogError("Sin conexión a BBDD");
        return null;
    }
    QueryMaster.onlineModel = QueryMaster.db.GetCollection<OnlineModel>("friends");
    return QueryMaster.onlineModel.Find(user => user.deviceId.Equals(deviceId)).SingleOrDefault();
}

     */

}
