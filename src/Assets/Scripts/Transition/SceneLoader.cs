using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;

    public Vector3 firstPosition;

    public SceneLoaderSO loadEventSO;

    public GameSceneSO firstLoadScene;

    public VoidEventSO afterScneLoadedEvent;

    public GameSceneSO currentLoadedScene;

    private GameSceneSO sceneToLoad;

    private Vector3 positionToGo;

    private bool isLoading;

    private void Awake()
    {
    //    //Addressables.LoadSceneAsync(firstLoadScene.sceneRefence,LoadSceneMode.Additive);
    //    currentLoadedScene = firstLoadScene;
    //    currentLoadedScene.sceneRefence.LoadSceneAsync(LoadSceneMode.Additive);
        
    }
    private void Start()
    {
        NewGame();
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }

    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        OnLoadRequestEvent(sceneToLoad, firstPosition);
    }
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo)
    {
        if (isLoading)
            return;
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        if (currentLoadedScene != null)
            StartCoroutine(UnloadPreviousScene());
        else
            LoadNewScene();
    }

    private IEnumerator UnloadPreviousScene()
    {
        yield return currentLoadedScene.sceneRefence.UnLoadScene();

        playerTrans.gameObject.SetActive(false);

        LoadNewScene();
    }
    private void LoadNewScene()
    {
       var loadingOption = sceneToLoad.sceneRefence.LoadSceneAsync(LoadSceneMode.Additive,true);
        loadingOption.Completed += OnLoadCompleted;

    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currentLoadedScene = sceneToLoad;

        playerTrans.position = positionToGo;

        playerTrans.gameObject.SetActive(true);

        isLoading = false;

        afterScneLoadedEvent?.RaiseEvent();
    }
}
