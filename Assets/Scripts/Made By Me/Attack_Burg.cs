using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Burg : MonoBehaviour
{

    //Variables
    public int damage;

    private Rigidbody2D rigidBody;
    private Collider2D collider;
    private Health_Burg health;

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<Health_Burg>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Player") {
            health.LoseHealth(damage);
        }
    }
}
