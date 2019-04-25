using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Crystal : MonoBehaviour
    {
        [SerializeField]
        float floatingSpeed = 0.25f;
        [SerializeField]
        float floatingRatio = 4;

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
            //not very natural looking
            transform.position = new Vector2(transform.position.x, Mathf.SmoothStep(transform.position.y, transform.position.y + heightRatio, floatingSpeed * Time.deltaTime));
        }

        void ReverseDirection()
        {
            floatingRatio = -floatingRatio;
        }

        public void Disappear()
        {
            Destroy(gameObject);
        }
    }
}