using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ChangeSceneTilemap : MonoBehaviour
    {
        [Header("Serializable")]
        [SerializeField] LevelChanger levelChanger = default;
        public bool active = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && active)
            {
                collision.gameObject.GetComponent<PlayerControllerIso>().ResetSavingPoint();
                levelChanger.LevelChanging();
            }
        }
    }
}