using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_MuteForATime : MonoBehaviour
    {
        [SerializeField] float muteTime = 1f;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        IEnumerator MuteTime()
        {
            GetComponent<AudioSource>().mute = true;
            yield return new WaitForSeconds(muteTime);
            GetComponent<AudioSource>().mute = false;
        }
    }
}
