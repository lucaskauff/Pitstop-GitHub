using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_MuteForATime : MonoBehaviour
    {
        [SerializeField] float muteTime = 1f;
        
        void Awake()
        {
            StartCoroutine(MuteTime());
        }

        IEnumerator MuteTime()
        {
            Debug.Log("blbl");
            GetComponent<AudioSource>().mute = true;
            yield return new WaitForSeconds(muteTime);
            GetComponent<AudioSource>().mute = false;
        }
    }
}
