using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRecuperation : MonoBehaviour
{
    [SerializeField]
    UIManager uIManager;
    [SerializeField]
    GameObject[] whatElementsShouldAppear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Zayn")
        {
            foreach (var element in whatElementsShouldAppear)
            {
                uIManager.MakeUIElementAppear(element);
            }
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