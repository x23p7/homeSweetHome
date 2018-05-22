using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadPortalScene : MonoBehaviour
{
    public int nextScene;
    public Scene oldScene;
    bool loadingDone = true;
    public GameObject showstopper;
    public Animator myAnim;
    public float minWaitTime;
    public GameObject portalCam;
    public Material portalCamMat;
    bool minWaitOver;
    public Transform targetPoint;
    private void Start()
    {
        oldScene = this.gameObject.scene;
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

        //Fade the loading screen out here


        loading.allowSceneActivation = true;

        while (!loading.isDone)
        {
            yield return null;
        }
        loadingDone = true;
        SetupPortal();
        if (minWaitOver && showstopper.activeSelf)
        {
            showstopper.SetActive(false);
        }

    }

    IEnumerator BlockSight()
    {
        minWaitOver = false;
        showstopper.SetActive(true);
        yield return new WaitForSeconds(minWaitTime);
        if (loadingDone)
        {
            showstopper.SetActive(false);
        }
        minWaitOver = true;
    }

    void SetupPortal()
    {
        Scene targetScene = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
        foreach (GameObject rootObject in targetScene.GetRootGameObjects())
        {
            if (rootObject.name == "Portal-Main")
            {
                foreach (Transform child in rootObject.GetComponentInChildren<Transform>())
                {
                    if (child.gameObject.name == "LandingPoint")
                    {
                        targetPoint = child.transform;
                        Debug.DrawRay(targetPoint.position, Vector3.up * 3f, Color.red, 5f);
                    }
                }
            }
        }
        Camera portalCamCam = portalCam.GetComponent<Camera>();
        if (portalCamCam.targetTexture != null)
        {
            portalCamCam.targetTexture.Release();
        }
        portalCamCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        portalCamMat.mainTexture = portalCamCam.targetTexture;
        portalCam.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (SceneManager.sceneCount < 3 && InputManager.instance.actionInputDown && other.CompareTag("Player") && loadingDone)
        {
            myAnim.SetTrigger("playerAction");
            StartCoroutine(BlockSight());
            StartCoroutine(LoadSceneAsync(nextScene));
        }
    }
}
