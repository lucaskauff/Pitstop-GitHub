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
        [SerializeField] GameObject player = default;
        public CrystalController crystalController;

        [Header("Serializable")]
        [SerializeField] float bounceTimeApple = 1;
        [SerializeField] float bounceTimePlayer = 1;
        [SerializeField] float bounceTimeHammerhead = 1;
        [SerializeField] float bounceAmountApple = 1;
        [SerializeField] float bounceAmountPlayer = 1;
        [SerializeField] float bounceAmountHammerhead = 1;
        [SerializeField] float decalageY = 0.5f;
        [SerializeField] LayerMask layerLiana = default;
        [SerializeField] Transform actualThreat = default;
        [SerializeField] float errorMargin = 2f;

        //Private
        Transform impactPos;
        int numberOfHookpointsOnStart;
        RaycastHit2D[] trips;
        bool raycastsOkay = false;
        bool impactAngleSet;
        Vector2 impactAngle;

        Vector2 impactPosProj;
        Vector2 bounceVector;

        //Debug
        Vector2 impactPosDebug;
        Vector2 appleTargetPos;
        Vector2 previousTargetpos;

        //SoundManagement
        AudioSource soundOfPilarSelection;
        float originalPitch;


        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;
            soundOfPilarSelection = GameObject.FindGameObjectWithTag("SoundLianaSelection").GetComponent<AudioSource>();
            originalPitch = soundOfPilarSelection.pitch;

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

            soundOfPilarSelection.pitch = originalPitch;
            

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
                    if (trips[i].collider != null && trips[i].collider.gameObject.tag != "HookPoint" && i >= 1)
                    {
                        Debug.Log(trips[i].collider.gameObject.name);

                        if (trips[i].collider.gameObject.tag == "Player")
                        {
                            StartCoroutine(PlayerBounce(hookpoints[i-1].transform.position, hookpoints[i].transform.position, trips[i].collider.gameObject.transform.position));
                        }
                        else if (trips[i].collider.gameObject.tag == "ObjectApple")
                        {
                            StartCoroutine(AppleBounce(hookpoints[i-1].transform.position, hookpoints[i].transform.position, trips[i].collider.gameObject.transform.position, trips[i].collider.gameObject));
                        }
                        else if (trips[i].collider.gameObject.name == "Hammerhead(Clone)")
                        {
                            StartCoroutine(HammerheadBounce(trips[i].collider.gameObject));
                        }
                        else if (trips[i].collider.gameObject.name == "Eerick")
                        {
                            myLineRend.enabled = false;
                            mark = true;
                            ResetHookpoints();
                        }
                        else if (trips[i].collider.gameObject.tag == "Arrow")
                        {
                            myLineRend.enabled = false;
                            mark = true;
                            ResetHookpoints();
                        }
                    }
                }
            }
        }

        /*
        public void LateUpdate()
        {
            Debug.DrawLine(impactPosDebug, appleTargetPos, Color.yellow);
            Debug.DrawLine(impactPosDebug, previousTargetpos, Color.cyan);
        }
        */

        IEnumerator AppleBounce(Vector2 originHP, Vector2 targetHP, Vector2 impactPos, GameObject apple)
        {
            myLineRend.enabled = false;
            mark = true;

            /*
            if (!impactAngleSet)
            {
                impactAngle = (impactPos + apple.GetComponent<ScannableObjectBehaviour>().targetPos).normalized;
                impactAngleSet = true;

                previousTargetpos = apple.GetComponent<ScannableObjectBehaviour>().targetPos;
            }

            if (Vector2.Distance(originHP, impactPos) <= Vector2.Distance(targetHP, impactPos))
            {
                impactPosProj = impactPos + (new Vector2(-(impactPos.x - originHP.x), -(impactPos.y - originHP.y)));

                if (Vector2.Distance(originHP, impactAngle) <= Vector2.Distance(impactPosProj, impactAngle))
                {
                    float angleForCase1;
                    angleForCase1 = Vector3.SignedAngle(impactAngle, originHP, impactPos);
                    //bounceVector = Quaternion.AngleAxis(2 * angleForCase1, new Vector3(0, 0, 1)) * impactAngle;
                    bounceVector = Quaternion.AngleAxis(180 - 2 * angleForCase1, new Vector3(0, 0, 1)) * impactAngle;
                    Debug.Log("case1");
                }
                else
                {
                    float angleForCase3;
                    angleForCase3 = Vector3.SignedAngle(impactAngle, impactPosProj, impactPos);
                    //bounceVector = Quaternion.AngleAxis(-2 * angleForCase3, new Vector3(0, 0, 1)) * impactAngle;
                    bounceVector = Quaternion.AngleAxis(180 - 2 * angleForCase3, new Vector3(0, 0, 1)) * impactAngle;
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
                    //bounceVector = Quaternion.AngleAxis(-2 * angleForCase2, new Vector3(0, 0, 1)) * impactAngle;
                    bounceVector = Quaternion.AngleAxis(180 - 2 * angleForCase2, new Vector3(0, 0, 1)) * impactAngle;
                    Debug.Log("case2");
                }
                else
                {
                    float angleForCase3;
                    angleForCase3 = Vector3.SignedAngle(impactAngle, impactPosProj, impactPos);
                    //bounceVector = Quaternion.AngleAxis(-2 * angleForCase3, new Vector3(0, 0, 1)) * impactAngle;
                    bounceVector = Quaternion.AngleAxis(180 - 2 * angleForCase3, new Vector3(0, 0, 1)) * impactAngle;
                    Debug.Log("case3");
                }
            }

            apple.GetComponent<ScannableObjectBehaviour>().targetPos = new Vector2(bounceVector.x * bounceAmount, bounceVector.y * bounceAmount);
            */

            apple.GetComponent<IMP_Apple>().appleProjectionSpeed = bounceAmountApple;
            apple.GetComponent<ScannableObjectBehaviour>().targetPos = (Random.insideUnitCircle * errorMargin) + (Vector2)actualThreat.position;            

            //Debug
            impactPosDebug = impactPos;
            appleTargetPos = apple.GetComponent<ScannableObjectBehaviour>().targetPos;

            yield return new WaitForSeconds(bounceTimeApple);

            ResetHookpoints();
        }

        IEnumerator PlayerBounce(Vector2 originHP, Vector2 targetHP, Vector2 impactPos)
        {
            myLineRend.enabled = false;
            mark = true;

            player.GetComponent<PlayerControllerIso>().playerCanMove = false;

            /*
            if (!impactAngleSet)
            {
                impactAngle = player.GetComponent<PlayerControllerIso>().lastMove;
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
                    bounceVector = Quaternion.AngleAxis(-2 * angleForCase1, new Vector3(0, 0, 1)) * impactAngle;
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
            */

            player.GetComponent<PlayerControllerIso>().moveInput = new Vector2(-player.GetComponent<PlayerControllerIso>().moveInput.x, -player.GetComponent<PlayerControllerIso>().moveInput.y).normalized * bounceAmountPlayer;

            player.GetComponent<SpriteRenderer>().flipX = true; //test feedback

            yield return new WaitForSeconds(bounceTimePlayer);

            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<PlayerControllerIso>().playerCanMove = true;

            player.GetComponent<SpriteRenderer>().flipX = false; //test feedback

            ResetHookpoints();
        }

        IEnumerator HammerheadBounce(GameObject hammerhead)
        {
            bool hhOriginRushSpeedStored = false;
            float originalHammerheadRushSpeed = 0;

            if (!hhOriginRushSpeedStored)
            {
                originalHammerheadRushSpeed = hammerhead.GetComponent<GorillaBehaviour>().rushSpeed;
                hhOriginRushSpeedStored = true;
            }

            myLineRend.enabled = false;
            mark = true;
            hammerhead.GetComponent<GorillaBehaviour>().rushSpeed = -hammerhead.GetComponent<GorillaBehaviour>().rushSpeed * bounceAmountHammerhead;
            yield return new WaitForSeconds(bounceTimeHammerhead);
            hammerhead.GetComponent<GorillaBehaviour>().rushSpeed = originalHammerheadRushSpeed;
            ResetHookpoints();
        }
    }
}