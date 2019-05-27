using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

public static class LoadSaveService
{
    public static GameModel game;

    private static void savePlayerTemp(GameModel gameModel)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, gameModel);
        game = gameModel;

        file.Close();

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
                    PlayerModel playerModel = QueryMaster.LoadPlayer(SystemInfo.deviceUniqueIdentifier);

                    if (playerModel == null)
                    {
                        playerModel = new PlayerModel();
                        playerModel.deviceId = SystemInfo.deviceUniqueIdentifier;
                        //TODO: PEDIR NICKNAME
                        playerModel.name = "Nombre: " + SystemInfo.deviceUniqueIdentifier;
                        playerModel.maxScore = 0;
                    }
                    game.playerModel = playerModel;
                    savePlayerTemp(game);
                }
            }
            catch (System.Exception)
            {
                //print("no ha ido bien lo de cargar");
                game = new GameModel();
                game.lifes = 10;
                game.bombs = 2;
                game.maxStage = 0;

                PlayerModel playerModel = QueryMaster.LoadPlayer(SystemInfo.deviceUniqueIdentifier);

                if (playerModel == null)
                {
                    playerModel = new PlayerModel();
                    playerModel.deviceId = SystemInfo.deviceUniqueIdentifier;
                    //TODO: PEDIR NICKNAME
                    playerModel.name = "Nombre: " + SystemInfo.deviceUniqueIdentifier;
                    playerModel.maxScore = 0;
                }
                game.playerModel = playerModel;
                savePlayerTemp(game);
                throw;
            }



        }
        else
        {
            game = new GameModel();
            game.lifes = 10;
            game.bombs = 2;
            game.maxStage = 0;

            PlayerModel playerModel = QueryMaster.LoadPlayer(SystemInfo.deviceUniqueIdentifier);

            if (playerModel == null)
            {
                playerModel = new PlayerModel();
                playerModel.deviceId = SystemInfo.deviceUniqueIdentifier;
                //TODO: PEDIR NICKNAME
                playerModel.name = "Nombre: " + SystemInfo.deviceUniqueIdentifier;
                playerModel.maxScore = 0;
            }
            game.playerModel = playerModel;
            savePlayerTemp(game);
        }

        return game;
    }

    public static GameModel saveCurrentGame(int lifesLost, long score, int stage, int bombs)
    {
        GameModel gameModel = new GameModel();
        gameModel.lifes = game.lifes - lifesLost;
        gameModel.maxStage = game.maxStage < stage ? stage : game.maxStage;
        gameModel.bombs = bombs;
        gameModel.playerModel = game.playerModel;
        gameModel.playerModel.maxScore = gameModel.playerModel.maxScore < score ? score : gameModel.playerModel.maxScore;
        savePlayerTemp(gameModel);

        return game;
    }

    public static GameModel addLifes(int lifes)
    {
        game.lifes = lifes;
        savePlayerTemp(game);

        return game;
    }

    public static GameModel addBoms(int boms)
    {
        game.bombs = boms;
        savePlayerTemp(game);

        return game;
    }

}
