using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace Pitstop
{
    public class EerickBehaviourTestLUD : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Rigidbody2D myRb = default;
        [SerializeField] EnemyHealthManager enemyHealthManager = default;

        [SerializeField] Transform[] positionPoints = default;
        [SerializeField] PlayerControllerIso target = default;
        [SerializeField] GameObject projectile = default;
        [SerializeField] Transform fromWhereTheApplesAreShot = default;
        [SerializeField] float cooldown = 2;
        [SerializeField] float goBackToFightSpeed = 2;
        //[SerializeField] float regularSpeed = 2;
        [SerializeField] float errorMargin = 0.5f;
        [SerializeField] int damageDealing = 1;
        [SerializeField] float repulseTime = 1f;
        [SerializeField] CinemachineImpulseSource myImpulseSource = default;

        bool fightCanStart = false;
        public bool playerHasTriggeredNative = false;

        bool waitForCooldown = false;
        GameObject cloneProj;

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

        [Header("Health")]
        [SerializeField] GameObject healthBar = default;
        [SerializeField] Image healthGauje = default;
        [SerializeField] Image secondHealthGauje = default;
        int storedHealth;
        [SerializeField] float timeBeforeSecondBarGoDown = 0.5f;
        [SerializeField] float timeForSecondHealthBarToGoDown = 0.3f;
        bool hasTheGoDownBeenTriggered = false;

        [Header("Sounds")]
        [SerializeField] AudioSource soundDeerRegular = default;
        [SerializeField] AudioSource soundShout = default;

        [Header ("Animations")]
        [SerializeField] Animator myAnim = default;
        public Vector2 moveInput;
        public Vector2 lastMove;
        public bool isMoving = false;


        private void Start()
        {
            myRb = GetComponent<Rigidbody2D>();
            enemyHealthManager = GetComponent<EnemyHealthManager>();


            transform.position = positionPoints[0].position;


            storedHealth = enemyHealthManager.enemyMaxHealth;
            goBackToFightSpeedStored = goBackToFightSpeed;

            //StartCoroutine(RageManagement());

            //orientation of the sprite on Start
            lastMove = new Vector2(-1, 0);
            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
        }

        private void Update()
        {
            if (target.canMove)
            {

                if (!fightCanStart)
                {
                    if (playerHasTriggeredNative)
                    {
                        fightCanStart = true;
                        StartCoroutine(RageManagement());
                        healthBar.SetActive(true);
                    }
                }
                else
                {
                    Animations();


                    if (enemyHealthManager.enemyCurrentHealth <= enemyHealthManager.enemyMaxHealth / 2 && !secondPhaseTriggered)
                    {
                        secondPhaseTriggered = true;
                        //anim de 2eme phase

                    }

                    ThrowProjAtTarget();

                    //LostHealthConsequence();

                    ChangeSpotIfNeeded();

                    if (Input.GetKey(KeyCode.K) && (Input.GetKey(KeyCode.I) && (Input.GetKey(KeyCode.L)))) enemyHealthManager.enemyCurrentHealth = 0;


                }
            }
        }



        void ChangeSpotIfNeeded()
        {
            if (haveToChangeItsSpot || enemyHealthManager.enemyCurrentHealth < storedHealth)
            {

                if (!hasTheGoDownBeenTriggered && enemyHealthManager.enemyCurrentHealth < storedHealth) StartCoroutine(ModifyHealthBar());
                //StartCoroutine(ModifyHealthBar());


                if (!backToFightPosSet)
                {
                    myRb.velocity = Vector2.zero;
                    goBackToFightSpeed = goBackToFightSpeedStored;
                    

                    //NEW DESTINATION DEPENDS ON THE DISTANCE TO THE POTENTIAL POINTS\\

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
                moveInput = positionPoints[indexOfTargetedPosition].position - transform.position;
                lastMove = moveInput;
                isMoving = true;
                //Debug.DrawLine(transform.position, positionPoints[indexOfTargetedPosition].position, Color.black);

                if (transform.position == positionPoints[indexOfTargetedPosition].position)
                {

                    backToFightPosSet = false;
                    isMoving = false;
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

            if (collision.gameObject.tag == "ExplosionZoneApple" && !isBeingRepulsed)
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
                myImpulseSource.GenerateImpulse();

                soundShout.Play();

                yield return new WaitForSeconds(durationOfRageMode2);

                cooldown = cooldownAtTheOrigin;

                StartCoroutine(RageManagement());
            }
            else
            {
                yield return new WaitForSeconds(timeBeforeRageMode);

                soundDeerRegular.Play();

                cooldown = cooldownDuringRage;
                myImpulseSource.GenerateImpulse();

                yield return new WaitForSeconds(durationOfRageMode);

                cooldown = cooldownAtTheOrigin;

                StartCoroutine(RageManagement());
            }

        }

        IEnumerator ModifyHealthBar()
        {
            hasTheGoDownBeenTriggered = true;

            healthGauje.fillAmount = (enemyHealthManager.enemyCurrentHealth / (float) enemyHealthManager.enemyMaxHealth);

            yield return new WaitForSeconds(timeBeforeSecondBarGoDown);

            float stepInAnimation = 20f;
            
            for (int i = 0;i< stepInAnimation;i++)
            {
                secondHealthGauje.fillAmount -= 1 / ((float)enemyHealthManager.enemyMaxHealth * stepInAnimation);

                yield return new WaitForSeconds(timeForSecondHealthBarToGoDown / stepInAnimation);
            }
            
            secondHealthGauje.fillAmount = (enemyHealthManager.enemyCurrentHealth / (float)enemyHealthManager.enemyMaxHealth);

            hasTheGoDownBeenTriggered = false;

        }


        public void Animations()
        {
            myAnim.SetBool("IsMoving", isMoving);
            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
            myAnim.SetFloat("MoveX", moveInput.x);
            myAnim.SetFloat("MoveY", moveInput.y);
        }

    }
}
