using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glade1Cinematic : MonoBehaviour
{
    SceneLoader sceneLoader;

    public GameObject virtualCameraPlayer;
    public GameObject virtualCameraGlade;
    public GameObject associatedStartingPoint;

    public EnemySpawner hHSpawner;

    private static bool playerCheck;

    private void Start()
    {
        sceneLoader = GameManager.Instance.sceneLoader;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Zayn" && !playerCheck)
        {
            virtualCameraPlayer.SetActive(false);

            sceneLoader.SaveStartingPoint(associatedStartingPoint);

            //hHSpawner.SpawnTheThing();

            playerCheck = true;
        }
    }

    public void SendInformationToHHSpawner()
    {
        
    }
}