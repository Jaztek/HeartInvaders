using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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
        var where = new QueryDocument("name", name);
        return QueryMaster.playerModel.FindOne(where);
    }

    public static PlayerModel getUserById(string deviceId)
    {
        if (!QueryMaster.isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
        var where = new QueryDocument("deviceId", deviceId);
        return QueryMaster.playerModel.FindOne(where);
    }

    public static List<PlayerModel> getUsersByIds(List<string> listDeviceId)
    {
        if (!QueryMaster.isOnline())
        {
            UnityEngine.Debug.LogError("Sin conexión a BBDD");
            return null;
        }
        List<BsonValue> listBson = new List<BsonValue>();
        listDeviceId.ForEach(d => listBson.Add(BsonValue.Create(d)));

        QueryMaster.playerModel = QueryMaster.db.GetCollection<PlayerModel>("users");
        var where = Query.In("deviceId", listBson);
        return QueryMaster.playerModel.Find(where).ToList();
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
                QueryMaster.playerModel.Save(LoadSaveService.game.playerModel);
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

        var where = new QueryDocument("deviceId", SystemInfo.deviceUniqueIdentifier);
        return QueryMaster.playerModel.FindOne(where);
    }
}
