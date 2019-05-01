using UnityEngine;
using System.Collections;
/*
    Novithian
    04/25/2016
*/
public class Barrera : MonoBehaviour
{
    public float radius = 2f;      //Distance from the center of the circle to the edge

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vectorZero = new Vector2(0, 0);

        float currentAngle = Quaternion.FromToRotation(Vector3.up, vectorCamera - vectorZero).eulerAngles.z;
        currentAngle = currentAngle + 90;

        Vector2 movement = Vector2.up;
        movement.x = 1 * Mathf.Cos(currentAngle * Mathf.PI / 180) * radius;
        movement.y = 1 * Mathf.Sin(currentAngle * Mathf.PI / 180) * radius;
        transform.position = movement;

        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, currentAngle + 90);
    }
}
