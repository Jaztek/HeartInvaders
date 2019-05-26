using System.Collections;
using System.Collections.Generic;

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

        Firebase.Messaging.FirebaseMessage message = new Firebase.Messaging.FirebaseMessage();
        message.To = token.Token + "";
        message.MessageId = "500";
        message.Data["hello"] = "world";
        Firebase.Messaging.FirebaseMessaging.Send(message);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }
}
