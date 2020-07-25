using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private GameManager_Burg manager;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager_Burg>();
        position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        manager.setRespawn(position);
    }
}
