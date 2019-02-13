using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetteraveBehaviour : MonoBehaviour
{
    [SerializeField]
    float lifeInSeconds;

    public ScannableObjectBehaviour scannableObjBeh;
    private bool living;

    private void Update()
    {
        if (scannableObjBeh.isArrived)
        {
            StartCoroutine(Life());
        }
    }

    IEnumerator Life()
    {
        while (lifeInSeconds > 0)
        {
            yield return new WaitForSeconds(1);
            lifeInSeconds--;
            Debug.Log("dead");
            Destroy(gameObject);
        }
    }
}