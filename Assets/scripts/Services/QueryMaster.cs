using System.Threading.Tasks;

using UnityEngine;
using Firebase;
using Firebase.Database;

using System;
using System.Threading;

public class QueryMaster
{
    private static int TIMEOUT_SECONDS = 30;

    public static void makeAsincToSinc(Task task){

        DateTime startTime = DateTime.Now;
        while (!task.IsCompleted)
        {
            // Check if we are waiting too long o
            if ((DateTime.Now - startTime).TotalSeconds > TIMEOUT_SECONDS)
            {
                throw new TimeoutException();
            }
            // Check if a cancelation has been requested
            //cancellationToken.ThrowIfCancellationRequested();

            // Wait a bit more
            Thread.Sleep(100);
        }
        
        ;
    }

/*

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
 */
}
