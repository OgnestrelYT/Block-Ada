using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_trigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
