using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehaviour2 : MonoBehaviour
{
    public LineRenderer liana;
    public GameObject player;
    public GameObject[] hookpoints;
    public bool pointSelect;
    public CrystalController crys;
    public int damageDealing = 1;
    public EnemyHealthManager bossHealth;
    public bool mark;

    int layerMask;
    [SerializeField]
    float lifeInSeconds; 
    private bool living;
    GorillaBehaviour rush;

    private void Awake()
    {
        liana = this.GetComponent<LineRenderer>();
        layerMask = LayerMask.GetMask("Liana");
        mark = false;
    }

    private void Update()
    {        
        if (crys.scannedObject.name == "ScannableRoot")
        {
            ResetHookpoints();

            LineGestion();

            LianaCollider();
        }        
    }

    public void ResetHookpoints()
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
    }

    public void LineGestion()
    {
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
    
    public void LianaCollider()
    {
        if (liana.enabled == true)
        {
            RaycastHit2D trip = Physics2D.Raycast(hookpoints[0].transform.position, hookpoints[1].transform.position - hookpoints[0].transform.position, Vector2.Distance(hookpoints[0].transform.position, hookpoints[1].transform.position), layerMask);
            Debug.DrawLine(hookpoints[0].transform.position, hookpoints[1].transform.position, Color.green);

            if (trip.collider != null)
            {
                Debug.Log(trip.collider.name);
                rush = trip.collider.gameObject.GetComponent<GorillaBehaviour>();
                StartCoroutine (EnemyDamage());
            }

            if (liana.positionCount == 3)
            {
                RaycastHit2D trip2 = Physics2D.Raycast(hookpoints[1].transform.position, hookpoints[2].transform.position - hookpoints[1].transform.position, Vector2.Distance(hookpoints[1].transform.position, hookpoints[2].transform.position), layerMask);
                Debug.DrawLine(hookpoints[1].transform.position, hookpoints[2].transform.position, Color.blue);

                if (trip2.collider != null)
                {
                    Debug.Log(trip2.collider.name + "2");
                    rush = trip2.collider.gameObject.GetComponent<GorillaBehaviour>();
                    StartCoroutine(EnemyDamage());
                }

                else
                {
                    return;
                }
            }                                              
        }
    }

    IEnumerator EnemyDamage()
    {
        bossHealth.HurtEnemy(damageDealing);
        liana.enabled = false;
        mark = true;
        rush.rushSpeed = -(rush.rushSpeed);
        rush.rushTime = 0.1f;
        yield return new WaitForSeconds(1f);
        rush.rushSpeed = -(rush.rushSpeed);

    }
}
