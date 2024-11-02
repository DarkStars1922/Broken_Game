using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public SceneLoaderSO loadEventSO;

    public GameSceneSO sceneToGo;

    public Vector3 positionToGo;
    public void TriggerAction()
    {
        Debug.Log("这就是命运石之门的选择！");

        loadEventSO.RaiseLoadRequestEvent(sceneToGo,positionToGo);
    }

   
}
