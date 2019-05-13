using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class EerickBehaviourTestLUD : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Rigidbody2D myRb = default;
        [SerializeField] EnemyHealthManager enemyHealthManager = default;

        [SerializeField] Transform[] positionPoints;
        [SerializeField] PlayerControllerIso target = default;
        [SerializeField] GameObject projectile = default;
        [SerializeField] Transform fromWhereTheApplesAreShot = default;
        [SerializeField] float cooldown = 2;
        [SerializeField] float goBackToFightSpeed = 2;
        //[SerializeField] float regularSpeed = 2;
        [SerializeField] float errorMargin = 0.5f;
        [SerializeField] int damageDealing = 1;
        [SerializeField] float repulseTime = 1f;

        bool fightCanStart = false;
        public bool playerHasTriggeredNative = false;

        bool waitForCooldown = false;
        GameObject cloneProj;

        int storedHealth;
        bool backToFightPosSet = false;
        Transform backToFightPos;
        float goBackToFightSpeedStored;
        bool isBeingRepulsed = false;
        bool backAfterRepulse = false;

        int indexOfTargetedPosition = 0;
        public bool haveToChangeItsSpot = false;

        [Header("Rage Mode")]
        [SerializeField] float timeBeforeRageMode = 5f;
        [SerializeField] float durationOfRageMode = 1f;
        [SerializeField] float cooldownDuringRage = 0.2f;
        [SerializeField] float cooldownAtTheOrigin = 2f;



        private void Start()
        {
            myRb = GetComponent<Rigidbody2D>();
            enemyHealthManager = GetComponent<EnemyHealthManager>();


            transform.position = positionPoints[0].position;

            storedHealth = enemyHealthManager.enemyMaxHealth;
            goBackToFightSpeedStored = goBackToFightSpeed;

            StartCoroutine(RageManagement());


        }

        private void Update()
        {
            if (!fightCanStart)
            {
                if (target.canMove && playerHasTriggeredNative)
                {
                    fightCanStart = true;
                }
            }
            else
            {
                ThrowProjAtTarget();

                //LostHealthConsequence();

                ChangeSpotIfNeeded();
                
            }
        }

        /*
        void LostHealthConsequence()
        {
            if (enemyHealthManager.enemyCurrentHealth < storedHealth || backAfterRepulse)
            {
                
                if (!backToFightPosSet)
                {
                    myRb.velocity = Vector2.zero;
                    goBackToFightSpeed = goBackToFightSpeedStored;
                    backToFightPos = positionPoints[Mathf.RoundToInt(Random.Range(0, positionPoints.Length - 1))];/////
                    backToFightPosSet = true;
                    goBackToFightSpeed = Vector2.Distance(transform.position, backToFightPos.position) * goBackToFightSpeed;
                }

                transform.position = Vector2.MoveTowards(transform.position, backToFightPos.position, goBackToFightSpeed * Time.deltaTime);
                Debug.Log("A avancé");
                Debug.DrawLine(transform.position, backToFightPos.position, Color.black);

                if (transform.position == backToFightPos.position)
                {
                    storedHealth = enemyHealthManager.enemyCurrentHealth;
                    backToFightPosSet = false;
                    backAfterRepulse = false;
                }
                
            }
        }
        */

            
        void ChangeSpotIfNeeded()
        {
            if (haveToChangeItsSpot || enemyHealthManager.enemyCurrentHealth < storedHealth)   
            {

                if (!backToFightPosSet)
                {
                    myRb.velocity = Vector2.zero;
                    goBackToFightSpeed = goBackToFightSpeedStored;

                    //CHANGE OF INDEX MODIFICATOR\\
                    int modificator = Random.Range(-1, 2);  //the maximum is exclusive if the random.range pick an integer
                    while (modificator == 0) modificator = Random.Range(-1, 2);       //so only have -1 or +1
                    
                    indexOfTargetedPosition += modificator;

                    if (indexOfTargetedPosition < 0) indexOfTargetedPosition = positionPoints.Length - 1;
                    else if (indexOfTargetedPosition > positionPoints.Length - 1) indexOfTargetedPosition = 0;
                    //----\\

                    backToFightPosSet = true;
                    goBackToFightSpeed = Vector2.Distance(transform.position, positionPoints[indexOfTargetedPosition].position) * goBackToFightSpeed;
                }

                transform.position = Vector2.MoveTowards(transform.position, positionPoints[indexOfTargetedPosition].position, goBackToFightSpeed * Time.deltaTime);   //mettre dans l'Update directement
                Debug.DrawLine(transform.position, positionPoints[indexOfTargetedPosition].position, Color.black);

                if (transform.position == positionPoints[indexOfTargetedPosition].position)
                {
                    
                    backToFightPosSet = false;
                    haveToChangeItsSpot = false;
                    storedHealth = enemyHealthManager.enemyCurrentHealth;
                }

            }
        }
        



        void ThrowProjAtTarget()
        {
            if (!waitForCooldown)
            {
                StopCoroutine(ProjectileCooldown());

                cloneProj = Instantiate(projectile, fromWhereTheApplesAreShot.position, projectile.transform.rotation);

                cloneProj.GetComponent<ScannableObjectBehaviour>().targetPos = target.gameObject.transform.position;
                cloneProj.GetComponent<ScannableObjectBehaviour>().projectileSpeed = projectile.GetComponent<IMP_Apple>().appleProjectionSpeed;
                cloneProj.GetComponent<ScannableObjectBehaviour>().isScannable = false;
                cloneProj.GetComponent<ScannableObjectBehaviour>().isFired = true;

                StartCoroutine(ProjectileCooldown());
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageDealing);
            }

            /*
            if (collision.gameObject.tag == "ObjectApple" && !isBeingRepulsed)
            {
                StartCoroutine(ComeOnAndFly());
            }*/

        }

        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectApple" && !isBeingRepulsed)
            {
                StartCoroutine(ComeOnAndFly());
            }
        }

        IEnumerator ProjectileCooldown()
        {
            waitForCooldown = true;
            yield return new WaitForSeconds(cooldown);
            waitForCooldown = false;
        }

        IEnumerator ComeOnAndFly()
        {
            isBeingRepulsed = true;
            yield return new WaitForSeconds(repulseTime);
            myRb.velocity = Vector2.zero;
            isBeingRepulsed = false;
            backAfterRepulse = true;

            haveToChangeItsSpot = true;

            StopCoroutine(ComeOnAndFly());
        }

        IEnumerator RageManagement()
        {
            yield return new WaitForSeconds(timeBeforeRageMode);

            cooldown = cooldownDuringRage;

            yield return new WaitForSeconds(durationOfRageMode);

            cooldown = cooldownAtTheOrigin;

            StartCoroutine(RageManagement());
        }
    }
}
