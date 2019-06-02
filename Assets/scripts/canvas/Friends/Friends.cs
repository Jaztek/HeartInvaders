using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friends : MonoBehaviour
{
    public GameObject friendTable;

     void OnEnable()
    {
        List<PlayerModel> friends = LoadSaveService.getOnlineModel().listFriends;

        friends.ForEach(fr => {
            GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/Canvas/FriendPanel", typeof(GameObject));
            GameObject panelFriend = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);
            panelFriend.GetComponent<FriendSingle>().setFriend(fr.name , fr.maxScore.ToString());
            panelFriend.transform.parent = friendTable.transform;
        });
    }
}
