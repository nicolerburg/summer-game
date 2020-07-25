using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [Tooltip("Object will always move toward Point Afirst")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 1f;

    private bool isMovingTowardsA = true;
    private Connected_Objects connected;
    private bool shouldMove;

    // Start is called before the first frame update
    void Start()
    {
        connected = gameObject.GetComponent<Connected_Objects>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        shouldMove = connected.shouldMove;
        UnityEngine.Debug.Log(shouldMove);
        if (shouldMove) {
            Patrol();
        }
    }

    void Patrol() {
        if (isMovingTowardsA) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, pointA.position, speed);
        }
        else {
            this.transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed);
        }
        if (this.transform.position == pointA.position) {
            isMovingTowardsA = false;
        }
        else if (this.transform.position == pointB.position) {
            isMovingTowardsA = true;
        }
    }
}
