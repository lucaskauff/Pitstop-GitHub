using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehaviour : MonoBehaviour
{
    public LineRenderer liana;
    public GameObject player;
    public GameObject target;

    [SerializeField]
    float lifeInSeconds;

    public CrystalController crys;
    public ScannableObjectBehaviour scannableObjBeh;
    private bool living;

    private void Awake()
    {
        liana = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RaycastHit2D hitPoint = Physics2D.Raycast(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), crys.maxScanRange);
        Debug.DrawLine(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.blue);


        if (scannableObjBeh.isFired)
        {
            
            if (target != null && target.tag == "HookPoint") { 
            RaycastHit2D trip = Physics2D.Raycast(player.transform.position, target.transform.position);
                Debug.DrawLine(player.transform.position, target.transform.position, Color.green);
                liana.enabled = true;
                liana.SetPosition(0, player.transform.position);
                liana.SetPosition(1, target.transform.position);
            }

            else
            {
                Debug.Log("Nope Rope !");
            }
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
            liana.enabled = false;
            Destroy(gameObject);
        }
    }
}