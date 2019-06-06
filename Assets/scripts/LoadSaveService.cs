using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;
using System.Threading.Tasks;

public static class LoadSaveService
{
    public static GameModel game;

    public static void savePlayerLocal()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/savedGames.gd",
                                       FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
        bf.Serialize(file, game);

        file.Close();
        file.Dispose();
    }

    private static void savePlayerRemote()
    {
        savePlayerLocal();
        PlayerService.savePlayer();
    }

    private static void saveGameRemote()
    {
        savePlayerLocal();
        PlayerService.savePlayer();
        FriendService.saveFriends();
    }


    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            File.Delete(Application.persistentDataPath + "/savedGames.gd");
        }
        /*
         */
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
                GameModel gameModel = (GameModel)bf.Deserialize(file);
                file.Close();
                file.Dispose();

                game = gameModel;

                bool isChanged = false;
                if (game.playerModel == null)
                {
                    UnityEngine.Debug.Log("Juego guardado antiguo... se necesita PlayerModel");
                    game.playerModel = getPlayerModel();
                    isChanged = true;
                }
                if (game.onlineModel == null)
                {

                    UnityEngine.Debug.Log("Juego guardado antiguo... se necesita OnlineModel");
                    game.onlineModel = getOnlineModel();
                    isChanged = true;
                }

                if (isChanged)
                {
                    savePlayerLocal();
                }
            }
            catch (System.Exception)
            {
                createNewPlayer();
                throw;
            }
        }
        else
        {
            createNewPlayer();
        }
    }

    private static void createNewPlayer()
    {
        game = new GameModel();
        game.lifes = 10;
        game.bombs = 2;
        game.maxStage = 0;
        game.playerModel = getPlayerModel();
        game.onlineModel = getOnlineModel();
        savePlayerLocal();
    }

    private static PlayerModel getPlayerModel()
    {
        PlayerModel playerModel = PlayerService.LoadPlayer();

        if (playerModel == null)
        {
            playerModel = new PlayerModel();
            playerModel.deviceId = SystemInfo.deviceUniqueIdentifier;
            playerModel.maxScore = 0;
        }
        return playerModel;
    }

    public static OnlineModel getOnlineModel()
    {
        OnlineModel onlineModel = FriendService.getFriends(SystemInfo.deviceUniqueIdentifier);

        if (onlineModel == null)
        {
            onlineModel = new OnlineModel();
            onlineModel.deviceId = SystemInfo.deviceUniqueIdentifier;
            onlineModel.listFriends = new List<FriendModel>();
        }
        return onlineModel;
    }

    public static void saveCurrentGame(int lifesLost, long score, int stage, int bombs)
    {
        game.lifes = game.lifes - lifesLost;
        game.maxStage = game.maxStage < stage ? stage : game.maxStage;
        game.bombs = bombs;

        long maxScore = game.playerModel.maxScore;
        if (maxScore < score)
        {
            Task.Run(() =>
            {
                game.onlineModel.listFriends.ForEach(f =>
                {
                    if (f.status != "Pending")
                    {
                        PlayerModel friend = PlayerService.getUserById(f.deviceId);
                        if(friend.maxScore > maxScore && friend.maxScore < score)
                        {
                            FirebaseController.sendMessageScoreTo(friend.token, score, game.playerModel.name);
                        }
                    }
                });
            });
            game.playerModel.maxScore = score;
            savePlayerRemote();
        }
        else
        {
            savePlayerLocal();
        }
    }

    public static void saveNick(string nick){

        game.playerModel.name = nick;
        LoadSaveService.saveGameRemote();
    }

    public static void addLifes(int lifes)
    {
        game.lifes = lifes;
        savePlayerLocal();
    }

    public static void addBoms(int boms)
    {
        game.bombs = boms;
        savePlayerLocal();
    }

}
