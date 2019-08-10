using UnityEngine;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
public class PlayerService
{
    public static PlayerModel getUserByNick(string name)
    {
        if (!QueryMaster.isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
        QueryMaster.playerModel.Find(user => user.name.Equals(name)).SingleOrDefault();
        return QueryMaster.playerModel.Find(user => user.name.Equals(name)).SingleOrDefault();
    }

    public static PlayerModel getUserById(string deviceId)
    {
        if (!QueryMaster.isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
        return QueryMaster.playerModel.Find(user => user.deviceId.Equals(deviceId)).SingleOrDefault();
    }

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

    public static void savePlayer()
    {
        Task.Run(() =>
        {
            if (!QueryMaster.isOnline())
            {
                UnityEngine.Debug.LogError("Sin conexión a BBDD");
            }
            else
            {
                QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
                QueryMaster.playerModel.InsertOne(LoadSaveService.game.playerModel);
            }
        });
    }

    public static PlayerModel LoadPlayer()
    {
        if (!QueryMaster.isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
        return QueryMaster.playerModel.Find(user => user.deviceId.Equals(SystemInfo.deviceUniqueIdentifier)).SingleOrDefault();
    }
}
