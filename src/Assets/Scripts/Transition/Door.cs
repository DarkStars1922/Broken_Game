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
        Debug.Log("���������ʯ֮�ŵ�ѡ��");

        loadEventSO.RaiseLoadRequestEvent(sceneToGo,positionToGo);
    }

   
}
