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
        [SerializeField] float bounceTime = 1;
        [SerializeField] float bounceAmount = 1;
        [SerializeField] int damageDealing = 1;
        [SerializeField] float decalageY = 0.5f;
        [SerializeField] LayerMask layerLiana = default;
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
        bool raycastsOkay = false;
        bool impactAngleSet;
        Vector2 impactAngle;

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
            trips = new RaycastHit2D[numberOfHookpointsOnStart];
            raycastsOkay = false;
            impactAngleSet = false;
            StopAllCoroutines();
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
                    else if (i >= 1)
                    {
                        raycastsOkay = true;
                    }
                }
            }
        }

        public void LianaCollider()
        {
            if (myLineRend.enabled == true)
            {
                if (raycastsOkay)
                {
                    for (int i = 0; i < hookpoints.Length; i++)
                    {
                        if (hookpoints[i] != null && i >= 1)
                        {
                            Vector2 originHookpoint = new Vector2(hookpoints[i - 1].transform.position.x, hookpoints[i - 1].transform.position.y + decalageY);
                            Vector2 targetHookpoint = new Vector2(hookpoints[i].transform.position.x, hookpoints[i].transform.position.y + decalageY);

                            trips[i] = Physics2D.Raycast(originHookpoint, targetHookpoint - originHookpoint, Vector2.Distance(originHookpoint, targetHookpoint), layerLiana);
                            Debug.DrawLine(new Vector2(hookpoints[i - 1].transform.position.x, hookpoints[i - 1].transform.position.y + decalageY), new Vector2(hookpoints[i].transform.position.x, hookpoints[i].transform.position.y + decalageY), Color.red);
                        }
                    }
                }

                for (int i = 0; i < trips.Length; i++)
                {
                    if (trips[i].collider != null && trips[i].collider.gameObject.tag != "HookPoint")
                    {
                        //Debug.Log(trips[i].collider.gameObject.name);

                        if (trips[i].collider.gameObject.tag == "Player" && i >= 1)
                        {
                            StartCoroutine(PlayerBounce(hookpoints[i-1].transform.position, hookpoints[i].transform.position, trips[i].collider.gameObject.transform.position));
                        }
                    }
                }
            }
        }

        IEnumerator PlayerBounce(Vector2 originHP, Vector2 targetHP, Vector2 impactPos)
        {
            myLineRend.enabled = false;
            mark = true;

            player.GetComponent<PlayerControllerIso>().playerCanMove = false;
            //player.GetComponent<PlayerControllerIso>().moveInput = new Vector2(player.GetComponent<PlayerControllerIso>().moveInput.x * -bounceAmount, player.GetComponent<PlayerControllerIso>().moveInput.y * -bounceAmount);
            
            if (!impactAngleSet)
            {
                impactAngle = player.GetComponent<PlayerControllerIso>().lastMove + impactPos;
                impactAngleSet = true;
            }

            Vector2 impactPosProj;
            Vector2 bounceVector;

            if (Vector2.Distance(originHP, impactPos) <= Vector2.Distance(targetHP, impactPos))
            {
                impactPosProj = impactPos + (new Vector2(-(impactPos.x - originHP.x), -(impactPos.y - originHP.y)));

                if (Vector2.Distance(originHP, impactAngle) <= Vector2.Distance(impactPosProj, impactAngle))
                {
                    float angleForCase1;
                    angleForCase1 = Vector3.SignedAngle(impactAngle, originHP, impactPos);
                    bounceVector = Quaternion.AngleAxis(2 * angleForCase1, new Vector3(0, 0, 1)) * impactAngle;
                    Debug.Log("case1");
                }
                else
                {
                    float angleForCase3;
                    angleForCase3 = Vector3.SignedAngle(impactAngle, impactPosProj, impactPos);
                    bounceVector = Quaternion.AngleAxis(2 * angleForCase3, new Vector3(0, 0, 1)) * impactAngle;
                    Debug.Log("case3");
                }
            }
            else
            {
                impactPosProj = impactPos + (new Vector2(-(impactPos.x - targetHP.x), -(impactPos.y - targetHP.y)));

                if (Vector2.Distance(targetHP, impactAngle) <= Vector2.Distance(impactPosProj, impactAngle))
                {
                    float angleForCase2;
                    angleForCase2 = Vector3.SignedAngle(impactAngle, targetHP, impactPos);
                    bounceVector = Quaternion.AngleAxis(2 * angleForCase2, new Vector3(0, 0, 1)) * impactAngle;
                    Debug.Log("case2");
                }
                else
                {
                    float angleForCase3;
                    angleForCase3 = Vector3.SignedAngle(impactAngle, impactPosProj, impactPos);
                    bounceVector = Quaternion.AngleAxis(2 * angleForCase3, new Vector3(0, 0, 1)) * impactAngle;
                    Debug.Log("case3");
                }
            }

            player.GetComponent<PlayerControllerIso>().moveInput = new Vector2(bounceVector.x * bounceAmount, bounceVector.y * bounceAmount);          

            yield return new WaitForSeconds(bounceTime);

            player.GetComponent<PlayerControllerIso>().playerCanMove = true;
            ResetHookpoints();
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