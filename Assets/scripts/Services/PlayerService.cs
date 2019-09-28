
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Database;


public class PlayerService
{
    public static DBStruct<PlayerModel> playerTable;
    public const string UsersTablePath = "Users/";



    public static void savePlayer()
    {
        Task.Run(() =>
        {
            Debug.Log(LoadSaveService.game.playerModel.name);
            PlayerModel player = LoadSaveService.game.playerModel;

            string usersPath = UsersTablePath + player.deviceId;
            DBStruct<PlayerModel> dbPlayer = new DBStruct<PlayerModel>(usersPath, CommonData.app);
            dbPlayer.Initialize(player);
            dbPlayer.PushData();
        });

    }

    public static Task getPlayerByNickTask(string name)
    {
        PlayerModel user = null;
        return FirebaseDatabase.DefaultInstance.RootReference.Child("Users").OrderByChild("name").EqualTo(name)
        .GetValueAsync().ContinueWith(taski =>
        {
            if (taski.IsFaulted)
            {
                // Handle the error...
            }
            if (taski.IsCompleted)
            {
                DataSnapshot snapshot = taski.Result;
                Debug.Log("busqueda ->");

                foreach (DataSnapshot children in snapshot.Children)
                {
                    user = JsonUtility.FromJson<PlayerModel>(children.GetRawJsonValue());
                }
            };
            return user;
        });
    }

    // public static PlayerModel getUserById(string deviceId)
    //  {
    // QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
    // return QueryMaster.playerModel.Find(user => user.deviceId.Equals(deviceId)).SingleOrDefault();
    //  }
    /*

       public static List<PlayerModel> getUsersByIds(List<string> listDeviceId)
       {
           if (!QueryMaster.isOnline())
           {
               UnityEngine.Debug.LogError("Sin conexión a BBDD");
               return null;
           }

           QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
           return QueryMaster.playerModel.Find(user => listDeviceId.Contains(user.deviceId)).ToList();
       }

      

    */
    public static Task<PlayerModel> LoadPlayer()
    {
        // QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
        // return QueryMaster.playerModel.Find(user => user.deviceId.Equals(SystemInfo.deviceUniqueIdentifier)).SingleOrDefault();
        PlayerModel user = null;
        return FirebaseDatabase.DefaultInstance.RootReference.Child("Users").OrderByChild("deviceId").EqualTo(SystemInfo.deviceUniqueIdentifier)
        .GetValueAsync().ContinueWith(taski =>
        {
            if (taski.IsFaulted)
            {
                Debug.Log("fasho");
            }
            if (taski.IsCompleted)
            {
                DataSnapshot snapshot = taski.Result;
                foreach (DataSnapshot children in snapshot.Children)
                {
                    user = JsonUtility.FromJson<PlayerModel>(children.GetRawJsonValue());
                }
                if (user == null)
                {
                    user = new PlayerModel();
                    user.deviceId = SystemInfo.deviceUniqueIdentifier;
                    user.maxScore = 0;
                }
                Debug.Log(user.name);
            };
            return user;
        });
    }
}

