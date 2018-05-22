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
    // Use this for initialization

    private void OnDisable()
    {
        this.activated = false;
    }
    private void OnEnable()
    {
        this.activated = true;
    }
    private void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
        myMesh.enabled = false;
        activated = true;
        Register();
    }

    void Register() // this method is used to initally register every state that isnt registered already, to create a collection of all used states within the local game state manager
    {
        foreach (StateEffect activeScenario in myActiveScenarios.activeScenarios)
        {
            foreach (State state in activeScenario.isActiveWhen)
            {
                bool found = false;
                currentGameState = GameStateManager.instance.gameState;
                foreach (StateConnection stateConnection in currentGameState.stateConnections)
                {
                    if (state.stateLabel.ToLowerInvariant() == stateConnection.stateLabel) // to minimize errors by developer input, we neglect upper case sensitivity
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
                    StateConnection myStateConnection = new StateConnection //if there is no stateconnection for our state, we create a new one
                    {
                        affectedScripts = new List<MonoBehaviour> // and add this script to the affected scripts list at the same time
                    {
                        this
                    },
                        stateLabel = state.stateLabel.ToLowerInvariant() // ToLowerInvariant() returns a lower case version of our state label, so we can neglect upper case sensitivity
                    };
                    currentGameState.stateConnections.Add(myStateConnection);
                }
            }
        }
    }
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(this, dialogue);
    }

    public void TriggerOutcome() // this method is used within the dialoguemanager which calls it upon user input when the end of the dialogue is reached. the effector/s is/are declared in inspector for each dialogue trigger
    {

        currentGameState = GameStateManager.instance.gameState;


        foreach (Effector effect in effects)
        {

            foreach (StateConnection stateConnection in currentGameState.stateConnections)
            {
                if (stateConnection.stateLabel == effect.stateName.ToLowerInvariant())
                {
                    stateConnection.currentValue = effect.setTo; // we set the state connection to the new value
                    GameStateManager.instance.Refresh(stateConnection); // and refresh all affected scripts through the game state mananger
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (activated)// we use the bool activated since script.enabled=false does not affect trigger methods (they would fire regardless)
        {
            if (other.gameObject.CompareTag("Player") && triggerOnButtonPress && InputManager.instance.actionInputDown)
            {
                if (!dialogueActive) //the dialogueActive bool tells us if we already started the dialogue. if no we start it, if yes we skip to our next sentence
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

    void Refresh() //this method gets called by the local game state manager after receiving a change for a state where this script is listed in the affected scripts list
    {
        foreach (StateEffect stateEffect in myActiveScenarios.activeScenarios)
        {
            activated = true;
            foreach (State state in stateEffect.isActiveWhen)
            {
                foreach (StateConnection stateConnection in GameStateManager.instance.gameState.stateConnections)
                {
                    if (state.stateLabel.ToLowerInvariant() == stateConnection.stateLabel)
                    {
                        if (state.currentValue != stateConnection.currentValue)
                        {
                            
                            activated = false;
                        }
                    }
                }
            }
        }
        this.enabled = activated;
    }
}
