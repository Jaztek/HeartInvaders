using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FirebaseModel
{
    public string to;
    public NotificationModel notification;

}

[Serializable]
public class NotificationModel
{
    public string title;
    public string body;
}
