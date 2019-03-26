using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_Apple : MonoBehaviour
{
    [SerializeField]
    GameObject explosionRange;
    [SerializeField]
    int timer;

    void Start()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(timer);
        explosionRange.SetActive(true);
        Destroy(gameObject,0.1f);
    }
}
