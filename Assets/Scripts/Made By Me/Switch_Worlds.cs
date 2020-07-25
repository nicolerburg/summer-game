using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Switch_Worlds : MonoBehaviour
{

    public float distance = 15f;
    public GameObject reference;
    public bool isFirstWorld = true;
    private GameObject player;

    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > reference.transform.position.y) {
            isFirstWorld = true;
        }
        else if (player.transform.position.y < reference.transform.position.y) {
            isFirstWorld = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && player.transform.position.x > reference.transform.position.x) {
            SwitchWorlds();
        }
    }

    void SwitchWorlds() {
        if (isFirstWorld) {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - distance, player.transform.position.z);
        } else {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + distance, player.transform.position.z);
        }
    }
}
