using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

public class QueryMaster{

    private static MongoClient client = new MongoClient("mongodb://userunity:userunity123@ds261096.mlab.com:61096/heart");
    private static MongoServer server = client.GetServer();
    private static MongoDatabase db = server.GetDatabase("heart");
    private static MongoCollection<PlayerModel> playerModel;

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
        }catch(Exception e)
        {
            socket.Close();
            return false;
        }
    }
    public static PlayerModel findFriend(string name)
    {
        //!isOnline()
        if (!isOnline()) {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        playerModel = db.GetCollection<PlayerModel>("users");
        var where =new QueryDocument("name", name);
        return playerModel.FindOne(where);
    }

    public static PlayerModel savePlayer(PlayerModel player)
    {
        if (!isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        playerModel = db.GetCollection<PlayerModel>("users");

        playerModel.Save(player);

        return player;
    }
}
