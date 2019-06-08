using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendSingle : MonoBehaviour
{
    public Text position;
    public Text nameFriend;
    public Text scoreFriend;
    public Color color;

    public void setFriend(int pos, string name, string score, bool isPlayer){
        nameFriend.text = name;
        scoreFriend.text = score;
        position.text = pos +":";

        if (isPlayer)
        {
            nameFriend.color = color;
            scoreFriend.color = color;
            position.color = color;
        }

    }
}
