﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class RootBehaviour : MonoBehaviour
    {
        public LineRenderer liana;
        public GameObject player;
        public GameObject target;
        public bool test = false;

        int layer_mask;

        [SerializeField]
        float lifeInSeconds;

        public int damageDealing = 1;

        //public EnemyHealthManager bossHealth;
        public CrystalController crys;
        public ScannableObjectBehaviour scannableObjBeh;
        private bool living;

        private void Awake()
        {
            liana = this.GetComponent<LineRenderer>();
            layer_mask = LayerMask.GetMask("Bear");
        }

        private void Update()
        {
            //RaycastHit2D hitPoint = Physics2D.Raycast(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), crys.maxScanRange);
            //Debug.DrawLine(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.blue);

            if (scannableObjBeh.isFired)
            {
                if (target != null && target.tag == "HookPoint")
                {
                    RaycastHit2D trip = Physics2D.Raycast(player.transform.position, target.transform.position, layer_mask);
                    Debug.DrawLine(player.transform.position, target.transform.position, Color.green);
                    liana.enabled = true;
                    liana.SetPosition(0, player.transform.position);
                    liana.SetPosition(1, target.transform.position);

                    /*if (trip.collider.gameObject.name == "Gorilla")
                    {
                        Debug.Log("HIT");
                        bossHealth.HurtEnemy(damageDealing);
                        Debug.Log("dead");
                        liana.enabled = false;
                        Destroy(gameObject);
                    }*/
                }

                else
                {
                    Debug.Log("Nope Rope !");
                }
            }

            if (scannableObjBeh.isArrived)
            {
                StartCoroutine(Life());
            }
        }

        IEnumerator Life()
        {
            while (lifeInSeconds > 0)
            {
                yield return new WaitForSeconds(1);
                lifeInSeconds--;
                Debug.Log("dead");
                liana.enabled = false;
                Destroy(gameObject);
            }
        }
    }
}