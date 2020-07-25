using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connected_Objects : MonoBehaviour
{

    public GameObject manager;
    public GameObject connectedObject;
    public bool inFirstWorld;
    public bool isMoveable;
    public bool movesLeftRight;

    private Switch_Worlds switcher;
    private bool isPlayerFirstWorld;
    public bool shouldMove;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        switcher = manager.GetComponent<Switch_Worlds>();
        distance = connectedObject.transform.position.y - this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerFirstWorld = switcher.isFirstWorld; //gets players world location
        if (isMoveable) {
            if (isPlayerFirstWorld && inFirstWorld || (!isPlayerFirstWorld && !inFirstWorld)) {
                shouldMove = true;
            }
            else {
                shouldMove = false;
            }
        } else {
            if ((isPlayerFirstWorld && !inFirstWorld) || (!isPlayerFirstWorld && inFirstWorld)) {
                FollowCounterpart();
            }
        }
    }

    void FollowCounterpart() {
        if (movesLeftRight) {
            this.transform.position = new Vector3(connectedObject.transform.position.x, this.transform.position.y, 0);
        } else {
            this.transform.position = new Vector3(this.transform.position.x, connectedObject.transform.position.y - distance, 0);
        }
    }
}

