using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class btAcceptFriend : MonoBehaviour, IPointerDownHandler
{
    public FriendRSingle friendRequestPanel;
    public bool isAccept;

     public void OnPointerDown(PointerEventData pointerEventData)
    {
        friendRequestPanel.acceptDeclineFriend(isAccept);
    }
    
}
