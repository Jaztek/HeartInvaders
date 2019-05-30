using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

public static class LoadSaveService
{
    public static GameModel game;

    private static void savePlayerLocal(GameModel gameModel)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, gameModel);
        game = gameModel;

        file.Close();     
    }

    private static void savePlayerRemote(GameModel gameModel)
    {
        savePlayerLocal(gameModel);
        QueryMaster.savePlayer(game.playerModel);
    }


        public static GameModel Load()
    {
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

                if (game.playerModel == null)
                {
                    game.playerModel = getPlayerModel();
                    savePlayerLocal(game);
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

        return game;
    }

    private static void createNewPlayer()
    {
        game = new GameModel();
        game.lifes = 10;
        game.bombs = 2;
        game.maxStage = 0;
        game.playerModel = getPlayerModel();
        savePlayerLocal(game);
    }

    private static PlayerModel getPlayerModel()
    {
        PlayerModel playerModel = QueryMaster.LoadPlayer(SystemInfo.deviceUniqueIdentifier);

        if (playerModel == null)
        {
            playerModel = new PlayerModel();
            playerModel.deviceId = SystemInfo.deviceUniqueIdentifier;
            //TODO: PEDIR NICKNAME
            playerModel.name = "Nombre: " + SystemInfo.deviceUniqueIdentifier;
            playerModel.maxScore = 0;
        }
        return playerModel;
    }

        public static GameModel saveCurrentGame(int lifesLost, long score, int stage, int bombs)
    {
        GameModel gameModel = new GameModel();
        gameModel.lifes = game.lifes - lifesLost;
        gameModel.maxStage = game.maxStage < stage ? stage : game.maxStage;
        gameModel.bombs = bombs;
        gameModel.playerModel = game.playerModel;

        if (gameModel.playerModel.maxScore < score)
        {
            gameModel.playerModel.maxScore = score;
            savePlayerRemote(gameModel);
        }
        else
        {
            savePlayerLocal(gameModel);
        }

        return game;
    }

    public static GameModel addLifes(int lifes)
    {
        game.lifes = lifes;
        savePlayerLocal(game);

        return game;
    }

    public static GameModel addBoms(int boms)
    {
        game.bombs = boms;
        savePlayerLocal(game);

        return game;
    }

}
