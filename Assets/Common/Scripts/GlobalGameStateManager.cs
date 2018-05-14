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
                  foreach(StateConnection stateConnection in savedStates[i - 1].stateConnections)
                  {
                      for (int j = targetGameState.stateConnections.Count; j > 0; j--)
                      {
                          for (int k = savedStates[i-1].stateConnections.Count; k >0; k--)
                          {
                              if (targetGameState.stateConnections[j-1].stateLabel== savedStates[i - 1].stateConnections[k-1].stateLabel)
                              {
                                  targetGameState.stateConnections[j-1].currentValue = savedStates[i - 1].stateConnections[k-1].currentValue;
                              }
                          }
                      }
                  }
                  return;
              }
          }
    }
}
