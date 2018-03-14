using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollisionCoin : MonoBehaviour {

    // Public Attributes
    public float VerticalSpeed;
    public bool IsCollected = false;

    public Animator anim; // Animator
   
    void OnTriggerEnter2D(Collider2D c)
    {
        anim = GetComponent<Animator>(); // Animator

        //Collides with a Hero and destroys itself
        if (c.gameObject.tag == "Hero" || c.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            IsCollected = true;
            anim.SetBool("IsCollected", IsCollected);
            Debug.LogWarning("Hero Collision");
        }
        else
        {
            Debug.LogError("Collision");
        }
    }

    // Use this for initialization
    void Start()
    {
        if (VerticalSpeed == 0)
        {
            VerticalSpeed = 0.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, VerticalSpeed);

    }

    // Update is called once per frame
    void Update()
    {

    }
}