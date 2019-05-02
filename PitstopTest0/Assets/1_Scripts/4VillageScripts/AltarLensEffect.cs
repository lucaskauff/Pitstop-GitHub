using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Pitstop
{
    public class AltarLensEffect : MonoBehaviour
    {
        [SerializeField] PostProcessVolume postProChromAberr = default;
        [SerializeField, Range(0, 1)] float chromAberrAmount = 1;

        ChromaticAberration chromaticAberration = null;
        float t;
        bool chromAberrhasBeenSet = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            t = 0;
            StopCoroutine(PostProDisparition());
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !chromAberrhasBeenSet)
            {
                postProChromAberr.profile.TryGetSettings(out chromaticAberration);
                chromaticAberration.intensity.value = Mathf.Lerp(0, chromAberrAmount, t);
                t += 0.5f * Time.deltaTime;

                if (chromaticAberration.intensity.value >= chromAberrAmount)
                {
                    chromAberrhasBeenSet = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            StartCoroutine(PostProDisparition());
            chromAberrhasBeenSet = false;
        }

        IEnumerator PostProDisparition()
        {
            while (t>0)
            {
                postProChromAberr.profile.TryGetSettings(out chromaticAberration);
                chromaticAberration.intensity.value = Mathf.Lerp(0, chromAberrAmount, t);
                t -= 0.5f * Time.deltaTime;

                yield return null;
            }
        }
    }
}