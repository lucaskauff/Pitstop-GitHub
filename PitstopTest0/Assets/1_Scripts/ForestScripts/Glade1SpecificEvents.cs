using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade1SpecificEvents : MonoBehaviour
    {
        [SerializeField] EnemySpawner hHNest = default;

        private bool playerEnteredCheck = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !playerEnteredCheck)
            {
                Debug.Log("Player entered Glade1.");
                hHNest.SpawnTheThing();
                playerEnteredCheck = true;
            }
        }
    }
}