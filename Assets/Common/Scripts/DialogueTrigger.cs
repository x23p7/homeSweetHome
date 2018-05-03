using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public bool triggerOnButtonPress;
    [HideInInspector]
    public bool dialogueActive;
    public Dialogue dialogue;

    MeshRenderer myMesh;
    // Use this for initialization
    private void Awake()
    {
        myMesh = GetComponent<MeshRenderer>();
        myMesh.enabled = false;
    }
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(this,dialogue);
    }

    private void OnTriggerStay (Collider other)
    {
        Debug.Log("collition with" + other.gameObject.tag);

        if (other.gameObject.CompareTag("Player") && triggerOnButtonPress && InputManager.instance.jumpInputDown)
        {
            if (!dialogueActive) { 
            TriggerDialogue();
            }
            else
            {
                DialogueManager.instance.DisplayNextSentence();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myMesh.enabled = false;
        if (dialogueActive) { 
        DialogueManager.instance.EndDialogue();
        }
    }
}
