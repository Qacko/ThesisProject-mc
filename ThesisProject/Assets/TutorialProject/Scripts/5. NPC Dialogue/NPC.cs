using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, F_IInteractable
{
    [SerializeField] SO_Dialogue[] dialogues;

    public void Interact()
    {
        DialogueManager.Instance.QueueDialogue(dialogues);
    }
}
