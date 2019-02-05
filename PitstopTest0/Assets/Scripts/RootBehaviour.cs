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

    private void Awake()
    {
        liana = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RaycastHit2D trip = Physics2D.Raycast(player.transform.position, scannableObjBeh.targetPos);
         

        if (scannableObjBeh.isFired)
        {
            Debug.DrawLine(player.transform.position, scannableObjBeh.targetPos, Color.blue);
            liana.enabled = true;
            liana.SetPosition(0, player.transform.position);
            liana.SetPosition(1, scannableObjBeh.targetPos);
        }


        if (Input.GetKeyUp(KeyCode.Mouse0))
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
            liana.enabled = false;
            Destroy(gameObject);
        }
    }
}