using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public bool triggerOnButtonPress;
    [HideInInspector]
    public bool dialogueActive;
    public Dialogue dialogue;
    public Effector[] effects;
    string currentSceneName;
    GameState currentGameState;
    MeshRenderer myMesh;
    bool activated;
    // Use this for initialization

    private void OnDisable()
    {
        this.activated = false;
    }
    private void OnEnable()
    {
        this.activated = true;
    }
    private void Awake()
    {
        myMesh = GetComponent<MeshRenderer>();
        myMesh.enabled = false;
        activated = true;
    }
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(this, dialogue);
    }

    public void TriggerOutcome()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        foreach (GameState gameState in GameStateManager.instance.gameStates)
        {
            if (gameState.areaName == currentSceneName)
            {
                currentGameState = gameState;
            }
        }
        foreach (Effector effect in effects)
        {

            foreach (State state in currentGameState.states)
            {
                if (state.stateLabel == effect.stateName)
                {
                    state.currentState = effect.setTo;
                    GameStateManager.instance.Refresh(state);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("collition with" + other.gameObject.tag);
        if (activated)
        {
            if (other.gameObject.CompareTag("Player") && triggerOnButtonPress && InputManager.instance.jumpInputDown)
            {
                if (!dialogueActive)
                {
                    TriggerDialogue();
                }
                else
                {
                    DialogueManager.instance.DisplayNextSentence();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myMesh.enabled = false;
        if (dialogueActive)
        {
            DialogueManager.instance.EndDialogue();
        }
    }
}
