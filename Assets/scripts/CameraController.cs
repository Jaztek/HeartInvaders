using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject ejemplo;
    Vector3 vectorObjetivo;
    Vector3 vectorMenu = new Vector3(0, 0, -2);
    Vector3 vectorGame = new Vector3(0, 0, -10);

    void Start()
    {
        vectorObjetivo = vectorMenu;
        StartCoroutine("goToVector");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goToGame()
    {
        vectorObjetivo = vectorGame;
       // GetComponent<Camera>().SetStereoProjectionMatrix() = Camera.projectionMatrix.
    }
    public void goToMenu()
    {
        vectorObjetivo = vectorMenu;
    }


    IEnumerator goToVector()
    {

        for (; ; )
        {
            if (vectorObjetivo != null)
            {

                float z = transform.position.z;

                // && (Mathf.Abs((transform.position.z - vectorObjetivo.z)) > 1)
                if (transform.position.z != vectorObjetivo.z)
                //&& transform.position.z + 0.1f < vectorObjetivo.z && transform.position.z - 0.1f > vectorObjetivo.z)
                {
                    z = transform.position.z < vectorObjetivo.z ? transform.position.z + 0.05f : transform.position.z - 0.1f;
                }

                Vector3 vectorTemporal = new Vector3(0, 0, z);

               // transform.position = vectorTemporal;
            }

            //este yield indica  cada cuando se va a llamar a la corrutina, en este caso cada 0.1 segundos.
            yield return new WaitForSeconds(.01f);
        }

    }
}
