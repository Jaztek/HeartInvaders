
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


    public async static Task Load()
    {
        /*
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            File.Delete(Application.persistentDataPath + "/savedGames.gd");
        }
         */
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd")){
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
                GameModel gameModel = (GameModel)bf.Deserialize(file);
                file.Close();
                file.Dispose();

                game = gameModel;

                if (game.playerModel == null) {
                    Task<PlayerModel> taskPlayer = getPlayerModel();
                    await taskPlayer;
                    game.playerModel = taskPlayer.Result;
                }

                Task<OnlineModel> taskOnlineModel = getOnlineModel();
                await taskOnlineModel;
                game.onlineModel = taskOnlineModel.Result;
                savePlayerLocal();

                UnityEngine.Debug.Log(gameModel.dateLastLife);
            }
            catch (System.Exception)
            {
                await createNewPlayer();
                throw;
            }
        }
        else {
            Debug.Log("crea player");
            await createNewPlayer();
        }
    }

    private async static Task createNewPlayer()
    {
        game = new GameModel();
        game.lifes = 10;
        game.bombs = 2;
        game.maxStage = 0;

        game.nextStage = 0;
        game.isMute = false;
        game.dateLastLife = DateTime.Now;

        Debug.Log("createNewPlayer");
        Task<PlayerModel> taskPlayer = PlayerService.LoadPlayer(SystemInfo.deviceUniqueIdentifier);
        await taskPlayer;

        Debug.Log("createNewPlayer" +taskPlayer.Result);
        game.playerModel = taskPlayer.Result;

        Task<OnlineModel> taskOnlineModel = getOnlineModel();
        await taskOnlineModel;
        game.onlineModel = taskOnlineModel.Result;

        savePlayerLocal();
    }

    private static Task<PlayerModel> getPlayerModel()
    {
        Task<PlayerModel> taskPlayer = PlayerService.LoadPlayer(SystemInfo.deviceUniqueIdentifier);
        return taskPlayer;
    }

    public static Task<OnlineModel> getOnlineModel()
    {
        Task<OnlineModel> taskOnlineModel = FriendService.getFriends(SystemInfo.deviceUniqueIdentifier);
        return taskOnlineModel;

    }

    public static void saveCurrentGame(int lifesLost, long score, int stage, int bombs, int nextStage)
    {
        game.lifes = game.lifes - lifesLost;
        game.nextStage = nextStage;
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
                        /*
                        PlayerModel friend = PlayerService.getUserById(f.deviceId);
                        if(friend.maxScore > maxScore && friend.maxScore < score)
                        {
                            FirebaseController.sendMessageScoreTo(friend.token, score, game.playerModel.name);
                        }
                         */
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

    public static void saveNick(string nick)
    {

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
