using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Firebase;
using Firebase.Unity.Editor;

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

        // inicializamos la base de datos de firebase.
        Debug.Log("firebaseInitialized  firebaseInitialized");
        //FirebaseApp app = FirebaseApp.DefaultInstance;


        Firebase.AppOptions ops = new Firebase.AppOptions();
        CommonData.app = Firebase.FirebaseApp.Create(ops);

        // Setup database url when running in the editor 
        #if UNITY_EDITOR
                if (CommonData.app.Options.DatabaseUrl == null)
                {
                    Debug.Log("https://heart-beat-defender.firebaseio.com/");
                    CommonData.app.SetEditorDatabaseUrl("https://heart-beat-defender.firebaseio.com/");
                }
        #endif


    }

    private static void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        if (LoadSaveService.game.playerModel.token != token.Token)
        {
            LoadSaveService.game.playerModel.token = token.Token;
        }

    }

    private static void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Message received");
        //LoadSaveService.game.onlineModel = FriendService.getFriends(LoadSaveService.game.onlineModel.deviceId);
    }

    public static void sendMessageScoreTo(string tokenPerdedor, long score, string nombreGanador)
    {
        Task.Run(() =>
        {
            UnityWebRequest request = new UnityWebRequest("https://fcm.googleapis.com/fcm/send", "POST");
            request.SetRequestHeader("Authorization", "key=" + "AIzaSyDoicdxWevoAwew0Lky4Uv5laDBmsJEQeY");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Cache-Control", "no-cache");

            NotificationModel notification = new NotificationModel();
            notification.title = nombreGanador + " has overtaken you with " + score + " points! 😯";
            notification.body = "Play to beat him! 🕹";

            FirebaseModel firebaseModel = new FirebaseModel();
            firebaseModel.to = tokenPerdedor;
            firebaseModel.notification = notification;

            string json = JsonUtility.ToJson(firebaseModel);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SendWebRequest();
            UnityEngine.Debug.Log("Message sended to " + tokenPerdedor);
        });
    }

    public static void sendMessageRequestFriend(string tokenAmigo)
    {
        Task.Run(() =>
        {
            UnityWebRequest request = new UnityWebRequest("https://fcm.googleapis.com/fcm/send", "POST");
            request.SetRequestHeader("Authorization", "key=" + "AIzaSyDoicdxWevoAwew0Lky4Uv5laDBmsJEQeY");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Cache-Control", "no-cache");

            NotificationModel notification = new NotificationModel();
            notification.title = LoadSaveService.game.playerModel.name + " sent you a friend request 😃";
            notification.body = "Click to see it!";

            FirebaseModel firebaseModel = new FirebaseModel();
            firebaseModel.to = tokenAmigo;
            firebaseModel.notification = notification;

            string json = JsonUtility.ToJson(firebaseModel);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SendWebRequest();
            UnityEngine.Debug.Log("Message sended to " + tokenAmigo);
        });
    }

    public static void sendMessageRejectedFriend(string tokenAmigo)
    {
        Task.Run(() =>
        {
            UnityWebRequest request = new UnityWebRequest("https://fcm.googleapis.com/fcm/send", "POST");
            request.SetRequestHeader("Authorization", "key=" + "AIzaSyDoicdxWevoAwew0Lky4Uv5laDBmsJEQeY");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Cache-Control", "no-cache");

            NotificationModel notification = new NotificationModel();
            notification.title = LoadSaveService.game.playerModel.name + " has rejected your friend request 😔";
            notification.body = "Click to see it!";

            FirebaseModel firebaseModel = new FirebaseModel();
            firebaseModel.to = tokenAmigo;
            firebaseModel.notification = notification;

            string json = JsonUtility.ToJson(firebaseModel);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SendWebRequest();
            UnityEngine.Debug.Log("Message sended to " + tokenAmigo);
        });
    }

    public static void sendMessageAcceptFriend(string tokenAmigo)
    {
        Task.Run(() =>
        {
            UnityWebRequest request = new UnityWebRequest("https://fcm.googleapis.com/fcm/send", "POST");
            request.SetRequestHeader("Authorization", "key=" + "AIzaSyDoicdxWevoAwew0Lky4Uv5laDBmsJEQeY");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Cache-Control", "no-cache");

            NotificationModel notification = new NotificationModel();
            notification.title = LoadSaveService.game.playerModel.name + " has accepted your friend request 😃";
            notification.body = "Click to see it!";

            FirebaseModel firebaseModel = new FirebaseModel();
            firebaseModel.to = tokenAmigo;
            firebaseModel.notification = notification;

            string json = JsonUtility.ToJson(firebaseModel);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SendWebRequest();
            UnityEngine.Debug.Log("Message sended to " + tokenAmigo);
        });
    }
}
