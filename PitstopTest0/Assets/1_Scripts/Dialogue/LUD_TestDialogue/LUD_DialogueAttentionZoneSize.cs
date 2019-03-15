using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_DialogueAttentionZoneSize : MonoBehaviour
    {

        //SerializeField
        [SerializeField]
        float timeNeededToReturnToNormal = 2f;
        [SerializeField]
        [Range(0, 20)]
        int timeNeededToExpand = 8;


        //Public
        public float shoutPower = 2f;
        public float delayBeforeReturnToNormal = 1f;


        //Private
        float timer;
        bool isBig;
        Vector3 initialScale;

        // Start is called before the first frame update
        void Start()
        {
            //INITIALISATION DES VALEURS
            isBig = false;
            timer = 0f;
            initialScale = this.transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (isBig)
            {
                if (timer > delayBeforeReturnToNormal)
                {

                    if (timer - delayBeforeReturnToNormal < timeNeededToReturnToNormal)
                    {
                        this.transform.localScale = initialScale + initialScale * (1 - ((timer - delayBeforeReturnToNormal) / timeNeededToReturnToNormal)) * (shoutPower - 1);
                        timer += Time.deltaTime;
                        //Debug.Log("timer - delayBeforeReturnToNormal = " + (1-((timer - delayBeforeReturnToNormal) / timeNeededToReturnToNormal)));
                    }
                    else
                    {
                        isBig = false;
                        timer = 0f;
                        this.transform.localScale = initialScale;
                    }

                }
                else
                {
                    timer += Time.deltaTime;
                }
            }

        }

        public void ShoutEffect()
        {

            isBig = true;
            timer = 0f;
            this.transform.localScale = initialScale;
            //this.transform.localScale *= shoutPower; //ADD UNE ANIMATION (pas que ce soit aussi direct)
            StartCoroutine("Expansion");

        }

        IEnumerator Expansion()
        {


            for (float i = 0f; i <= timeNeededToExpand; ++i)
            {
                this.transform.localScale = initialScale + initialScale * (i / timeNeededToExpand) * (shoutPower - 1);

                //yield return new WaitForSeconds(timeNeededToExpand/maxI);
                yield return null;
            }
        }
    }
}