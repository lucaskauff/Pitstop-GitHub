using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DialogueWheelTuto : MonoBehaviour
    {
        [SerializeField, Tooltip("Angle in Degrees")] private float startingAngle;
        [SerializeField, Tooltip("Angle in Degrees")] private float endingAngle;
        [SerializeField] private float speed;
        [SerializeField] private Transform center;
        [SerializeField] private float radius;
        [SerializeField, Tooltip("In seconds.")] private float delay;

        private float angle;
        private float time;
        private bool inReset;

        private void Awake()
        {
            angle = startingAngle;
            time = 0;
        }

        private void Update()
        {
            if(!inReset) AngleVariation();
            DoTheMove();
        }

        public void DoTheMove()
        {
            angle = Mathf.SmoothStep(startingAngle, endingAngle, time);
            float radian = angle * Mathf.Deg2Rad;
            transform.position = center.transform.position + (new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0)).normalized * radius;
        }

        public void AngleVariation()
        {
            if(Mathf.Abs(angle-endingAngle)<0.1)
            {
                StartCoroutine(DelayingReset());
            }
            else
            {
                time += Time.deltaTime * speed;
            }
        }

        private IEnumerator DelayingReset()
        {
            inReset = true;

            yield return new WaitForSeconds(delay);

            time = 0;
            inReset = false;
        }
    }
}