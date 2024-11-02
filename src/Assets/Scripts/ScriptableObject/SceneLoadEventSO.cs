using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoaderSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3> LoadRequestEvent;

    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad,Vector3 posToGo)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo); 
    }
}
