using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class CreateNick : MonoBehaviour
{

    public InputField input;
    public Text ErrorText;

    void OnEnable()
    {
        // PlayerService.setUserCache();
    }

    public void checkNick()
    {

        string nick = input.text.ToUpper();
        if (!string.IsNullOrWhiteSpace(nick))
        {
            ErrorText.gameObject.SetActive(false);
            Task task = PlayerService.getPlayerByNickTask(nick);

            StartCoroutine(login(task, nick));

        }
    }

    public IEnumerator login(Task task, string nick)
    {
        while (task.IsCompleted == false)
        {
            yield return null;
        }
        if (task.IsFaulted)
        {
            Debug.Log("IsFaulted");
            throw task.Exception;
        }
        if (task.IsCompleted)
        {
            Task<PlayerModel> user = ((Task<PlayerModel>)task);
            if(user.Result == null){
                this.gameObject.SetActive(false);
                LoadSaveService.saveNick(nick);
            } else {
                ErrorText.gameObject.SetActive(true);
            }
        }
    }
}
