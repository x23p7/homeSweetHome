using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadPortalScene : MonoBehaviour {
    public int nextScene;
    Scene oldScene;
    bool loadingDone;
    public GameObject showstopper;

    private void Start()
    {
        oldScene = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
    }
    IEnumerator loadSceneAsync(int sceneIndex)
    {
        loadingDone = false;
        GlobalGameStateManager.instance.SaveState(GameStateManager.instance.gameState);
        GameStateManager.instance = null;
        showstopper.SetActive(true);
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
        showstopper.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.sceneCount < 3)
        {
            StartCoroutine(loadSceneAsync(nextScene));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (loadingDone && SceneManager.sceneCount > 2)
        {
            
            GlobalGameStateManager.instance.LoadState(GameStateManager.instance.gameState);
            GameStateManager.instance.InitiateGameState();
            SceneManager.UnloadSceneAsync(oldScene);
        }
    }
}
