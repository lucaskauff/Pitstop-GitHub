using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject diaBox;
    public GameObject nameText;
    public GameObject dialogueText;
    public PlayerControllerIso playerControl;

    [SerializeField]
    float letterSpeed = 0;

    Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        playerControl.canMove = false;
        //playerControl.
        diaBox.SetActive(true);

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
            yield return new WaitForSeconds(letterSpeed);
        }
    }

    public void EndDialogue()
    {
        diaBox.SetActive(false);
        playerControl.canMove = true;
    }
}