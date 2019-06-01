using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbodyAst;
    public float vel = 0.5f;
    public int internMult = 1;
    void Start()
    {
         rigidbodyAst = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.rigidbodyAst.velocity = transform.TransformDirection(Vector3.right * vel * internMult);
        this.rigidbodyAst.AddForce(Vector3.right * vel* internMult);
    }
}
