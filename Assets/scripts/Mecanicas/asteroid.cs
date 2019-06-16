using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public int rotateVel = 15;
    public int vel = 15;
    private Rigidbody2D rigidbodyShot;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyShot = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward * Time.deltaTime * rotateVel);
        //rigidbodyShot.velocity = transform.TransformDirection(Vector3.right * vel);
        //rigidbodyShot.AddForce(Vector3.right * vel);
    }
}
