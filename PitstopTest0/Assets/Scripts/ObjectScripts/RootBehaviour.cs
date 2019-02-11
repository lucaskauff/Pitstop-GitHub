using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehaviour : ScannableObjectBehaviour
{
    public LineRenderer liana;
    public GameObject player;
    GameObject target;

    [SerializeField]
    float lifeInSeconds;

    private bool living;

    private void Awake()
    {
        liana = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (isFired)
        {
            /*RaycastHit2D hitPoint = Physics2D.Raycast(player.transform.position, scannableObjBeh.targetPos);
            Debug.DrawLine(player.transform.position, scannableObjBeh.targetPos, Color.blue);*/

            //if (hitPoint == true && hitPoint.collider.gameObject.tag == ("HookPoint"))
            //{
                RaycastHit2D trip = Physics2D.Raycast(player.transform.position, targetPos);
                liana.enabled = true;
                liana.SetPosition(0, player.transform.position);
                liana.SetPosition(1, targetPos);
            /*}

            else
            {
                Debug.Log("Nope Rope !");
            }*/
        }


        if (isArrived)
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