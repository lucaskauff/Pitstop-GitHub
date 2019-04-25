using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class RootBehaviour2 : MonoBehaviour
    {
        //GameManager
        InputManager inputManager;

        [Header("My Components")]
        [SerializeField] LineRenderer myLineRend = default;

        [Header("Public")]
        public GameObject[] hookpoints;
        [HideInInspector] public bool pointSelect;
        [HideInInspector] public bool mark;

        [Header("Player related")]
        [SerializeField] Transform thePlayer = default;
        [SerializeField] GameObject player;
        [SerializeField] Vector2 playerPos;
        public CrystalController crystalController;

        [Header("Serializable")]
        [SerializeField] int damageDealing = 1;
        [SerializeField] float decalageY = 0.5f;
        [SerializeField] EnemyHealthManager bossHealth;
        [SerializeField] ScannableObjectBehaviour appleScanObjBeh = default;

        //Private
        bool preview = true;
        float Angle1;
        float Angle2;
        Transform impactPos;
        float velocityX;
        Rigidbody2D impAppleRb;
        GorillaBehaviour rush;
        int numberOfHookpointsOnStart;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            numberOfHookpointsOnStart = hookpoints.Length;
            Debug.Log(numberOfHookpointsOnStart);
        }

        private void Update()
        {
            if (crystalController.scannedObject != null)
            {
                if (crystalController.scannedObject.tag == "ObjectRoot")
                {
                    if (inputManager.onLeftClick)
                    {
                        ResetHookpoints();
                    }

                    LineManagement();
                }

                LianaCollider();
            }
        }

        public void ResetHookpoints()
        {
            for (int i = 0; i < hookpoints.Length; i++)
            {
                if (hookpoints[i] == null)
                {
                    return;
                }
                else if (hookpoints[i] != null)
                {
                    hookpoints = new GameObject[numberOfHookpointsOnStart];
                }
            }
        }

        public void LineManagement()
        {
            if (inputManager.leftClickBeingPressed)
            {
                pointSelect = true;
                myLineRend.enabled = false;

                if (hookpoints[0] == null)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < hookpoints.Length - 1; i++)
                    {
                        myLineRend.positionCount = hookpoints.Length - 1;
                        myLineRend.enabled = true;
                        preview = true;

                        if (hookpoints[i] != null)
                        {
                            myLineRend.SetPosition(i, new Vector2(hookpoints[i].transform.position.x, hookpoints[i].transform.position.y + decalageY));
                        }
                        else
                        {
                            myLineRend.SetPosition(i, inputManager.cursorPosition);
                        }
                    }
                }
            }

            if (inputManager.onLeftClickReleased)
            {
                pointSelect = false;

                if (hookpoints[1] == null)
                {
                    preview = false;
                    myLineRend.enabled = false;
                    return;
                }
                else
                {
                    for (int i = 0; i < hookpoints.Length - 1; i++)
                    {
                        myLineRend.positionCount = i + 2;
                    }
                }
            }
        }

        public void LianaCollider()
        {
            if (myLineRend.enabled == true && preview == false && myLineRend.positionCount == 2)
            {
                RaycastHit2D trip = Physics2D.Raycast(new Vector2(hookpoints[0].transform.position.x, hookpoints[0].transform.position.y + decalageY), new Vector2(hookpoints[1].transform.position.x, hookpoints[1].transform.position.y + decalageY) - new Vector2(hookpoints[0].transform.position.x, hookpoints[0].transform.position.y + decalageY), Vector2.Distance(new Vector2(hookpoints[0].transform.position.x, hookpoints[0].transform.position.y + decalageY), new Vector2(hookpoints[1].transform.position.x, hookpoints[1].transform.position.y + decalageY)));

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
                        impAppleRb = crystalController.cloneProj.GetComponent<Rigidbody2D>();
                        velocityX = impAppleRb.velocity.x;
                        appleScanObjBeh = crystalController.cloneProj.GetComponent<ScannableObjectBehaviour>();
                        impactPos = (trip.collider.transform);
                        AppleBounce();
                    }
                }

                if (myLineRend.positionCount == 3)
                {
                    RaycastHit2D trip2 = Physics2D.Raycast(new Vector2(hookpoints[1].transform.position.x, hookpoints[1].transform.position.y + decalageY), new Vector2(hookpoints[2].transform.position.x, hookpoints[2].transform.position.y + decalageY) - new Vector2(hookpoints[1].transform.position.x, hookpoints[1].transform.position.y + decalageY), Vector2.Distance(new Vector2(hookpoints[1].transform.position.x, hookpoints[1].transform.position.y + decalageY), new Vector2(hookpoints[2].transform.position.x, hookpoints[2].transform.position.y + decalageY)));

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
            Vector2 shootVect = thePlayer.position;      
            Vector2 pillarVect = impactPos.position;
            Angle1 = Vector2.Angle(pillarVect, shootVect);
            Angle2 = 180 - Angle1;

            Debug.Log("Angle is" + Angle1);
            Debug.Log("Angle 2 is" + Angle2);

            Vector2 result = new Vector2(Mathf.Sin(Angle2), Mathf.Cos(Angle2));
            Debug.Log(result);

            appleScanObjBeh.targetPos = result;

            /*else if (rb.velocity.x < 0f)
            {
                scanObjBeh.targetPos = -result;
            }*/

            appleScanObjBeh.Shoot();
        }

        IEnumerator EnemyDamage()
        {
            bossHealth.HurtEnemy(damageDealing);
            myLineRend.enabled = false;
            mark = true;
            rush.rushSpeed = -rush.rushSpeed;
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