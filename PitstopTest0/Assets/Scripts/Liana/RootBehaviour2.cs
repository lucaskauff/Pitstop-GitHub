using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehaviour2 : MonoBehaviour
{
    public LineRenderer liana;
    public GameObject player;
    public GameObject[] hookpoints;
    public bool pointSelect;
    public HookPointBehaviour hookBeh;
    public CrystalController crys;
    public int damageDealing = 1;

    int layer_mask;
    [SerializeField]
    float lifeInSeconds; 
    private bool living;

    private void Awake()
    {
        liana = this.GetComponent<LineRenderer>();
        layer_mask = LayerMask.GetMask("Bear");
    }

    private void Update()
    {
        if (crys.scannedObject.name == "ScannableRoot")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                for (int x = 0; x < 3; x++)
                {
                    if (hookpoints[x] == null)
                    {
                        return;
                    }

                    else if (hookpoints[x] != null)
                    {
                        hookpoints = new GameObject[3];
                    }
                }
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                pointSelect = true;
                liana.enabled = false;
            }


            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                pointSelect = false;

                if (hookpoints[2] == null)
                {
                    liana.positionCount = 2;
                }

                else
                {
                    liana.positionCount = 3;
                }

                if (hookpoints[1] == null)
                {
                    return;
                }

                else
                {
                    liana.enabled = true;
                    liana.SetPosition(0, hookpoints[0].transform.position);
                    liana.SetPosition(1, hookpoints[1].transform.position);


                    if (hookpoints[2] != null)
                    {
                        liana.SetPosition(2, hookpoints[2].transform.position);

                    }

                }      
            }
        }

        /*if (liana.enabled == true)
        {
            RaycastHit2D trip = Physics2D.Raycast(hookpoints[0].transform.position, hookpoints[1].transform.position, layer_mask);
            Debug.DrawLine(hookpoints[0].transform.position, hookpoints[1].transform.position, Color.green);

            if (trip.collider.gameObject.name == "Bear")
            {
                Debug.Log("Detect1");
            }

            if (liana.positionCount == 3)
            {
                RaycastHit2D trip2 = Physics2D.Raycast(hookpoints[1].transform.position, hookpoints[2].transform.position, layer_mask);
                Debug.DrawLine(hookpoints[1].transform.position, hookpoints[2].transform.position, Color.blue);

                if (trip2.collider.gameObject.name == "Bear")
                {
                    Debug.Log("Detect1");
                }
            }
        }*/
    }
}
