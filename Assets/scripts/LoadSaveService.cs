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
    }


    public static GameModel Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            GameModel gameModel = (GameModel)bf.Deserialize(file);
            file.Close();
            file.Dispose();

            game = gameModel;
        }
        else
        {
            game = new GameModel();
            game.lifes = 10;
            game.maxScore = 0;
            game.maxStage = 0;
            savePlayerTemp(game);
        }

        return game;
    }

    public static GameModel saveCurrentGame(int lifesLost, long score, int stage)
    {
        GameModel gameModel = new GameModel();
        gameModel.lifes = game.lifes - lifesLost;
        gameModel.maxScore = game.maxScore < score ? score : game.maxScore;
        gameModel.maxStage = game.maxStage < stage ? stage : game.maxStage;
        savePlayerTemp(gameModel);

		return game;
    }

	public static GameModel addLifes(int lifes)
    {
        game.lifes = lifes;
        savePlayerTemp(game);

		return game;
    }

}
