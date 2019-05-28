using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomsController : MonoBehaviour
{

    public GameObject[] booms;
    // Use this for initialization
    public void setBombs(int bombs)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        for (int i = 0; i < bombs; i++)
        {
            booms[i].SetActive(true);
        }
    }
}
