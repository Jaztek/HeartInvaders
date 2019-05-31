using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class FirebaseController
{
    public static void start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
        
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private static void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
        if (LoadSaveService.game.playerModel.token != token.Token)
        {
            LoadSaveService.game.playerModel.token = token.Token;
            QueryMaster.savePlayer();
        }

    }

    private static void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }

    public static void sendMessageTo(string tokenPerdedor, long score, string nombreGanador)
    {
        Task.Run(() => {
            UnityEngine.Debug.Log("---------------------------sendingMessage----------------------------");
            UnityWebRequest request = new UnityWebRequest("https://fcm.googleapis.com/fcm/send", "POST");
            request.SetRequestHeader("Authorization", "key=" + "AIzaSyDoicdxWevoAwew0Lky4Uv5laDBmsJEQeY");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Cache-Control", "no-cache");

            NotificationModel notification = new NotificationModel();
            notification.title = nombreGanador + " te ha superado con " + score + " puntos! 😯";
            notification.body = "Juega para superarle! 🕹";

            FirebaseModel firebaseModel = new FirebaseModel();
            firebaseModel.to = tokenPerdedor;
            firebaseModel.notification = notification;

            string json = JsonUtility.ToJson(firebaseModel);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SendWebRequest();
            UnityEngine.Debug.Log("---------------------------sended----------------------------");
        });
    }
}
