using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendSingle : MonoBehaviour
{
    public Text nameFriend;
    public Text scoreFriend;

    public void setFriend(string name, string score){
        nameFriend.text = name;
        scoreFriend.text = score;
    }
}
