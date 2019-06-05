using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchFriend : MonoBehaviour
{
    public InputField input;
    public Text ErrorText;
    public FriendPanelController friendsPanel; 

    public void findFriend()
    {

        string nick = input.text.ToUpper();
        if (!string.IsNullOrWhiteSpace(nick))
        {
            ErrorText.gameObject.SetActive(false);

            PlayerModel player = QueryMaster.getUserByNick(nick);


            if (player == null)
            {
                UnityEngine.Debug.LogError("Nick no existe");
                ErrorText.gameObject.SetActive(true);
            }
            else
            {
                QueryMaster.addFriend(nick);
                refreshFriendList();
                this.gameObject.SetActive(false);
            }
        }
    }

    public void refreshFriendList(){
        friendsPanel.refreshFriendList();
    }
}
