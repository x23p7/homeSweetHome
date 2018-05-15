using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleport_Player : MonoBehaviour
{
    GameObject player;
    public LoadPortalScene loadScript;
    private void OnCollisionEnter(Collision collision)
    {
        if (SceneManager.sceneCount > 2)
        {
            player = GlobalGameStateManager.instance.player;
            player.transform.position = loadScript.targetPoint.position + Vector3.up * player.transform.localScale.y/2.2f;
            player.transform.rotation = loadScript.targetPoint.parent.rotation;
            SceneManager.UnloadSceneAsync(loadScript.oldScene);
            GlobalGameStateManager.instance.LoadState(GameStateManager.instance.gameState);
            GameStateManager.instance.InitiateGameState();
        }
    }
}