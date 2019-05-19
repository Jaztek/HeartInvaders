using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameModel gameModel;

    public void setGameModel(GameModel game)
    {
        gameModel = game;
    }

    public GameModel getGameModel()
    {
        return gameModel;
    }

    public int getBombs()
    {
        return gameModel.bombs;
    }

    public void setBooms(int bombs)
    {
        gameModel.bombs = bombs;
    }
}
