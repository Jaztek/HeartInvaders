using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendSingle : MonoBehaviour
{
    public Text nameFriend;
    public Text scoreFriend;
    public GameObject panelRequest;
    public GameObject panelPending;
     public GameObject panelAccepted;

    public void setFriend(string name, string score, string status){
        nameFriend.text = name;
        scoreFriend.text = score;

        switch (status)
        {
            case "Request":
                panelRequest.SetActive(true);
                break;
            case "Pending":
                panelPending.SetActive(true);
                break;
            case "OK":
                 panelAccepted.SetActive(true);
                break;
        }
    }
}
