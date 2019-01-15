using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject nameText;
    public GameObject dialogueText;

    Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.GetComponent<TextMeshProUGUI>().text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        //Displays the whole sentence
        //dialogueText.GetComponent<TextMeshProUGUI>().text = sentence;

        //Displays letter by letter
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.GetComponent<TextMeshProUGUI>().text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.GetComponent<TextMeshProUGUI>().text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {

    }
}