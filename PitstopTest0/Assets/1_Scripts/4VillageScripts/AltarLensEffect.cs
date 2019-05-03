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
        [SerializeField] float chromAberrPropSpeed = 0.75f;

        ChromaticAberration chromaticAberration = null;
        float t;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StopCoroutine(PostProDisparition());
                StartCoroutine(PostProApparition());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StopCoroutine(PostProApparition());
                StartCoroutine(PostProDisparition());
            }
        }

        IEnumerator PostProApparition()
        {
            while (t < chromAberrAmount)
            {
                postProChromAberr.profile.TryGetSettings(out chromaticAberration);
                chromaticAberration.intensity.value = Mathf.Lerp(0, chromAberrAmount, t);
                t += chromAberrPropSpeed * Time.deltaTime;
                yield return null;
            }
        }

        IEnumerator PostProDisparition()
        {
            while (t > 0)
            {
                postProChromAberr.profile.TryGetSettings(out chromaticAberration);
                chromaticAberration.intensity.value = Mathf.Lerp(0, chromAberrAmount, t);
                t -= chromAberrPropSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
}