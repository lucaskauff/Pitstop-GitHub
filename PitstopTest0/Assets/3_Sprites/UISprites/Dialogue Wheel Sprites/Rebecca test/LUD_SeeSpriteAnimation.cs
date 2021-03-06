﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{

    public class LUD_SeeSpriteAnimation : MonoBehaviour
    {

        public Sprite seeSprite1;
        public Sprite seeSprite2;
        public float timeBetweenTwoSprites = 1f;
        float timer = 0f;

        bool isFirstFrameDisplay = true;

        private void Start()
        {
            GetComponent<Image>().sprite = seeSprite1;
        }

        private void Update()
        {
            if (timer>=timeBetweenTwoSprites)
            {
                if (isFirstFrameDisplay)
                {
                    GetComponent<Image>().sprite = seeSprite1;
                }
                else
                {
                    GetComponent<Image>().sprite = seeSprite2;
                }

                isFirstFrameDisplay = !isFirstFrameDisplay;

                timer = 0f;
            }

            timer += Time.deltaTime;
        }

        /*
        IEnumerator WaitBeforeChangeSprite()
        {
            yield return new WaitForSeconds(timeBetweenTwoSprites);
            
            if (isFirstFrameDisplay)
            {
                GetComponent<Image>().sprite = seeSprite1;
            }
            else
            {
                GetComponent<Image>().sprite = seeSprite2;
            }

            isFirstFrameDisplay = !isFirstFrameDisplay;
            
            StartCoroutine(WaitBeforeChangeSprite());
        }
        */
    }

}
