using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Burg : MonoBehaviour
{

    //Variables
    public int maxHealth = 100;
    [HideInInspector]
    public int health;


    private GameManager_Burg gameManager;

    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        gameManager = FindObjectOfType<GameManager_Burg>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    //Subtracts health from player upon damage
    public void LoseHealth(int damage) {
        health -= damage;
        if (health <= 0) {
            gameManager.Respawn();
        }
    }
}
