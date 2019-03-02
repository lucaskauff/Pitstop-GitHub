using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseApple_Behavior : MonoBehaviour
{
    public float Explosion_Delay = 1.0f;
    public float Explosion_Rate = 1.0f;
    public float Explosion_MaxSize = 10.0f;
    public float Explosion_Speed = 1.0f;
    public float Current_Radius = 0f;

    bool Exploded = false;
    CircleCollider2D Explosion_Radius;

    // Start is called before the first frame update
    void Start()
    {
        Explosion_Radius = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Explosion_Delay -= Time.deltaTime;
        if (Explosion_Delay <0)
        {
            Exploded = true;
        }
    }

    void FixedUpdate()
    {
        if (Exploded == true)
        {
            if (Current_Radius<Explosion_MaxSize)
            {
                Current_Radius += Explosion_Rate;
            }
            else
            {
                Object.Destroy(this.gameObject.transform.parent.gameObject);
            }

            Explosion_Radius.radius = Current_Radius;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Exploded == true)
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>() !=null)
            {
                Vector2 target = collision.gameObject.transform.position;
                Vector2 Apple = gameObject.transform.position;
            }
        }
    }
}
