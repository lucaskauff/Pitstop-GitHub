using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class CrystalController : MonoBehaviour
    {
        InputManager inputManager;

        //Public
        public UIManager uIManager;
        public int scanProgress = 0;
        public GameObject scannedObject;
        public GameObject circularRange;
        public LayerMask raycastLayerMask;

        //SerializedField
        [SerializeField]
        float fireSpeed = 1;
        [SerializeField]
        float scanSpeed = 1;
        [SerializeField]
        float descanSpeed = 1;
        [Range(1, 10)]
        public float maxScanRange = 3;
        [Range(1, 10), SerializeField]
        float maxShootRange = 3;
        [SerializeField]
        int maxObjectOnScene = 1;
        [SerializeField]
        SpriteRenderer previsualisation = default;
        [SerializeField]
        PrevizContact previsualisationContact = default;
        [SerializeField]
        float previzAlphaRatio = 0.2f;

        //Private
        List<GameObject> gameObjectsOnScene = new List<GameObject>();
        int objectCountOnScene;
        public bool hitting = false;
        GameObject objectHittedBefore;
        public GameObject objectHitted = default;
        GameObject objectOnScan;
        float fireRate = 0;

        public GameObject cloneProj;
        Vector2 playerPosGround;
        Vector2 cursorPos;
        Vector2 crystalDirection;
        Vector2 crystalScanTarget;
        Vector2 crystalShootTarget;
        bool canShoot = false;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            circularRange.transform.localScale *= maxScanRange;
        }

        void Update()
        {
            playerPosGround = circularRange.transform.position;
            cursorPos = inputManager.cursorPosition;

            crystalDirection = cursorPos - playerPosGround;
            //Vector2 crystalOrigin = playerPosGround + crystalDirection.normalized;

            crystalScanTarget = Vector2.ClampMagnitude(crystalDirection, maxScanRange);
            //crystalShootTarget = Vector2.ClampMagnitude(crystalDirection, maxShootRange);

            if (scannedObject != null)
            {
                Previsualisation(scannedObject.GetComponent<SpriteRenderer>());
            }

            float scanRange = Vector2.Distance(playerPosGround, (Vector2)transform.position + crystalScanTarget);

            RaycastHit2D scanRay = Physics2D.Raycast(playerPosGround, crystalDirection, scanRange); //raycast's definition
            //Debug.DrawRay(playerPosGround, crystalDirection, Color.red); //draws the line in scene/debug

            //SCAN
            if (scanRay.collider != null && inputManager.rightClickBeingPressed)
            {
                if (scanRay.collider.isTrigger && scanRay.collider.gameObject.GetComponent<ScannableObjectBehaviour>() != null && scanRay.collider.gameObject != scannedObject)
                {
                    if (scanRay.collider.gameObject.GetComponent<ScannableObjectBehaviour>().isScannable)
                    {
                        objectHittedBefore = objectHitted;
                        objectHitted = scanRay.transform.gameObject;

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
                                uIManager.ChangeImageInCrystalSlot(scannedObject.GetComponent<ScannableObjectBehaviour>().associatedIcon);

                                FindObjectOfType<LUD_PreviewOfScannedObject>().ChangePreviewCrystalInDialogueWheel(scannedObject.GetComponent<ScannableObjectBehaviour>().associatedIcon, scannedObject.GetComponent<ScannableObjectBehaviour>().valueOfTheWorld, scannedObject.GetComponent<ScannableObjectBehaviour>().isAWord);

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

            //conditions for shoot
            if (canShoot && inputManager.onLeftClick && scannedObject != null && Time.time > fireRate)
            {   
                Shoot();
            }
        }

        void Previsualisation(SpriteRenderer whatToPreviz)
        {
            Vector2 shootTargetTest = crystalDirection.normalized * maxShootRange;
            shootTargetTest = new Vector2(shootTargetTest.x, shootTargetTest.y / 2);

            if (Vector2.Distance(playerPosGround, cursorPos) <= shootTargetTest.magnitude)
            {
                crystalShootTarget = crystalDirection;
            }
            else
            {
                crystalShootTarget = shootTargetTest;
            }           

            switch (scannedObject.tag)
            {
                case "ObjectLiana":
                    return;

                case "ObjectRock":
                    previsualisation.gameObject.transform.position = playerPosGround + crystalShootTarget;
                    previsualisation.sprite = whatToPreviz.sprite;

                    if (previsualisationContact.objectShootable)
                    {
                        canShoot = true;
                        previsualisation.color = new Color(0, 1, 0, previzAlphaRatio); //if he can shoot the scannedObject : color of the previz = green
                    }
                    else
                    {
                        canShoot = false;
                        previsualisation.color = new Color(1, 0, 0, previzAlphaRatio); //if he can't shoot the scannedObject : color of the previz = red
                    }
                    break;

                case "ObjectApple":
                    previsualisation.gameObject.transform.position = playerPosGround + crystalShootTarget;
                    previsualisation.sprite = whatToPreviz.sprite;

                    if (previsualisationContact.objectShootable)
                    {
                        canShoot = true;
                        previsualisation.color = new Color(0, 1, 0, previzAlphaRatio); //if he can shoot the scannedObject : color of the previz = green
                    }
                    else
                    {
                        canShoot = false;
                        previsualisation.color = new Color(1, 0, 0, previzAlphaRatio); //if he can't shoot the scannedObject : color of the previz = red
                    }
                    break;
            }
        }

        void Shoot()
        {
            switch (scannedObject.tag)
            {
                case "ObjectLiana":
                    return;

                case "ObjectRock":
                    ShootableObject((Vector2)transform.position + crystalShootTarget + new Vector2(0, scannedObject.GetComponent<RockBehaviour>().heightWhereToSpawn), scannedObject.GetComponent<RockBehaviour>().fallSpeed);
                    break;

                case "ObjectApple":
                    ShootableObject((Vector2)transform.position, scannedObject.GetComponent<IMP_Apple>().appleProjectionSpeed);
                    break;
            }
        }

        void ShootableObject(Vector2 objectSpawnPoint, float projSpeed)
        {
            if (objectCountOnScene == maxObjectOnScene)
            {
                foreach (var obj in gameObjectsOnScene)
                {
                    Destroy(obj);
                }

                objectCountOnScene = 0;
                gameObjectsOnScene.Clear();
            }

            cloneProj = Instantiate(scannedObject, objectSpawnPoint, scannedObject.transform.rotation);

            cloneProj.GetComponent<ScannableObjectBehaviour>().targetPos = (Vector2)transform.position + crystalShootTarget;
            cloneProj.GetComponent<ScannableObjectBehaviour>().projectileSpeed = projSpeed;
            cloneProj.GetComponent<ScannableObjectBehaviour>().isScannable = false;
            cloneProj.GetComponent<ScannableObjectBehaviour>().isFired = true;

            objectCountOnScene += 1;
            gameObjectsOnScene.Add(cloneProj);
            fireRate = Time.time + fireSpeed;
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
}