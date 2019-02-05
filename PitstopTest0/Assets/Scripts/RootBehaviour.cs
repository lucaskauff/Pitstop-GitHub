using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehaviour : MonoBehaviour
{
    public LineRenderer liana;
    public GameObject player;

    [SerializeField]
    float lifeInSeconds;

    public ScannableObjectBehaviour scannableObjBeh;
    private bool living;

    private void Update()
    {
        if (scannableObjBeh.isFired)
        {
            liana.enabled = true;
            liana.SetPosition(0, player.transform.position);
            liana.SetPosition(1, scannableObjBeh.targetPos);
        }

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
        }
    }
}