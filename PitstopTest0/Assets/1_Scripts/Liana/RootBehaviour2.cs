using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
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
        public IMP_Apple appleScript;

        int layerMask;
        [SerializeField]
        float lifeInSeconds;
        private bool living;
        GorillaBehaviour rush;
        private Vector2 mousePos;
        bool preview = true;
        [SerializeField]
        float Angle1;

        private void Awake()
        {
            liana = this.GetComponent<LineRenderer>();
            layerMask = LayerMask.GetMask("Liana");
            mark = false;
        }

        private void Update()
        {
            if (crys.scannedObject != null)
            {
                if (crys.scannedObject.name == "ScannableRoot")
                {
                    ResetHookpoints();

                    LineGestion();

                    LianaCollider();
                }
            }

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

                if (hookpoints[0] == null)
                {
                    return;
                }

                else if (hookpoints[1] == null)
                {
                    liana.positionCount = 2;
                    liana.enabled = true;
                    preview = true;
                    liana.SetPosition(0, hookpoints[0].transform.position);
                    liana.SetPosition(1, mousePos);
                }

                else if (hookpoints[2] == null)
                {
                    liana.positionCount = 3;
                    liana.enabled = true;
                    preview = true;
                    liana.SetPosition(0, hookpoints[0].transform.position);
                    liana.SetPosition(1, hookpoints[1].transform.position);
                    liana.SetPosition(2, mousePos);
                }

                else
                {
                    for (int x = 0; x < 3; x++)
                    {
                        preview = true;
                        liana.enabled = true;
                        if (hookpoints[x] != null)
                            liana.SetPosition(0, hookpoints[0].transform.position);
                        liana.SetPosition(1, hookpoints[1].transform.position);
                        liana.SetPosition(2, hookpoints[2].transform.position);
                    }
                }
            }


            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                preview = false;

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
                    preview = false;
                    liana.enabled = false;
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
            if (liana.enabled == true && preview == false && hookpoints[1] != null)
            {
                RaycastHit2D trip = Physics2D.Raycast(hookpoints[0].transform.position, hookpoints[1].transform.position - hookpoints[0].transform.position, Vector2.Distance(hookpoints[0].transform.position, hookpoints[1].transform.position), layerMask);
                Debug.DrawLine(hookpoints[0].transform.position, hookpoints[1].transform.position, Color.green);

                if (trip.collider != null)
                {
                    if (trip.collider.tag == "Enemy")
                    {
                        Debug.Log(trip.collider.name);
                        rush = trip.collider.gameObject.GetComponent<GorillaBehaviour>();
                        StartCoroutine(EnemyDamage());
                    }

                    if (trip.collider.tag == "ObjectApple")
                    {
                        Transform impactPos = (trip.collider.transform);
                        Vector2 shootVect = new Vector2(impactPos.position.x - appleScript.playerPos.x, impactPos.position.y - appleScript.transform.position.y);
                        Vector2 pillarVect = new Vector2(hookpoints[1].transform.position.x - hookpoints[0].transform.position.x, hookpoints[1].transform.position.y - hookpoints[0].transform.position.y);
                        Angle1 = Vector2.Angle(pillarVect, shootVect);
                        Debug.Log("Angle is" + Angle1);
                    }

                }

                if (liana.positionCount == 3)
                {
                    RaycastHit2D trip2 = Physics2D.Raycast(hookpoints[1].transform.position, hookpoints[2].transform.position - hookpoints[1].transform.position, Vector2.Distance(hookpoints[1].transform.position, hookpoints[2].transform.position), layerMask);
                    Debug.DrawLine(hookpoints[1].transform.position, hookpoints[2].transform.position, Color.blue);

                    if (trip2.collider != null)
                    {
                        if (trip.collider.tag == "Enemy")
                        {
                            Debug.Log(trip2.collider.name + "2");
                            rush = trip2.collider.gameObject.GetComponent<GorillaBehaviour>();
                            StartCoroutine(EnemyDamage());
                        }



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

}
