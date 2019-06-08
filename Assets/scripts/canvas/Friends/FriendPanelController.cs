using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FriendPanelController : MonoBehaviour
{
    public GameObject friendTable;

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
        LoadSaveService.game.onlineModel = FriendService.getFriends(LoadSaveService.game.onlineModel.deviceId);
        List<FriendModel> listFriends = LoadSaveService.game.onlineModel.listFriends;

        if (listFriends.Count > 0)
        {

            List<string> listDeviceId = LoadSaveService.game.onlineModel.listFriends.Select(f => f.deviceId).ToList();
            List<PlayerModel> friends = PlayerService.getUsersByIds(listDeviceId);
            friends.Add(LoadSaveService.game.playerModel);
            friends.Sort((friend1,friend2) => friend2.maxScore.CompareTo(friend1.maxScore));
            int pos = 1;
            string idPlayer = LoadSaveService.game.playerModel.deviceId;
            friends.ForEach(fr =>
            {
                GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/Canvas/FriendPanel", typeof(GameObject));
                GameObject panelFriend = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);
                panelFriend.transform.parent = friendTable.transform;

                bool isPlayer = fr.deviceId == idPlayer ? true : false;
                panelFriend.GetComponent<FriendSingle>().setFriend(pos, fr.name, fr.maxScore.ToString(), isPlayer);
                pos++;
            });
        }
    }
   class GFG : IComparer<int> 
{ 
    public int Compare(int x, int y) 
    { 
        if (x == 0 || y == 0) 
        { 
            return 0; 
        } 
          
        // CompareTo() method 
        return x.CompareTo(y); 
          
    } 
} 
}
