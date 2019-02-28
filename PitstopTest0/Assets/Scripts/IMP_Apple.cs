using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_Apple : MonoBehaviour
{
    [SerializeField]
    private GameObject ExplosionRange;
    [SerializeField]
    private int Timer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(Timer);
        ExplosionRange.SetActive(true);
        Destroy(gameObject,0.1f);
    }
}
