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
        int pos = 1;
        if (listFriends.Count > 0)
        {
            List<FriendModel> friendsModelOK = listFriends.Where(f => f.status == "OK").ToList();

            if (friendsModelOK.Count > 0)
            {
                List<string> listDeviceIdOK = friendsModelOK.Select(f => f.deviceId).ToList();
                List<PlayerModel> friendsOK = PlayerService.getUsersByIds(listDeviceIdOK);

                friendsOK.Add(LoadSaveService.game.playerModel);
                friendsOK.Sort((friend1, friend2) => friend2.maxScore.CompareTo(friend1.maxScore));
                string idPlayer = LoadSaveService.game.playerModel.deviceId;

                friendsOK.ForEach(fr =>
                {
                    FriendModel frModel = new FriendModel();
                    if (fr.deviceId != idPlayer)
                    {
                        frModel = listFriends.Find(onlinefr => onlinefr.deviceId == fr.deviceId);
                    }

                    GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/Canvas/FriendPanel", typeof(GameObject));
                    GameObject panelFriend = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);
                    panelFriend.transform.SetParent(friendTable.transform);

                    bool isPlayer = fr.deviceId == idPlayer ? true : false;
                    panelFriend.GetComponent<FriendSingle>().setFriend(pos, fr.name, fr.maxScore.ToString(), isPlayer);
                    pos++;
                });
            }

            List<FriendModel> friendsModelRequest = listFriends.Where(f => f.status == "Request").ToList();
            if (friendsModelRequest.Count > 0)
            {
                friendRequestPanel.SetActive(true);
                cleanChilds(friendRequestTable);

                List<string> listDeviceIdRequest = listFriends.Select(f => f.deviceId).ToList();
                List<PlayerModel> friendsRequest = PlayerService.getUsersByIds(listDeviceIdRequest);
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
        } else
        {
            PlayerModel me = LoadSaveService.game.playerModel;

            GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/Canvas/FriendPanel", typeof(GameObject));
            GameObject panelFriend = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);
            panelFriend.transform.SetParent(friendTable.transform);

            panelFriend.GetComponent<FriendSingle>().setFriend(pos, me.name, me.maxScore.ToString(), true);
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
