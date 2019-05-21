using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ScannableObjectBehaviour : MonoBehaviour
    {
        public Sprite associatedIcon;
        public bool isScannable = true;
        public bool isFired = false;
        public bool isArrived = false;
        public float projectileSpeed = 3;
        public Vector2 targetPos;


        //public ScanData entity;

        public bool col = false;

        [Header("ParametersForDialogueWheel")]      //Add by Luc D.
        public bool isAWord = false;
        public int valueOfTheWorld = 0;

        private void Update()
        {
            if (isFired)
            {
                
                if (gameObject.tag != "ObjectRoot")
                {
                    
                    Shoot();
                }
            }

            if (col || new Vector2(transform.position.x, transform.position.y) == targetPos)
            {
                isArrived = true;
                isFired = false;

                //GetComponentInChildren<AudioSource>().Stop();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (isFired && other.gameObject.name != "Zayn")
            {
                col = true;
            }
        }

        public void Shoot()
        {
            if (!GetComponentInChildren<AudioSource>().isPlaying) GetComponentInChildren<AudioSource>().Play();
            transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);
            
        }
    }
}