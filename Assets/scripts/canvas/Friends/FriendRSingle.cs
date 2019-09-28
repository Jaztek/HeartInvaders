using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendRSingle : MonoBehaviour
{
    public Text nameFriend;
    public Text scoreFriend;

    private string friendDeviceID;

      public void setFriend(string deviceID, string name, string score){
        nameFriend.text = name;
        scoreFriend.text = score;

        this.friendDeviceID = deviceID;

    }

    public void acceptDeclineFriend(bool isAccept){
        if(isAccept){
           // FriendService.acceptFriend(friendDeviceID);
        } else {
          //  FriendService.rejectedFriend(friendDeviceID);
        }
         GameObject.Find("FriendsCanvas").GetComponent<FriendPanelController>().refreshFriendList();
         Destroy(this.gameObject);
    }

}
