using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalController : MonoBehaviour
{
    InputManager inputManager;

    //SerializedField
    [SerializeField]
    float fireSpeed = 1;
    [SerializeField]
    float projSpeed = 3;
    [SerializeField]
    float scanSpeed = 1;
    [SerializeField]
    float descanSpeed = 1;
    [Range(1, 10)]
    public float maxScanRange = 5;
    [Range(1, 10), SerializeField]
    float maxShootRange = 5;

    //Private
    public bool hitting = false;
    GameObject objectHittedBefore;
    public GameObject objectHitted;
    GameObject objectOnScan;    
    float fireRate = 0;
    GameObject cloneProj;

    //Public
    public UIManager uIManager;
    public int scanProgress = 0;
    public GameObject scannedObject;
    public GameObject circularRange;
    public LayerMask raycastLayerMask;

    private void Start()
    {
        inputManager = GameManager.Instance.inputManager;

        circularRange.transform.localScale *= maxScanRange;
    }

    void Update()
    {
        Vector2 playerPos = transform.position;
        Vector2 playerPosGround = circularRange.transform.position;
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 test1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 test2 = new Vector2(test1.x, test1.y / 2);
        //Vector2 crystalDir = test2 - playerPosGround;

        Vector2 crystalDirection = cursorPos - playerPosGround;
        //Vector2 crystalOrigin = playerPosGround + crystalDirection.normalized;

        Vector2 crystalScanTarget = Vector2.ClampMagnitude(crystalDirection, maxScanRange);
        Vector2 crystalShootTarget = Vector2.ClampMagnitude(crystalDirection, maxShootRange);

        float scanRange = Vector2.Distance(playerPosGround, playerPos + crystalScanTarget);

        RaycastHit2D hit = Physics2D.Raycast(playerPosGround, crystalDirection, scanRange); //raycast's definition
        Debug.DrawRay(playerPosGround, crystalDirection, Color.red); //draws the line in scene/debug

        //SCAN
        if (hit.collider != null && inputManager.scanKey)
        {
            if (hit.collider.isTrigger && hit.collider.gameObject.GetComponent<ScannableObjectBehaviour>() != null && hit.collider.gameObject != scannedObject)
            {
                if (hit.collider.gameObject.GetComponent<ScannableObjectBehaviour>().isScannable)
                {
                    objectHittedBefore = objectHitted;
                    objectHitted = hit.transform.gameObject;

                    //If no registred objectOnScan
                    if (objectOnScan == null)
                    {
                        StopAllCoroutines();

                        //If it's a different object than the last one scanned => Reinitialise scanProgress
                        if (objectHittedBefore != null)
                        {
                            if (objectHittedBefore.tag != objectHitted.tag)
                            {
                                scanProgress = 0;
                            }
                        }

                        StartCoroutine(Scan());
                    }
                    //If hit object is the same as the registered one
                    else if (objectOnScan.tag == objectHitted.tag)
                    {
                        if (scanProgress == 5)
                        {
                            scannedObject = GameObject.FindWithTag(objectOnScan.tag);
                            uIManager.SendMessage("ChangeImageInCrystalSlot", scannedObject.GetComponent<ScannableObjectBehaviour>().associatedIcon);

                            StopAllCoroutines();
                            scanProgress = 0;
                        }
                    }
                    //If new object hitted directly => Reinitialise scanProgress
                    else
                    {
                        scanProgress = 0;
                    }

                    hitting = true;
                    objectOnScan = objectHitted;
                }                
            }
        }
        //No object hit => DESCAN
        else if (hitting)
        {
            StopAllCoroutines();
            StartCoroutine(DeScan());
            hitting = false;
            objectOnScan = null;           
        }

        //SHOOT the scanned object
        if (inputManager.shootKey && Time.time > fireRate)
        {
            if (scannedObject.name == "ScannableRoot")
            {
                return;
            }

            fireRate = Time.time + fireSpeed;

            cloneProj = (GameObject)Instantiate(scannedObject, transform.position, scannedObject.transform.rotation);

            cloneProj.GetComponent<ScannableObjectBehaviour>().targetPos = playerPos + crystalShootTarget;
            cloneProj.GetComponent<ScannableObjectBehaviour>().projectileSpeed = projSpeed;
            cloneProj.GetComponent<ScannableObjectBehaviour>().isScannable = false;
            cloneProj.GetComponent<ScannableObjectBehaviour>().isFired = true;

            

            //not optimized at all
            if (scannedObject.name == "Apple")
            {
                cloneProj.GetComponent<AppleBehaviour>().targetPos = playerPos + crystalShootTarget;
                cloneProj.GetComponent<AppleBehaviour>().projectileSpeed = projSpeed;
                cloneProj.GetComponent<AppleBehaviour>().isScannable = false;
                cloneProj.GetComponent<AppleBehaviour>().isFired = true;
            }
        }
    }

    void isoVectorTwo(Vector2 vector)
    {
        vector = new Vector2(vector.x, vector.y / 2);
    }

    IEnumerator Scan()
    {
        while (scanProgress < 5)
        {
            Debug.Log(scanProgress);
            yield return new WaitForSeconds(scanSpeed);
            scanProgress++;
        }       
    }

    IEnumerator DeScan()
    {
        while (scanProgress > 0)
        {
            Debug.Log(scanProgress);
            yield return new WaitForSeconds(descanSpeed);
            scanProgress--;            
        }
    }    
}