using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBoxScript : MonoBehaviour {

    // Speed of the released Projectile
    public float ReleaseSpeedX;
    public float ReleaseSpeedY;
    public Transform projSpawn; //Spawn of either the Mushroom/ Flower or Star
    // Time for the Swtch States
    public float SwitchTimeSet;
    float SwitchTime = 0; //Current Time
    public int State = 0; // decides what state the Box state is

    // The States of the Box
    public bool IsStar = true;
    public bool IsFlower = false;
    public bool IsMushroom = false;

    // Projectile Prefabs for each State
    public Projectile StarPrefabProj;
    public Projectile MushroomPrefabProj;
    public Projectile FlowerPrefabProj;
   

    public Animator anim; // Animator

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Hero")
        {
            Destroy(gameObject); //Destroy Initial Object
            Debug.LogError("Hero Collision");

            //Spawn the Correct Item
            if (IsStar && !IsMushroom && !IsFlower)
            {
                Projectile temp = Instantiate(StarPrefabProj, projSpawn.position, projSpawn.rotation);
                temp.speedX = ReleaseSpeedX;
                temp.speedY = ReleaseSpeedX;
                Debug.LogWarning("StarSpawned");
            }
            else if (IsMushroom && !IsStar && !IsFlower)
            {
                Projectile temp = Instantiate(MushroomPrefabProj, projSpawn.position, projSpawn.rotation);
                temp.speedX = ReleaseSpeedX;
                temp.speedY = ReleaseSpeedX;
                Debug.LogWarning("MushroomSpawned");
            }
            else if (IsFlower && !IsMushroom && !IsStar)
            {
                Projectile temp = Instantiate(FlowerPrefabProj, projSpawn.position, projSpawn.rotation);
                temp.speedX = ReleaseSpeedX;
                temp.speedY = ReleaseSpeedX;
                Debug.LogWarning("FlowerSpawned");
            }
            else
            {
                Debug.LogError("Projectile Error");
            }
        }
        else
        {
            Debug.LogError("Collision");
        }
    }
    // Use this for initialization
    void Start () {
		if (ReleaseSpeedX == 0.0f)
        {
            ReleaseSpeedX = 2.0f;
            Debug.LogWarning("Release speed not specified :: X");
        }
        if (ReleaseSpeedY == 0.0f)
        {
            ReleaseSpeedY = 1.0f;
            Debug.LogWarning("Release speed not specified :: Y");
        }
        if (SwitchTimeSet == 0)
        {
            SwitchTimeSet = 0.3f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        anim = GetComponent<Animator>(); // Animator
        SwitchTime += Time.deltaTime; // Time in Seconds

        // NOTE: The states are not switching correctly. My only assumption is there is something wrong with the time function. Ran out of time trying to fix it. 
        if (SwitchTime >= SwitchTimeSet)
        {
            State++;
            SwitchTime = 0;
        }

        if (State == 0)
        {
            IsStar = true;
            IsFlower = false;
            IsMushroom = false;
        }
        else if (State == 1)
        {
            IsStar = false;
            IsFlower = false;
            IsMushroom = true;
        }
        else if (State == 2)
        {
            IsStar = false;
            IsFlower = true;
            IsMushroom = false;
            State = 0;
        }
        else
        {
            Debug.LogError("Error: Iswhat? 'EndBox' ");
        }
        // Send to animator
        anim.SetInteger("State", State);

    }
}
