using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNick : MonoBehaviour
{

    public InputField input;
    public Text ErrorText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkNick()
    {

        string nick = input.text.ToUpper();
        if (!string.IsNullOrWhiteSpace(nick))
        {
            ErrorText.gameObject.SetActive(false);

            PlayerModel player = PlayerService.getUserByNick(nick);


            if (player != null)
            {
                UnityEngine.Debug.LogError("Nick ya existe");
                ErrorText.gameObject.SetActive(true);
            }
            else
            {
                LoadSaveService.saveNick(nick);
                this.gameObject.SetActive(false);
            }
        }
    }
}
