using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameStateManager : MonoBehaviour {
    public List<GameState> savedStates;
    public static GlobalGameStateManager instance;

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
        savedStates = new List<GameState>();
    }

    public void SaveState(GameState currentRoomState)
    {
        for(int i = savedStates.Count; i>0; i--)
        {
            if (savedStates[i-1].areaName == currentRoomState.areaName)
            {
                savedStates[i - 1] = currentRoomState;
                return;
            }
        }
        savedStates.Add(currentRoomState);
    }

    public void LoadState(GameState targetGameState)
    {
        for (int i = savedStates.Count; i > 0; i--)
        {
            if (savedStates[i - 1].areaName == targetGameState.areaName)
            {
                foreach(State state in savedStates[i - 1].states)
                {
                    for (int j = targetGameState.states.Length; j > 0; j--)
                    {
                        for (int k = savedStates[i-1].states.Length; k >0; k--)
                        {
                            if (targetGameState.states[j-1].stateLabel== savedStates[i - 1].states[k-1].stateLabel)
                            {
                                targetGameState.states[j-1].currentState = savedStates[i - 1].states[k-1].currentState;
                            }
                        }
                    }
                }
                return;
            }
        }
    }
}
