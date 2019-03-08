using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class CrystalController : MonoBehaviour
    {
        InputManager inputManager;

        //SerializedField
        [SerializeField]
        float fireSpeed = 1;
        [SerializeField]
        float scanSpeed = 1;
        [SerializeField]
        float descanSpeed = 1;
        [Range(1, 10)]
        public float maxScanRange = 5;
        [Range(1, 10), SerializeField]
        float maxShootRange = 5;
        [SerializeField]
        int maxObjectOnScene = 1;
        [SerializeField]
        SpriteRenderer previsualisation;
        [SerializeField]
        PrevizContact previsualisationContact;

        //Private
        List<GameObject> gameObjectsOnScene = new List<GameObject>();
        int objectCountOnScene;
        public bool hitting = false;
        GameObject objectHittedBefore;
        public GameObject objectHitted;
        GameObject objectOnScan;
        float fireRate = 0;
        GameObject cloneProj;

        Vector2 playerPosGround;
        Vector2 cursorPos;
        Vector2 crystalDirection;
        Vector2 crystalShootTarget;
        bool canShoot = false;

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
            playerPosGround = circularRange.transform.position;
            cursorPos = inputManager.cursorPosition;

            crystalDirection = cursorPos - playerPosGround;
            //Vector2 crystalOrigin = playerPosGround + crystalDirection.normalized;

            Vector2 crystalScanTarget = Vector2.ClampMagnitude(crystalDirection, maxScanRange);
            crystalShootTarget = Vector2.ClampMagnitude(crystalDirection, maxShootRange);

            float scanRange = Vector2.Distance(playerPosGround, (Vector2)transform.position + crystalScanTarget);

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

            //conditions for shoot
            if (canShoot && inputManager.shootKey && scannedObject != null && Time.time > fireRate)
            {
                Shoot();
            }

            if (scannedObject != null)
            {
                Previsualisation(scannedObject.GetComponent<SpriteRenderer>());
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
                    //do stuff
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

        void Previsualisation(SpriteRenderer whatToPreviz)
        {
            previsualisation.gameObject.transform.position = inputManager.cursorPosition;
            previsualisation.sprite = whatToPreviz.sprite;

            if (previsualisationContact.objectShootable)
            {
                canShoot = true;
                //previsualisation.color = new Color();
            }
            else
            {
                canShoot = false;
                previsualisation.color = Color.red;
            }
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