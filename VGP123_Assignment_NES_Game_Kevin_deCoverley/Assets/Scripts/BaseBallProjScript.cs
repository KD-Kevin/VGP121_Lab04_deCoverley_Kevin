using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBallProjScript : MonoBehaviour {

    // Public Attributes
    public float speed;
    public float lifeTime;


    void OnTriggerEnter2D(Collider2D c)
        {
        if (c.gameObject.tag == "Hero" || c.gameObject.tag == "Projectile")
        {
            Debug.LogError("Passthrough Projectile || Hero");
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Collision");
        }
        }
       
    // Use this for initialization
    void Start () {
		if(speed == 0)
        {
            speed = 7.0f;
        }
        if (lifeTime <= 0)
        {
            speed = 1.0f;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        Destroy(gameObject, lifeTime);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
