using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class EerickBehaviour : MonoBehaviour
    {
        [SerializeField] Transform[] positionPoints;
        [SerializeField] PlayerControllerIso target = default;
        [SerializeField] GameObject projectile = default;
        [SerializeField] Transform fromWhereTheApplesAreShot = default;
        [SerializeField] float cooldown = 2;

        bool fightCanStart = false;
        bool waitForCooldown = false;
        GameObject cloneProj;

        private void Update()
        {
            if (!fightCanStart)
            {
                if (target.canMove)
                {
                    fightCanStart = true;
                }
            }
            else
            {
                ThrowProjAtTarget();

                CheckPosition();
            }
        }

        void CheckPosition()
        {

        }

        void ThrowProjAtTarget()
        {
            if (!waitForCooldown)
            {
                StopCoroutine(ProjectileCooldown());

                cloneProj = Instantiate(projectile, fromWhereTheApplesAreShot.position, projectile.transform.rotation);

                cloneProj.GetComponent<ScannableObjectBehaviour>().targetPos = target.transform.position;
                cloneProj.GetComponent<ScannableObjectBehaviour>().projectileSpeed = projectile.GetComponent<IMP_Apple>().appleProjectionSpeed;
                cloneProj.GetComponent<ScannableObjectBehaviour>().isScannable = false;
                cloneProj.GetComponent<ScannableObjectBehaviour>().isFired = true;

                StartCoroutine(ProjectileCooldown());
            }
        }

        IEnumerator ProjectileCooldown()
        {
            waitForCooldown = true;
            yield return new WaitForSeconds(cooldown);
            waitForCooldown = false;
        }
    }
}