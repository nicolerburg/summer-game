using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No_Switch : MonoBehaviour
{

    private Switch_Worlds switcher;

    // Start is called before the first frame update
    void Start()
    {
        switcher = FindObjectOfType<Switch_Worlds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            switcher.canSwitch = false;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            switcher.canSwitch = true;
        }
    }
}
