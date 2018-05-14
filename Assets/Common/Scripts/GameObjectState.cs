using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectState : MonoBehaviour {
    public StateEffects myActiveScenarios;
    StateConnection myStateConnection;
    bool active;
    private void Awake()
    {
        myStateConnection = new StateConnection();
        myStateConnection.affectedScripts = new List<MonoBehaviour>();
        Register();
    }
    void Register()
    {
        myStateConnection.affectedScripts.Add(this);
        foreach (StateEffect activeScenario in myActiveScenarios.activeScenarios) {
            foreach(State state in activeScenario.isActiveWhen)
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
    void Refresh()
    {
        foreach (StateEffect stateEffect in myActiveScenarios.activeScenarios)
        {
            foreach (State state in stateEffect.isActiveWhen)
            {
                active = true;
                foreach (StateConnection stateConnection in GameStateManager.instance.gameState.stateConnections)
                {
                    if (state.stateLabel == stateConnection.stateLabel)
                    {
                        if (state.currentValue != stateConnection.currentValue)
                        {
                            active = false;
                        }
                    }
                }
            }
        }
        this.gameObject.SetActive(active);
    }
}
