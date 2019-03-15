using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Crystal : MonoBehaviour
    {
        [SerializeField]
        float floatingSpeed;
        [SerializeField]
        float floatingRatio;

        float minPos;
        float maxPos;

        private void Start()
        {
            minPos = transform.position.y - floatingRatio;
            maxPos = transform.position.y + floatingRatio;

            Float(floatingRatio);
        }

        private void Update()
        {
            Float(floatingRatio);

            if ((transform.position.y <= minPos && floatingRatio < 0) || (transform.position.y >= maxPos && floatingRatio > 0))
            {
                ReverseDirection();
            }
        }

        void Float(float heightRatio)
        {
            //Version 1 (not very natural looking)
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + heightRatio), floatingSpeed * Time.deltaTime);

            //Version 2 (still not perfect)
            transform.position = new Vector2(transform.position.x, Mathf.SmoothStep(transform.position.y, transform.position.y + heightRatio, floatingSpeed * Time.deltaTime));
        }
        
        void ReverseDirection()
        {
            floatingRatio = -floatingRatio;
        }
    }
}