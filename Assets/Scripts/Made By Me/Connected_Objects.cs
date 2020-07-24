using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connected_Objects : MonoBehaviour
{

    public GameObject manager;
    public GameObject connectedObject;
    public bool inFirstWorld;
    public bool isMoveable;

    private Switch_Worlds switcher;
    private bool isPlayerFirstWorld;
    public bool shouldMove;

    // Start is called before the first frame update
    void Start()
    {
        switcher = manager.GetComponent<Switch_Worlds>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerFirstWorld = switcher.isFirstWorld; //gets players world location
        if (isMoveable) {
            if (isPlayerFirstWorld && inFirstWorld || !(isPlayerFirstWorld && inFirstWorld)) {
                shouldMove = true;
            }
            else {
                shouldMove = false;
            }
        } else {
            if (isPlayerFirstWorld && !inFirstWorld || !isPlayerFirstWorld && inFirstWorld) {
                FollowCounterpart();
            }
        }
    }

    void FollowCounterpart() {
        this.transform.position = new Vector3(connectedObject.transform.position.x, this.transform.position.y, 0);
    }
}
