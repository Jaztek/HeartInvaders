using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseController
{

    public FirebaseController()
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

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }

    public void sendMessageTo(string tokenPerdedor, string nombreGanador)
    {
        UnityEngine.Debug.Log("---------------------------sendingMessage----------------------------");
        UnityWebRequest request = new UnityWebRequest("https://fcm.googleapis.com/fcm/send", "POST");
        request.SetRequestHeader("Authorization", "key=" + "AIzaSyDoicdxWevoAwew0Lky4Uv5laDBmsJEQeY");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Cache-Control", "no-cache");

        NotificationModel notification = new NotificationModel();
        notification.title = nombreGanador + " te ha superado fuertemente 👀";
        notification.body = "Juega o seguirás siendo el último 🤡";

        FirebaseModel firebaseModel = new FirebaseModel();
        firebaseModel.to = tokenPerdedor;
        firebaseModel.notification = notification;

        string json = JsonUtility.ToJson(firebaseModel);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SendWebRequest();
        UnityEngine.Debug.Log("---------------------------sended----------------------------");
    }
}
