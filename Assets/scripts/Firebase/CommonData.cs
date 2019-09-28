using System.Collections.Generic;
using UnityEngine;

  class CommonData {
 
    public static Firebase.FirebaseApp app;
 
    public static DBStruct<PlayerModel> currentUser;
    public const string UsersTablePath = "Users/";
    public const string FriendTablePath = "Friends/";

  }
