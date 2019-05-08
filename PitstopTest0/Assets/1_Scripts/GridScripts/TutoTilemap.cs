using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTilemap : MonoBehaviour
{
    //Serializable
    [SerializeField] float delay = 0;
    [SerializeField] GameObject textToShow = default;

    //Private
    bool triggerOnceCheck = false;

    private void TheTutorial()
    {
        //could be improved
        textToShow.SetActive(true);
    }

    private void EndOfTheTutorial()
    {
        //could be improved
        textToShow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !triggerOnceCheck)
        {
            triggerOnceCheck = true;
            StartCoroutine(DelayForThePlayerToUnderstand());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EndOfTheTutorial();
        }
    }

    IEnumerator DelayForThePlayerToUnderstand()
    {
        yield return new WaitForSeconds(delay);
        TheTutorial();
    }
}