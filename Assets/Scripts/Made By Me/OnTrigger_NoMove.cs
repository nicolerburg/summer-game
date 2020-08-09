using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger_NoMove : MonoBehaviour
{
    private GameObject target = null;
    private Vector3 offset;

    private Switch_Worlds switcher;
    private bool shouldntStick = true;

    void Start() {
        target = null;
        switcher = FindObjectOfType<Switch_Worlds>();
    }

    void Update() {
        shouldntStick = switcher.wantsToSwitch;
        if (Input.GetKeyDown(KeyCode.E)) {

        } else if (target != null && !shouldntStick) {
            UnityEngine.Debug.Log("Working");
            target.transform.position = transform.position + offset;
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }

    void OnTriggerExit2D(Collider2D col) {
        target = null;
    }

    void LateUpdate() {

    }
}
