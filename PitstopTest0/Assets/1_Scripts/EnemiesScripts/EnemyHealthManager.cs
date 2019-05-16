using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class EnemyHealthManager : MonoBehaviour
    {
        //Public
        public int enemyMaxHealth = 3;
        public int enemyCurrentHealth;

        //Serializable
        [SerializeField] Animator myAnim = default;

        void Start()
        {
            ResetHealth();
        }

        void Update()
        {
            if (enemyCurrentHealth <= 0)
            {
                gameObject.SetActive(false);
                FindObjectOfType<LUD_ScenarioOfTheMiniBossWithTheNative>().EndOfTheFightConcequences();
            }

            if (enemyCurrentHealth > enemyMaxHealth)
            {
                enemyCurrentHealth = enemyMaxHealth;
            }
        }

        public void HurtEnemy(int damageToGive)
        {

            enemyCurrentHealth -= damageToGive;
            myAnim.SetTrigger("Hit");
        }

        public void HealEnemy(int healToGive)
        {
            enemyCurrentHealth += healToGive;
        }

        public void ResetHealth()
        {
            enemyCurrentHealth = enemyMaxHealth;
        }
    }
}