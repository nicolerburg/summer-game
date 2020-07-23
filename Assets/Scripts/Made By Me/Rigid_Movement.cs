using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigid_Movement : MonoBehaviour
{
    //Variables
    [Range(0.0f, 1.0f)]
    public float speedReduction;

    private Rigidbody2D rigidBody;
    private Collider2D collider;
    private Collider2D trigger;
    private Collider2D collidingWith;
    private PlayerMovement_Burg movement;
    private bool isInteracting;


    // Start is called before the first frame update
    void Start() {
        movement = FindObjectOfType<PlayerMovement_Burg>();
        rigidBody = GetComponent<Rigidbody2D>();

        Collider2D[] colliders = GetComponents<Collider2D>();
        for (int i = 0; i < colliders.Length; i++) {
            Collider2D col = colliders[i];
            if (col.isTrigger) {
                trigger = col;
            }
            else { collider = col; }
        }
    }

    // Update is called once per frame
    void Update() {
        rigidBody.velocity = new Vector2(0, 0);
        if (collidingWith) {
            UnityEngine.Debug.Log("Colliding");
            GameObject obj = collidingWith.gameObject;
            if (obj.name == "Player") {
                if (Input.GetMouseButton(0)) {
                    Vector3 playerVel = obj.GetComponent<Rigidbody2D>().velocity;
                    rigidBody.velocity = new Vector2(playerVel.x, playerVel.y);
                } else 
                { 
                    rigidBody.velocity = new Vector2(0, 0);
                    UnityEngine.Debug.Log("Rigid body stopped");
                }
            }
        }
        else 
        {
            rigidBody.velocity = new Vector2(0, 0);
            UnityEngine.Debug.Log("Rigid body stopped");
        }
    }

    public void OnTriggerEnter2D(Collider2D otherCol) {
        collidingWith = otherCol;
    }

    public void OnTriggerExit2D(Collider2D otherCol) {
        collidingWith = null;
    }
}
