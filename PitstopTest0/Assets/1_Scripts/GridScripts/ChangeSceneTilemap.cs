﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ChangeSceneTilemap : MonoBehaviour
    {
        [SerializeField]
        LevelChanger levelChanger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn")
            {
                levelChanger.LevelChanging();
            }
        }
    }
}