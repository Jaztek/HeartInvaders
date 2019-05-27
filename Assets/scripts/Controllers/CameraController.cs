using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform transformCam;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.05f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 0.8f;

    // The initial position of the GameObject
    Vector3 initialPosition;


    void Awake()
    {
        if (transformCam == null)
        {
            transformCam = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = transformCam.localPosition;
    }
    void Start()
    {
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transformCam.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transformCam.localPosition = initialPosition;
        }
    }

    public void triggerShake()
    {
        shakeDuration = 1.3f;
    }


}
