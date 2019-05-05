using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LinesMaskBehaviour : MonoBehaviour
    {
        [SerializeField] CrystalAltarRecuperation crystalAltar = default;
        [SerializeField] float propSpeed = 1;
        [SerializeField] float maxSize = 2.15f;
        [SerializeField] float minSize = 0.25f;

        float t;
        float currentSize;

        bool switchDone = false;

        private void Start()
        {
            t = maxSize;
            currentSize = maxSize;

            StartCoroutine(BeamTowardsCrystalAltar());
        }

        private void Update()
        {
            transform.localScale = new Vector3(currentSize, currentSize, currentSize);

            if (crystalAltar.triggerOnceCheck && !switchDone)
            {
                Reset(minSize);

                StopCoroutine(BeamTowardsCrystalAltar());
                StartCoroutine(BeamTowardsExitDoor());

                switchDone = true;
            }
        }

        private void Reset(float floatForReset)
        {
            transform.localScale = Vector3.one * floatForReset;
            t = 0;
            currentSize = floatForReset;
        }

        IEnumerator BeamTowardsCrystalAltar()
        {
            t = 0;

            while (t < propSpeed)
            {
                t += Time.deltaTime;

                currentSize = Mathf.Lerp((maxSize+currentSize*2)/3, minSize, t / propSpeed);

                yield return null;
            }

            Reset(maxSize);

            if (!switchDone)
            {
                StartCoroutine(BeamTowardsCrystalAltar());
            }
        }

        IEnumerator BeamTowardsExitDoor()
        {
            t = 0;

            while (t < propSpeed)
            {
                t += Time.deltaTime;

                currentSize = Mathf.Lerp(minSize, maxSize, t / propSpeed);

                yield return null;
            }

            Reset(minSize);
            StartCoroutine(BeamTowardsExitDoor());
        }
    }
}