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
        [SerializeField] GameObject player = default;
        [SerializeField] Vector2 playerPos;
        public CrystalController crystalController;

        [Header("Serializable")]
        [SerializeField] int damageDealing = 1;
        [SerializeField] float decalageY = 0.5f;
        [SerializeField] EnemyHealthManager bossHealth = default;
        [SerializeField] ScannableObjectBehaviour appleScanObjBeh = default;

        //Private
        float Angle1;
        float Angle2;
        Transform impactPos;
        float velocityX;
        Rigidbody2D impAppleRb;
        GorillaBehaviour rush;
        int numberOfHookpointsOnStart;
        RaycastHit2D[] trips;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            numberOfHookpointsOnStart = hookpoints.Length;

            ResetHookpoints();
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
            hookpoints = new GameObject[numberOfHookpointsOnStart];
            trips = new RaycastHit2D[0];
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
                    myLineRend.positionCount = hookpoints.Length;
                    myLineRend.enabled = true;

                    for (int i = 0; i < hookpoints.Length; i++)
                    {
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

                for (int i = 0; i < hookpoints.Length; i++)
                {
                    if (hookpoints[i] == null)
                    {
                        myLineRend.positionCount = i;
                        trips = new RaycastHit2D[i];
                        return;
                    }
                    else
                    {
                        trips[i] = Physics2D.Raycast(new Vector2(hookpoints[i-1].transform.position.x, hookpoints[i-1].transform.position.y + decalageY), new Vector2(hookpoints[i].transform.position.x, hookpoints[i].transform.position.y + decalageY) - new Vector2(hookpoints[i-1].transform.position.x, hookpoints[i-1].transform.position.y + decalageY), Vector2.Distance(new Vector2(hookpoints[i-1].transform.position.x, hookpoints[i-1].transform.position.y + decalageY), new Vector2(hookpoints[i].transform.position.x, hookpoints[i].transform.position.y + decalageY)));
                    }
                }
            }
        }

        public void LianaCollider()
        {
            if (myLineRend.enabled == true)
            {
                for (int i = 0; i < trips.Length; i++)
                {
                    if (trips[i].collider != null)
                    {
                        Debug.Log(trips[i].collider.gameObject.name);
                    }
                }

                /*
                if (trip.collider != null)
                {
                    Debug.Log(trip.collider.gameObject.name);

                    if (trip.collider.tag == "Enemy")
                    {
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
                }*/
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