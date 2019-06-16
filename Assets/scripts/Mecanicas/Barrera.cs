using UnityEngine;
using System.Collections;

public class Barrera : MonoBehaviour
{
    public float radius = 2f;      //Distance from the center of the circle to the edge
    public GameObject barreraAzul;
    public GameObject barreraRoja;


    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Vector3 vectorCameraM = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float normalYM = vectorCameraM.y + 3.5f;
            float normalAngleM = normalYM * 180 / 7;
            float currentAngleM = normalAngleM + 270;

            Vector2 movementM = Vector2.up;
            movementM.x = 1 * Mathf.Cos(currentAngleM * Mathf.PI / 180) * radius;
            movementM.y = 1 * Mathf.Sin(currentAngleM * Mathf.PI / 180) * radius;
            barreraAzul.transform.position = movementM;

            barreraAzul.transform.eulerAngles = new Vector3(barreraAzul.transform.rotation.x, barreraAzul.transform.rotation.y, currentAngleM + 90);
        }

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 vectorCamera = Camera.main.ScreenToWorldPoint(touch.position);

                if (vectorCamera.x < 0) { moveBlueBarrera(vectorCamera); }
                else { moveRedBarrera(vectorCamera); }
            }

            if (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Touch touch2 = Input.GetTouch(1);
                Vector3 vectorCamera = Camera.main.ScreenToWorldPoint(touch2.position);

                if (vectorCamera.x < 0) { moveBlueBarrera(vectorCamera); }
                else { moveRedBarrera(vectorCamera); }
            }
        }
    }


    private void moveBlueBarrera(Vector3 vectorCamera)
    {
        if (vectorCamera.y < -1.5f)
        {
            vectorCamera.y = -1.5f;
        }
        else if (vectorCamera.y > 1.5f)
        {
            vectorCamera.y = 1.5f;
        }
        float normalY = vectorCamera.y + 1.5f;
        float normalAngle = normalY * 180 / 3;
        float currentAngle = -normalAngle + 270;

        Vector2 movement = Vector2.up;
        movement.x = 1 * Mathf.Cos(currentAngle * Mathf.PI / 180) * radius;
        movement.y = 1 * Mathf.Sin(currentAngle * Mathf.PI / 180) * radius;
        barreraAzul.transform.position = movement;

        barreraAzul.transform.eulerAngles = new Vector3(barreraAzul.transform.rotation.x, barreraAzul.transform.rotation.y, currentAngle + 90);

    }

    private void moveRedBarrera(Vector3 vectorCamera)
    {
        if (vectorCamera.y < -1.5f)
        {
            vectorCamera.y = -1.5f;
        }
        else if (vectorCamera.y > 1.5f)
        {
            vectorCamera.y = 1.5f;
        }
        float normalY = vectorCamera.y + 1.5f;
        float normalAngle = normalY * 180 / 3;
        float currentAngle = normalAngle + 270;

        Vector2 movement = Vector2.up;
        movement.x = 1 * Mathf.Cos(currentAngle * Mathf.PI / 180) * radius;
        movement.y = 1 * Mathf.Sin(currentAngle * Mathf.PI / 180) * radius;
        barreraRoja.transform.position = movement;

        barreraRoja.transform.eulerAngles = new Vector3(barreraRoja.transform.rotation.x, barreraRoja.transform.rotation.y, currentAngle + 90);

    }

    public void activeBarreras(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
