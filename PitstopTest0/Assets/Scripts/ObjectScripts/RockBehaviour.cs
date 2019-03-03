using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RockBehaviour : MonoBehaviour
{
    public float heightWhereToSpawn;

    [SerializeField]
    GameObject rockDetection;
    [SerializeField]
    ScannableObjectBehaviour scannableObjectBehaviour;
    [SerializeField]
    CinemachineImpulseSource playerImpulseSource;
    [SerializeField]
    float impulseDuration;

    private bool impulseGenerated = false;
    private bool arrivalCheck = false;

    private void Update()
    {
        if (scannableObjectBehaviour.isArrived && !arrivalCheck)
        {
            rockDetection.SetActive(true);

            if (scannableObjectBehaviour.isFired)
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
    }

    IEnumerator CameraShake()
    {
        playerImpulseSource.GenerateImpulse();
        yield return new WaitForSeconds(impulseDuration);
        impulseGenerated = true;
    }
}