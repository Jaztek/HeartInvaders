using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

public class QueryMaster
{

    private static MongoClient client = new MongoClient("mongodb://userunity:userunity123@ds261096.mlab.com:61096/heart");
    private static MongoServer server = client.GetServer();
    private static MongoDatabase db = server.GetDatabase("heart");
    private static MongoCollection<PlayerModel> playerModel;
    private static MongoCollection<OnlineModel> onlineModel;

    private static bool isOnline()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            IAsyncResult result = socket.BeginConnect("ds261096.mlab.com", 61096, null, null);

            //Si en 2 segundos no se conecta da la conexión por fallida
            bool success = result.AsyncWaitHandle.WaitOne(2000, true);

            if (socket.Connected)
            {

                return true;
            }
            else
            {
                socket.Close();
                return false;
            }
        }
        catch (Exception e)
        {
            socket.Close();
            return false;
        }
    }

    public static void addFriend(string name)
    {
        PlayerModel friendPlayerModel = getUserByNick(name);
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

                    onlineModel = db.GetCollection<OnlineModel>("friends");
                    onlineModel.Save(LoadSaveService.game.onlineModel);

                    FriendModel me = new FriendModel();
                    me.deviceId = LoadSaveService.game.playerModel.deviceId;
                    me.status = "Request";

                    onlineModel = db.GetCollection<OnlineModel>("friends");
                    var where = new QueryDocument("deviceId", friend.deviceId);
                    OnlineModel friendOnlineModel = onlineModel.FindOne(where);
                    friendOnlineModel.listFriends.Add(me);
                    onlineModel.Save(friendOnlineModel);
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

            onlineModel = db.GetCollection<OnlineModel>("friends");
            onlineModel.Save(oldFriendOnlineModel);


            FriendModel oldFriend = LoadSaveService.game.onlineModel.listFriends.Find(f => f.deviceId.Equals(deviceId));
            LoadSaveService.game.onlineModel.listFriends.Remove(oldFriend);
            onlineModel.Save(LoadSaveService.game.onlineModel);
        });
    }

    public static OnlineModel getFriends(string deviceId)
    {
        if (!isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        onlineModel = db.GetCollection<OnlineModel>("friends");
        var where = new QueryDocument("deviceId", deviceId);
        return onlineModel.FindOne(where);
    }

    public static PlayerModel getUserByNick(string name)
    {
        if (!isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        playerModel = db.GetCollection<PlayerModel>("users");
        var where = new QueryDocument("name", name);
        return playerModel.FindOne(where);
    }

    public static PlayerModel getUserById(string deviceId)
    {
        if (!isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        playerModel = db.GetCollection<PlayerModel>("users");
        var where = new QueryDocument("deviceId", deviceId);
        return playerModel.FindOne(where);
    }

    public static List<PlayerModel> getUsersByIds(List<string> listDeviceId)
    {
        if (!isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        List<BsonValue> listBson = new List<BsonValue>();
        listDeviceId.ForEach(d => listBson.Add(BsonValue.Create(d)));

        playerModel = db.GetCollection<PlayerModel>("users");
        var where = Query.In("deviceId", listBson);
        return playerModel.Find(where).ToList();
    }

    public static void savePlayer()
    {
        Task.Run(() =>
        {
            if (!isOnline())
            {
                UnityEngine.Debug.LogError("Sin conexión a BBDD");
            }
            else
            {
                playerModel = db.GetCollection<PlayerModel>("users");
                playerModel.Save(LoadSaveService.game.playerModel);
            }
        });
    }

    public static PlayerModel LoadPlayer()
    {
        if (!isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        playerModel = db.GetCollection<PlayerModel>("users");

        var where = new QueryDocument("deviceId", SystemInfo.deviceUniqueIdentifier);
        return playerModel.FindOne(where);
    }
}
