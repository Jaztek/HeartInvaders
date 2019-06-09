using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FriendPanelController : MonoBehaviour
{
    public GameObject friendTable;
    public GameObject friendRequestPanel;
    public GameObject friendRequestTable;

    void OnEnable()
    {
        refreshFriendList();
    }

    public void refreshFriendList()
    {
        cleanChilds(friendTable);
        List<FriendModel> listFriends = LoadSaveService.game.onlineModel.listFriends;
        List<PlayerModel> friendsRequest = new List<PlayerModel>();
        if (listFriends.Count > 0)
        {
            List<string> listDeviceId = listFriends.Select(f => f.deviceId).ToList();
            List<PlayerModel> friends = PlayerService.getUsersByIds(listDeviceId);

            friends.Add(LoadSaveService.game.playerModel);
            friends.Sort((friend1, friend2) => friend2.maxScore.CompareTo(friend1.maxScore));
            string idPlayer = LoadSaveService.game.playerModel.deviceId;

            int pos = 1;
            friends.ForEach(fr =>
            {
                FriendModel frModel = new FriendModel();
                if(fr.deviceId != idPlayer){
                    frModel = listFriends.Find(onlinefr =>  onlinefr.deviceId == fr.deviceId );
                }

                if (frModel.status == null || frModel.status != "Request")
                {

                    GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/Canvas/FriendPanel", typeof(GameObject));
                    GameObject panelFriend = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);
                    panelFriend.transform.SetParent(friendTable.transform);

                    bool isPlayer = fr.deviceId == idPlayer ? true : false;
                    panelFriend.GetComponent<FriendSingle>().setFriend(pos, fr.name, fr.maxScore.ToString(), isPlayer);
                    pos++;
                }
                else
                {
                    friendsRequest.Add(fr);
                }
            });

            if (friendsRequest.Count > 0)
            {
                friendRequestPanel.SetActive(true);
                cleanChilds(friendRequestTable);

                friendsRequest.ForEach(frR =>
                {
                    GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/Canvas/FriendRSingle", typeof(GameObject));
                    GameObject panelFriendR = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);
                    panelFriendR.transform.SetParent(friendRequestTable.transform);
                    panelFriendR.GetComponent<FriendRSingle>().setFriend(frR.deviceId, frR.name, frR.maxScore.ToString());
                });
            } else {
                 friendRequestPanel.SetActive(false);
            }
        }
    }

    private void cleanChilds(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
