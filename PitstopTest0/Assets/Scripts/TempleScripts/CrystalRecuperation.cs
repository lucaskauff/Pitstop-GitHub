using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRecuperation : MonoBehaviour
{
    [SerializeField]
    UIManager uIManager;
    [SerializeField]
    GameObject scanProgress;
    [SerializeField]
    GameObject crystalSlot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Zayn")
        {
            uIManager.MakeUIElementAppear(scanProgress);
            uIManager.MakeUIElementAppear(crystalSlot);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Zayn")
        {
            Destroy(gameObject);
        }
    }
}