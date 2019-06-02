using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        PlayerModel friend = getUserByNick(name);
        if (friend != null)
        {
            if (LoadSaveService.game.onlineModel.listFriends.Find(f => f.deviceId.Equals(friend.deviceId)) == null)
            {
                Task.Run(() =>
                {
                    friend.status = "Pending";
                    LoadSaveService.game.onlineModel.listFriends.Add(friend);

                    onlineModel = db.GetCollection<OnlineModel>("friends");
                    onlineModel.Save(LoadSaveService.game.onlineModel);

                    PlayerModel meModel = new PlayerModel();
                    meModel.name = LoadSaveService.game.playerModel.name;
                    meModel.maxScore = LoadSaveService.game.playerModel.maxScore;
                    meModel.token = LoadSaveService.game.playerModel.token;
                    meModel.deviceId = LoadSaveService.game.playerModel.deviceId;
                    meModel.Id = LoadSaveService.game.playerModel.Id;
                    meModel.status = "Request";
                    onlineModel = db.GetCollection<OnlineModel>("friends");
                    var where = new QueryDocument("deviceId", friend.deviceId);
                    OnlineModel friendOnlineModel = onlineModel.FindOne(where);
                    friendOnlineModel.listFriends.Add(meModel);
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

    public static void removeFriend(string name)
    {
        Task.Run(() =>
        {
            PlayerModel oldFriend = LoadSaveService.game.onlineModel.listFriends.Find(f => f.name.Equals(name));

            OnlineModel oldFriendOnlineModel = getFriends(oldFriend.deviceId);
            PlayerModel me = oldFriendOnlineModel.listFriends.Find(f3 => f3.deviceId.Equals(LoadSaveService.game.playerModel.deviceId));

            oldFriendOnlineModel.listFriends.Remove(me);

            onlineModel = db.GetCollection<OnlineModel>("friends");
            onlineModel.Save(oldFriendOnlineModel);

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

                LoadSaveService.game.onlineModel.listFriends.ForEach(f =>
                {
                    OnlineModel friendOnlineModel = getFriends(f.deviceId);
                    PlayerModel me = friendOnlineModel.listFriends.Find(f3 => f3.deviceId.Equals(LoadSaveService.game.playerModel.deviceId));

                    me.maxScore = LoadSaveService.game.playerModel.maxScore;
                    me.token = LoadSaveService.game.playerModel.token;

                    onlineModel = db.GetCollection<OnlineModel>("friends");
                    onlineModel.Save(friendOnlineModel);
                });
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

    public static void test()
    {
        Task.Run(() =>
        {
            playerModel = db.GetCollection<PlayerModel>("users");
            List<PlayerModel> allPlayers = playerModel.FindAll().ToList();
            allPlayers.ForEach(p =>
            {
                if (p.deviceId != SystemInfo.deviceUniqueIdentifier)
                {
                    if (LoadSaveService.game.onlineModel.listFriends.Find(f => f.deviceId.Equals(p.deviceId)) == null)
                    {
                        UnityEngine.Debug.Log("Agregar amigo -> " + p.name);
                        p.status = "OK";
                        LoadSaveService.game.onlineModel.listFriends.Add(p);

                        onlineModel = db.GetCollection<OnlineModel>("friends");
                        onlineModel.Save(LoadSaveService.game.onlineModel);

                        PlayerModel meModel = new PlayerModel();
                        meModel.name = LoadSaveService.game.playerModel.name;
                        meModel.maxScore = LoadSaveService.game.playerModel.maxScore;
                        meModel.token = LoadSaveService.game.playerModel.token;
                        meModel.deviceId = LoadSaveService.game.playerModel.deviceId;
                        meModel.Id = LoadSaveService.game.playerModel.Id;
                        meModel.status = "OK";

                        OnlineModel newOnlineModel = new OnlineModel();
                        newOnlineModel.deviceId = p.deviceId;
                        newOnlineModel.listFriends = new List<PlayerModel>();
                        newOnlineModel.listFriends.Add(meModel);

                        onlineModel = db.GetCollection<OnlineModel>("friends");
                        onlineModel.Save(newOnlineModel);
                    }
                }
            });
            LoadSaveService.savePlayerLocal();
        });
    }
}
