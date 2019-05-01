using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public int speed = 50;
    private Home homeController;


    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        homeController = GameObject.FindWithTag("GameController").GetComponent<Home>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = transform.TransformDirection(Vector3.right * speed);
        rigidbody.AddForce(Vector3.right * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("barrera"))
        {
            Destroy(this.gameObject);
            homeController.scored();
        }
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
