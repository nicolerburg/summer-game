using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class Movable_Object : MonoBehaviour
{

    //Variables
    [Range(0.0f, 1.0f)]
    public float speedReduction;

    //private Rigidbody2D rigidBody;
    private Collider2D collider;
    private Collider2D trigger;
    private Collider2D collidingWith;
    private PlayerMovement_Burg movement;
    private bool isInteracting;


    // Start is called before the first frame update
    void Start()
    {
        movement = FindObjectOfType<PlayerMovement_Burg>();
        Collider2D[] colliders = GetComponents<Collider2D>();
        for (int i = 0; i < colliders.Length; i++) {
            Collider2D col = colliders[i];
            if (col.isTrigger) {
                trigger = col;
            } else { collider = col; }
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (collidingWith) {
            GameObject obj = collidingWith.gameObject;
            if (obj.name == "Player") {
                if (Input.GetMouseButton(0)) {
                    //movement.changeSpeed(speedReduction);
                    float delta = 0.1f;
                    Vector3 boxPos = this.transform.position;
                    Vector2 boxSize = collider.bounds.size;
                    Vector3 playerPos = obj.transform.position;
                    Vector2 playerSize = collidingWith.bounds.size;
                    Vector3 playerVel = obj.GetComponent<Rigidbody2D>().velocity;
                    UnityEngine.Debug.Log(Mathf.Sign(playerVel.x));
                    //UnityEngine.Debug.Log((playerPos.x + 0.5f * playerSize.x).ToString() + " vs. " + (boxPos.x - 0.5f * boxSize.x).ToString());
                    //Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.extents.x, 0), Vector2.down * (collider.bounds.extents.y + boxCastDepth), color);
                    if (playerPos.x + 0.5f * playerSize.x <= boxPos.x - 0.5f * boxSize.x) { //player is on left
                        //Debug.DrawRay(new Vector3(playerPos.x + 0.5f * playerSize.x, playerPos.y, playerPos.z), new Vector3(boxPos.x - 0.5f * boxSize.x - playerPos.x + 0.5f * playerSize.x, 0, 0), Color.green);
                        boxPos.x = playerPos.x + 0.5f * playerSize.x + 0.5f * boxSize.x + delta + playerVel.x / 100;
                    }
                    else if (playerPos.x - 0.5f * playerSize.x >= boxPos.x + 0.5f * boxSize.x) { //player is on right
                        //Debug.DrawRay(new Vector3(playerPos.x - 0.5f * playerSize.x, playerPos.y, playerPos.z), new Vector3(boxPos.x + 0.5f * boxSize.x + playerPos.x - 0.5f * playerSize.x, 0, 0), Color.green);
                        boxPos.x = playerPos.x - 0.5f * playerSize.x - 0.5f * boxSize.x - delta + playerVel.x / 100;
                        UnityEngine.Debug.Log("Is on right and clicking.");
                    }
                    this.transform.position = boxPos;
                    //rigidBody.velocity = new Vector2(movement.walkSpeed, rigidBody.velocity.y);
                    //rigidBody.velocity.x = GameObject.Find("Player").GetComponent<PlayerMovement_Burg>().walkSpeed;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D otherCol) {
        collidingWith = otherCol;
    }

    public void OnTriggerExit2D(Collider2D otherCol) {
        collidingWith = null;
    }


}
