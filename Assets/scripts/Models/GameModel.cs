using System;

[System.Serializable]
public class GameModel
{
    public int lifes;
    public int bombs;
    public int maxStage;

    public int nextStage;
    public bool isMute;
    public DateTime dateLastLife;

    public PlayerModel playerModel;
    public OnlineModel onlineModel;
}
