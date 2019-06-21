using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdPanel : MonoBehaviour
{
    Text titlePanel;
    Text bodyPanel;
    // Start is called before the first frame update
    public void adPanel(string title, string body)
    {
        titlePanel.text = title;
        bodyPanel.text = title;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
