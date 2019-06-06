using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FriendPanelController : MonoBehaviour
{
    public GameObject friendTable;
    public Text name;

    void Start()
    {
        name.text = LoadSaveService.game.playerModel.name;
    }

    void OnEnable()
    {
        refreshFriendList();

    }

    public void refreshFriendList()
    {
        foreach (Transform child in friendTable.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<FriendModel> listFriends = LoadSaveService.game.onlineModel.listFriends;

        List<string> listDeviceId = LoadSaveService.game.onlineModel.listFriends.Select(f => f.deviceId).ToList();
        List<PlayerModel> friends = PlayerService.getUsersByIds(listDeviceId);
        friends.ForEach(fr =>
        {
            GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/Canvas/FriendPanel", typeof(GameObject));
            GameObject panelFriend = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);
            string status = listFriends.Find(Onlinefr => Onlinefr.deviceId == fr.deviceId).status;
            panelFriend.transform.parent = friendTable.transform;
            panelFriend.GetComponent<FriendSingle>().setFriend(fr.name, fr.maxScore.ToString(), status);
        });
    }
}
