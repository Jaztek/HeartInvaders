using MongoDB.Driver;
using System;
using System.Net.Sockets;
public class QueryMaster
{

    private static MongoClient client = new MongoClient("mongodb://userunity:userunity123@ds261096.mlab.com:61096/heart");
    public static IMongoDatabase db = client.GetDatabase("heart");
    public static IMongoCollection<PlayerModel> playerModel;
    public static IMongoCollection<OnlineModel> onlineModel;

    public static bool isOnline()
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
            UnityEngine.Debug.LogError(e.Message);
            socket.Close();
            return false;
        }
    }
}
