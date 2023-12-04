using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;

    public GameObject startDialogue;
    public GameObject winDialogue;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        winDialogue.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        winDialogue.SetActive(true);
        startDialogue.SetActive(false);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentences in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArrsy())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        winDialogue.SetActive(false);
    }
}
