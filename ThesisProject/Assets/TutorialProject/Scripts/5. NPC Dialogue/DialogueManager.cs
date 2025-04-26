using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private bool inDialogue;
    private bool isTyping;
    private Queue<SO_Dialogue.Info> dialogueQueue;
    private string completeText;
    [SerializeField] private float textDelay;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TMP_Text dialogueText;
    private int nbrOfDialogues;
    private int dialogueIndex;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        dialogueQueue = new Queue<SO_Dialogue.Info>();
        dialogueIndex = 0;
    }

    private void OnInteract()
    {
        if (inDialogue)
        {
            DequeueDialogue();
        }
    }

    public void QueueDialogue(SO_Dialogue[] dialogues)
    {
        nbrOfDialogues = dialogues.Length;

        if (inDialogue)
        {
            return;
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = false;
        inDialogue = true;
        dialogueBox.SetActive(true);
        dialogueQueue.Clear();
        foreach (SO_Dialogue.Info line in dialogues[dialogueIndex].dialogueInfo)
        {
            dialogueQueue.Enqueue(line);
        }
        DequeueDialogue();
    }
    private void DequeueDialogue()
    {
        if (isTyping)
        {
            CompleteText();
            StopAllCoroutines();
            isTyping = false;
            return;
        }
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        SO_Dialogue.Info info = dialogueQueue.Dequeue();
        completeText = info.dialogue;
        dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }
    private void CompleteText()
    {
        dialogueText.text = completeText;
    }
    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        inDialogue = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = true;

        if (dialogueIndex < nbrOfDialogues -1)
        {
            dialogueIndex++;
        }
        else
        {
            dialogueIndex = 0;
        }
    }
    private IEnumerator TypeText(SO_Dialogue.Info info)
    {
        isTyping = true;
        foreach (char c in info.dialogue.ToCharArray())
        {
            yield return new WaitForSeconds(textDelay);
            dialogueText.text += c;
        }
        isTyping = false;
    }

}
