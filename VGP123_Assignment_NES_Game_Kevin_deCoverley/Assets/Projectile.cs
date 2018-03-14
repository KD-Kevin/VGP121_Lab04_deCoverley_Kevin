using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Public Attributes
    public float speedX;
    public float speedY;
    public float lifeTime;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Hero" || c.gameObject.tag == "Projectile")
        {
            Debug.LogError("Hero Hit This Projectile");
        }
        else
        {
            Debug.LogError("Collision Error");
        }
    }

    // Use this for initialization
    void Start()
    {
        if (speedX == 0)
        {
            speedX = 7.0f;
        }
        if (speedY == 0)
        {
            speedY = 7.0f;
        }
        if (lifeTime <= 0)
        {
            lifeTime = 3.0f;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
