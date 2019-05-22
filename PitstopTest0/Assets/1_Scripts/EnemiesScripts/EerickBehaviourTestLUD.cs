﻿using System.Collections;
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

        [Header("Rage Mode 2")]
        [SerializeField] float timeBeforeRageMode2 = 6f;
        [SerializeField] float durationOfRageMode2 = 0.5f;
        [SerializeField] float cooldownDuringRage2 = 0.05f;

        bool secondPhaseTriggered = false;


        private void Start()
        {
            myRb = GetComponent<Rigidbody2D>();
            enemyHealthManager = GetComponent<EnemyHealthManager>();


            transform.position = positionPoints[0].position;

            //tp toi au starting point

            storedHealth = enemyHealthManager.enemyMaxHealth;
            goBackToFightSpeedStored = goBackToFightSpeed;

            //StartCoroutine(RageManagement());


        }

        private void Update()
        {
            if (!fightCanStart)
            {
                if (target.canMove && playerHasTriggeredNative)
                {
                    fightCanStart = true;
                    StartCoroutine(RageManagement());
                }
            }
            else
            {
                if (enemyHealthManager.enemyCurrentHealth <= enemyHealthManager.enemyMaxHealth / 2 && !secondPhaseTriggered)
                {
                    secondPhaseTriggered = true;
                    //anim de 2eme phase

                }

                ThrowProjAtTarget();

                //LostHealthConsequence();

                ChangeSpotIfNeeded();
                
            }
        }


            
        void ChangeSpotIfNeeded()
        {
            if (haveToChangeItsSpot || enemyHealthManager.enemyCurrentHealth < storedHealth)   
            {

                if (!backToFightPosSet)
                {
                    myRb.velocity = Vector2.zero;
                    goBackToFightSpeed = goBackToFightSpeedStored;

                    /*
                    //CHANGE OF INDEX MODIFICATOR\\
                    int modificator = Random.Range(-1, 2);  //the maximum is exclusive if the random.range pick an integer
                    while (modificator == 0) modificator = Random.Range(-1, 2);       //so only have -1 or +1
                    
                    indexOfTargetedPosition += modificator;

                    if (indexOfTargetedPosition < 0) indexOfTargetedPosition = positionPoints.Length - 1;
                    else if (indexOfTargetedPosition > positionPoints.Length - 1) indexOfTargetedPosition = 0;
                    //----\\
                    */

                    //NEW DESTINATION DEPENDS ON THE DISTANCE TO THE PLAYER\\

                    int superiorIndex = indexOfTargetedPosition + 1;
                    if (superiorIndex > positionPoints.Length - 1) superiorIndex = 0;

                    int inferiorIndex = indexOfTargetedPosition - 1;
                    if (inferiorIndex < 0) inferiorIndex = positionPoints.Length - 1;

                    if ((positionPoints[inferiorIndex].position - this.transform.position).magnitude < (positionPoints[superiorIndex].position - this.transform.position).magnitude)
                    {
                        indexOfTargetedPosition = inferiorIndex;
                    }
                    else
                    {
                        indexOfTargetedPosition = superiorIndex;
                    }

                        

                    //---\\

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
            if (secondPhaseTriggered)
            {
                yield return new WaitForSeconds(timeBeforeRageMode2);

                cooldown = cooldownDuringRage2;

                yield return new WaitForSeconds(durationOfRageMode2);

                cooldown = cooldownAtTheOrigin;

                StartCoroutine(RageManagement());
            }
            else
            {
                yield return new WaitForSeconds(timeBeforeRageMode);

                cooldown = cooldownDuringRage;

                yield return new WaitForSeconds(durationOfRageMode);

                cooldown = cooldownAtTheOrigin;

                StartCoroutine(RageManagement());
            }
            
        }
    }
}
