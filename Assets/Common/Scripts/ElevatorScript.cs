using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorScript : MonoBehaviour
{
    public string[] sceneNames;
    bool loadingDone = true;
    public float travelTime;
    public float doorAnimationTime;
    float travelTimer;
    Scene oldScene;
    GameObject player;
    Camera currentCam;
    GameObject otherElevator;
    public AudioSource myMusic;
    public AudioSource myArrivalDing;
    public AudioSource myRideSound;
    GameObject myElevator;
    OpenElevator myDoorScript;
    [HideInInspector]
    public bool rideComplete = true;
    private void Start()
    {
        myElevator = this.gameObject.transform.parent.gameObject;
        oldScene = this.gameObject.scene;
        myDoorScript = myElevator.GetComponent<OpenElevator>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && myDoorScript.leftDoorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > doorAnimationTime)
        {
            player = collision.gameObject;
            if (rideComplete && (Input.GetKeyDown(KeyCode.Alpha1) || InputManager.instance.strongInputDown))
            {
                if (sceneNames[0].ToLowerInvariant() != oldScene.name.ToLowerInvariant())
                {
                    StartCoroutine(LoadSceneAsync(1));
                    StartCoroutine(ElevatorRide());
                }
            }
            if (rideComplete && (Input.GetKeyDown(KeyCode.Alpha2) || InputManager.instance.backStepInputDown))
            {
                if (sceneNames[1].ToLowerInvariant() != oldScene.name.ToLowerInvariant())
                {
                    StartCoroutine(LoadSceneAsync(2));
                    StartCoroutine(ElevatorRide());
                }
            }
            if (rideComplete && (Input.GetKeyDown(KeyCode.Alpha3) || InputManager.instance.itemInputDown))
            {
                if (sceneNames[2].ToLowerInvariant() != oldScene.name.ToLowerInvariant())
                {
                    StartCoroutine(LoadSceneAsync(3));
                    StartCoroutine(ElevatorRide());
                }
            }
            if (rideComplete && (Input.GetKeyDown(KeyCode.Alpha4) || InputManager.instance.vialInputDown))
            {
                if (sceneNames[3].ToLowerInvariant() != oldScene.name.ToLowerInvariant())
                {
                    StartCoroutine(LoadSceneAsync(4));
                    StartCoroutine(ElevatorRide());
                }
            }
        }
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        loadingDone = false;
        GlobalGameStateManager.instance.SaveState(GameStateManager.instance.gameState);
        GameStateManager.instance = null;
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        loading.allowSceneActivation = false;
        while (loading.progress < 0.9f)
        {
            yield return null;
        }

        loading.allowSceneActivation = true;

        while (!loading.isDone)
        {
            yield return null;
        }
        loadingDone = true;
    }

    IEnumerator ElevatorRide()
    {
        rideComplete = false;
        myMusic.Play();
        myRideSound.Play();
        SceneManager.MoveGameObjectToScene(myElevator, SceneManager.GetSceneAt(0));
        travelTimer = 0;
        while (!loadingDone)
        {
            travelTimer += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject gameObject in SceneManager.GetSceneAt(2).GetRootGameObjects())
        {
            if (gameObject.name == "Elevator")
            {
                otherElevator = gameObject;
            }
        }
        SceneManager.UnloadSceneAsync(oldScene.buildIndex);
        GlobalGameStateManager.instance.LoadState(GameStateManager.instance.gameState);
        GameStateManager.instance.InitiateGameState();
        while (loadingDone && travelTimer < travelTime)
        {
            travelTimer += Time.deltaTime;
            yield return null;
        }
        float angularDifference = otherElevator.transform.rotation.eulerAngles.y - myElevator.transform.rotation.eulerAngles.y;
        GameObject playerParent = new GameObject("PlayerParent");
        CameraController camController = CameraController.instance;
        Transform currentCamTrans = camController.currentCam.transform;
        playerMovement playerMoveScript = player.GetComponent<playerMovement>();
        playerParent.transform.position = myElevator.transform.position;
        playerParent.transform.rotation = myElevator.transform.rotation;
        player.transform.parent = playerParent.transform;
        currentCamTrans.parent = playerParent.transform;
        playerParent.transform.position = otherElevator.transform.position;
        playerParent.transform.RotateAround(otherElevator.transform.position, Vector3.up, angularDifference);
        player.transform.parent = null;
        currentCamTrans.parent = null;
        camController.camPointer = (currentCamTrans.position - (player.transform.position + player.transform.up * player.transform.localScale.y / 2)).normalized;
        playerMoveScript.currentForward = new Vector3(currentCamTrans.forward.x, 0, currentCamTrans.forward.z).normalized;
        playerMoveScript.currentRight = new Vector3(currentCamTrans.right.x, 0, currentCamTrans.right.z).normalized;
        myMusic.Stop();
        myRideSound.Stop();
        myArrivalDing.Play();
        otherElevator.GetComponent<OpenElevator>().rightDoorAnim.SetTrigger("playerInteraction");
        otherElevator.GetComponent<OpenElevator>().leftDoorAnim.SetTrigger("playerInteraction");
        while (myArrivalDing.isPlaying)
        {
            yield return null;
        }
        Destroy(myElevator.gameObject);
        Destroy(playerParent);
    }
}
