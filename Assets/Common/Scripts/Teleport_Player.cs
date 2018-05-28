using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleport_Player : MonoBehaviour
{
    Transform playerTrans;
    public GameObject myLocalLandingPoint;
    public LoadPortalScene loadScript;
    private void OnCollisionEnter(Collision collision)
    {
        if (SceneManager.sceneCount > 2)
        {
            playerTrans = GlobalGameStateManager.instance.player.transform;
            Vector3 offSet =new Vector3(playerTrans.position.x - transform.position.x, playerTrans.position.y-myLocalLandingPoint.transform.position.y, playerTrans.position.z - transform.position.z);
            playerTrans.position = loadScript.targetPoint.position + offSet;
            playerTrans.rotation = loadScript.targetPoint.parent.rotation;
            SceneManager.UnloadSceneAsync(loadScript.oldScene);
            GlobalGameStateManager.instance.LoadState(GameStateManager.instance.gameState);
            GameStateManager.instance.InitiateGameState();
        }
    }
}