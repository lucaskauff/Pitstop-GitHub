using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class RootBehaviour2 : MonoBehaviour
    {
        [Header("Other Components")]
        [SerializeField] LineRenderer liana = default;
        [SerializeField] ScannableObjectBehaviour scanObjBeh = default;

        //Public
        public GameObject player;
        public GameObject[] hookpoints;
        public bool pointSelect;
        public CrystalController crys;
        public int damageDealing = 1;
        public EnemyHealthManager bossHealth;
        public bool mark;

        //Serializable
        [SerializeField]
        float lifeInSeconds;
        [SerializeField]
        bool preview = true;
        [SerializeField]
        Vector2 playerPos;
        [SerializeField]
        Rigidbody2D rb;
        [SerializeField]
        float velocityX;
        [SerializeField]
        float decalageY = 0.5f;

        //Private
        bool living;
        GorillaBehaviour rush;
        Vector2 mousePos;
        float Angle1;
        float Angle2;
        Transform impactPos;

        private void Update()
        {
            if (crys.scannedObject != null)
            {
                if (crys.scannedObject.name == "ScannableRoot")
                {
                    ResetHookpoints();

                    LineManagement();
                }

                LianaCollider();

                if (crys.scannedObject.tag == "ObjectApple" && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    playerPos = player.transform.position;
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

        public void LineManagement()
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
                RaycastHit2D trip = Physics2D.Raycast(hookpoints[0].transform.position, hookpoints[1].transform.position - hookpoints[0].transform.position, Vector2.Distance(hookpoints[0].transform.position, hookpoints[1].transform.position));
                Debug.DrawLine(hookpoints[0].transform.position, hookpoints[1].transform.position, Color.green);

                if (trip.collider != null)
                {
                    Debug.Log(trip.collider.gameObject.name);

                    if (trip.collider.tag == "Enemy")
                    {
                        Debug.Log(trip.collider.name);
                        rush = trip.collider.gameObject.GetComponent<GorillaBehaviour>();
                        StartCoroutine(EnemyDamage());
                    }

                    if (trip.collider.tag == "ObjectApple")
                    {
                        //StartCoroutine(Bounce());
                        rb = crys.cloneProj.GetComponent<Rigidbody2D>();
                        velocityX = rb.velocity.x;
                        scanObjBeh = crys.cloneProj.GetComponent<ScannableObjectBehaviour>();
                        impactPos = (trip.collider.transform);
                        AppleBounce();                   
                    }
                }

                if (liana.positionCount == 3)
                {
                    RaycastHit2D trip2 = Physics2D.Raycast(hookpoints[1].transform.position, hookpoints[2].transform.position - hookpoints[1].transform.position, Vector2.Distance(hookpoints[1].transform.position, hookpoints[2].transform.position));
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

        void AppleBounce()
        {            
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 shootVect = playerPos;      
            Vector2 pillarVect = impactPos.position;
            Angle1 = Vector2.Angle(pillarVect, shootVect);
            Angle2 = 180 - Angle1;

            Debug.Log("Angle is" + Angle1);
            Debug.Log("Angle 2 is" + Angle2);

            Vector2 result = new Vector2(Mathf.Sin(Angle2), Mathf.Cos(Angle2));
            Debug.Log(result);

            scanObjBeh.targetPos = result;

            /*else if (rb.velocity.x < 0f)
            {
                scanObjBeh.targetPos = -result;
            }*/

            scanObjBeh.Shoot();
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

        IEnumerator Bounce()
        {
            Vector2 shootVect = player.transform.position;
            Vector2 pillarVect = impactPos.position;
            Angle1 = Vector2.Angle(pillarVect, shootVect);
            Angle2 = 180 - Angle1;
            yield return new WaitForSeconds(1f);
            Debug.Log("Angle is" + Angle1);
            Debug.Log("Angle 2 is" + Angle2);
        }
    }
}