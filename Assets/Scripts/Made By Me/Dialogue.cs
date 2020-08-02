using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Dialogue : MonoBehaviour {

    public GameObject CanvasBox;

    [Tooltip("Your text body")]
    public Text TextBox; // the text body
    [Tooltip("the text body of the name you want to display")]
    public Text NameText; // the text body of the name you want to display

    private Queue<string> inputStream = new Queue<string>();

    private PlayerMovement_Burg playerMovement;

    [HideInInspector]
    public DialogueTrigger currentTrigger;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void FreezePlayer() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement_Burg>();
        playerMovement.frozen = true;
    }

    private void UnFreezePlayer() {
        playerMovement.frozen = false;
    }

    public void StartDialogue(Queue<string> dialogue) {
        CanvasBox.SetActive(true);
        FreezePlayer();

        inputStream = dialogue; // store the dialogue from dialogue trigger
        PrintDialogue(); // Prints out the first line of dialogue
    }

    private void PrintDialogue() {
        if (inputStream.Peek().Contains("END")) {
            inputStream.Dequeue();
            EndDialogue();
        }
        else if (inputStream.Peek().Contains("[NAME =")) {
            string name = inputStream.Peek();
            name = inputStream.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
            NameText.text = name;
            PrintDialogue(); // print the rest of this line
        }
        else {
            TextBox.text = inputStream.Dequeue();
        }
    }

    public void AdvanceDialogue() {
        PrintDialogue();
    }

    public void EndDialogue() {
        TextBox.text = "";
        NameText.text = "";
        inputStream.Clear();
        CanvasBox.SetActive(false);

        if (currentTrigger.singleUseDialogue) {
            currentTrigger.hasBeenUsed = true;
        }
        inputStream.Clear();

        Invoke("UnFreezePlayer", 0.5f);

    }
}
