using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    Dialogue manager;
    public TextAsset TextFileAsset; // imported text file for dialogue
    private Queue<string> dialogue = new Queue<string>(); // stores the dialogue
    public float waitTime = 0.5f; // lag time for advancing dialogue so you can actually read it
    private float nextTime = 0f; // used with waitTime to create a timer system
    public bool singleUseDialogue = false;
    [HideInInspector]
    public bool hasBeenUsed = false;
    bool inArea = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenUsed && inArea && Input.GetKeyDown(KeyCode.Return) && nextTime < Time.timeSinceLevelLoad) {
            nextTime = Time.timeSinceLevelLoad + waitTime;
            manager.AdvanceDialogue();
        }
    }

    void TriggerDialogue() {
        ReadTextFile();
        manager.StartDialogue(dialogue);
    }

    private void ReadTextFile() {
        string txt = TextFileAsset.text;

        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // Split dialogue lines by newline

        SearchForTags(lines);

        dialogue.Enqueue("END");
    }

    private void SearchForTags(string[] lines) {
        foreach (string line in lines) // for every line of dialogue
        {
            if (!string.IsNullOrEmpty(line))// ignore empty lines of dialogue
            {
                if (line.StartsWith("[")) // e.g [NAME=Michael] Hello, my name is Michael
                {
                    string special = line.Substring(0, line.IndexOf(']') + 1); // special = [NAME=Michael]
                    string curr = line.Substring(line.IndexOf(']') + 1); // curr = Hello, ...
                    dialogue.Enqueue(special); // adds to the dialogue to be printed
                    string[] remainder = curr.Split(System.Environment.NewLine.ToCharArray());
                    SearchForTags(remainder);
                } else {
                    dialogue.Enqueue(line); // adds to the dialogue to be printed
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !hasBeenUsed) {
            manager.currentTrigger = this;
            TriggerDialogue();
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            inArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            manager.EndDialogue();
        }
        inArea = false;

    }


}
