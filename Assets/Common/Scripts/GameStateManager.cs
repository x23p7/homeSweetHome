using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public GameState[] gameStates;
    public static GameStateManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        
        foreach (GameState gameState in gameStates)
        {
            Debug.Log(gameState.states.Length);
            for (int i = gameState.states.Length; i>0; i--)
            {
                Refresh(gameState.states[i-1]);
            }
        }
    }
    public void Refresh(State targetState)
    {
        foreach (StateEffect stateEffect in targetState.effects)
        {
            if (stateEffect.inverseInfluence)
            {
                if (stateEffect.affectedScript != null) { 
                stateEffect.affectedScript.enabled = !targetState.currentState;
                }
                if (stateEffect.affectedGameObject != null)
                {
                    stateEffect.affectedGameObject.SetActive(!targetState.currentState);
                }

            }
            else
            {
                if (stateEffect.affectedScript != null)
                {
                    stateEffect.affectedScript.enabled = targetState.currentState;
                }
                if (stateEffect.affectedGameObject != null)
                {
                    stateEffect.affectedGameObject.SetActive(targetState.currentState);
                }
            }
        }
    }
}
