using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dalle : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField]
    private GameObject groundTileMap;

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        if (other.gameObject.tag == "Player")
        {
            gameObject.transform.position = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y - 0.12f);
            transform.GetChild(0).gameObject.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.12f);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
