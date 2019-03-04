using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RockBehaviour : MonoBehaviour
{
    public float heightWhereToSpawn;
    public float fallSpeed;

    [SerializeField]
    float impulseDuration;
    [SerializeField]
    GameObject rockDetection;
    [SerializeField]
    ScannableObjectBehaviour scannableObjectBehaviour;
    [SerializeField]
    CinemachineImpulseSource playerImpulseSource;

    private bool impulseGenerated = false;
    private bool arrivalCheck = false;

    private void Update()
    {
        if (scannableObjectBehaviour.isFired)
        {
            rockDetection.SetActive(false);
            return;
        }
        else
        {
            rockDetection.SetActive(true);
        }

        if (scannableObjectBehaviour.isArrived && !arrivalCheck)
        {
            if (impulseGenerated)
            {
                StopCoroutine(CameraShake());
                arrivalCheck = true;
                return;
            }

            StartCoroutine(CameraShake());
        }
    }

    IEnumerator CameraShake()
    {
        playerImpulseSource.GenerateImpulse();
        yield return new WaitForSeconds(impulseDuration);
        impulseGenerated = true;
    }
}