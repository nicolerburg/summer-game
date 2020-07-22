using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Burg : MonoBehaviour
{

    //Variables

    [Header("Player Info")] //Header makes a header in Unity component
    [Tooltip("Drag you player object here!")] //Tooltip gets attached to whatever variable is directly below
    public GameObject player; //public looks gives field in Unity
    private PlayerMovement_Burg playerMov; //private looks for script already attached to player object
    private Health_Burg playerH;


    [Header("Spawn Point")]
    [Tooltip("Enter where you would like player to spawn")]
    public Vector3 spawnPoint;
    public bool setRespawnToPlayerPlacement = false;

    // Start is called before the first frame update
    void Start() {
        playerMov = player.GetComponent<PlayerMovement_Burg>(); //gets script on player
        playerH = player.GetComponent<Health_Burg>(); //script for health
        if (setRespawnToPlayerPlacement) {
            spawnPoint = player.transform.position;
        }
    }

    // Update is called once per frame
    void Update() {
        //Exits the game
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    // Respawn function
    public void Respawn() {
        player.transform.position = spawnPoint;
        playerH.health = playerH.maxHealth;
    }

    // Enables Checkpoints to change player spawn
    public void setRespawn(Vector3 newPosition) {
        spawnPoint = newPosition;
    }
}
