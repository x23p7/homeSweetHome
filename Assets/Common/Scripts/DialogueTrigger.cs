using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool triggerOnButtonPress;
    [HideInInspector]
    public bool dialogueActive;
    public Dialogue dialogue;
    public Effector[] effects;
    public StateEffects myActiveScenarios;
    string currentSceneName;
    GameState currentGameState;
    MeshRenderer myMesh;
    bool activated;
    StateConnection myStateConnection;
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
        myStateConnection = new StateConnection();
        myStateConnection.affectedScripts = new List<MonoBehaviour>();
        myMesh = GetComponent<MeshRenderer>();
        myMesh.enabled = false;
        activated = true;
        Register();
    }

    void Register()
    {
        print(myStateConnection);
        myStateConnection.affectedScripts.Add(this);
        foreach (StateEffect activeScenario in myActiveScenarios.activeScenarios)
        {
            foreach (State state in activeScenario.isActiveWhen)
            {
                bool found = false;
                GameState currentGameState = GameStateManager.instance.gameState;
                foreach (StateConnection stateConnection in currentGameState.stateConnections)
                {
                    if (state.stateLabel == stateConnection.stateLabel)
                    {
                        found = true;
                        bool existing = false;
                        foreach (MonoBehaviour script in stateConnection.affectedScripts)
                        {
                            if (script == this)
                            {
                                existing = true;
                            }
                        }
                        if (!existing)
                        {
                            stateConnection.affectedScripts.Add(this);
                        }
                    }
                }
                if (!found)
                {
                    myStateConnection.stateLabel = state.stateLabel;
                    currentGameState.stateConnections.Add(myStateConnection);
                }
            }
        }
    }
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(this, dialogue);
    }

    public void TriggerOutcome()
    {

        currentGameState = GameStateManager.instance.gameState;


        foreach (Effector effect in effects)
        {

            foreach (StateConnection stateConnection in currentGameState.stateConnections)
            {
                if (stateConnection.stateLabel == effect.stateName)
                {
                    stateConnection.currentValue = effect.setTo;
                    GameStateManager.instance.Refresh(stateConnection);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
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

    void Refresh()
    {
        foreach (StateEffect stateEffect in myActiveScenarios.activeScenarios)
        {
            activated = true;
            foreach (State state in stateEffect.isActiveWhen)
            {
                foreach (StateConnection stateConnection in GameStateManager.instance.gameState.stateConnections)
                {
                    if (state.stateLabel == stateConnection.stateLabel)
                    {
                        if (state.currentValue != stateConnection.currentValue)
                        {
                            activated = false;
                        }
                    }
                }
            }
        }
        print(activated + this.gameObject.name);
        this.enabled = activated;
    }
}
