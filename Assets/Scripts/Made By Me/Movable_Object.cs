using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Movable_Object : MonoBehaviour
{

    //Variables
    [Range(0.0f, 1.0f)]
    public float speedReduction;

    //private Rigidbody2D rigidBody;
    private Collider2D collider;
    private PlayerMovement_Burg movement;
    private bool isInteracting;
    private float difference;


    // Start is called before the first frame update
    void Start()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        movement = FindObjectOfType<PlayerMovement_Burg>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            isInteracting = true;
        } else {
            isInteracting = false;
        }
    }

    // On collision with player object will follow player's x position
    public void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.name == "Player" && isInteracting) {
            //movement.changeSpeed(speedReduction);
            difference = GameObject.Find("Player").transform.position.x - this.transform.position.x;
            if (difference < 0) {
                this.transform.position = new Vector3(GameObject.Find("Player").transform.position.x + 1.1f, this.transform.position.y, this.transform.position.z);
            }
            else if (difference > 0){
                this.transform.position = new Vector3(GameObject.Find("Player").transform.position.x - 1, this.transform.position.y, this.transform.position.z); ;
            }
            //this.transform.position = GameObject.Find("Player").transform.position;
            //rigidBody.velocity = new Vector2(movement.walkSpeed, rigidBody.velocity.y);
            //rigidBody.velocity.x = GameObject.Find("Player").GetComponent<PlayerMovement_Burg>().walkSpeed;
        } else { movement.changeSpeed(1); }

    }
}
