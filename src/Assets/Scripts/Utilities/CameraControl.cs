using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public VoidEventSO afterSceneLoadedEvent;

    private CinemachineConfiner2D confiner2D;

    public void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>(); 
    }
    private void OnEnable()
    {
        afterSceneLoadedEvent.OnEventRasied += OnAfterSceneLodedEvent;
    }
    private void OnDisable()
    {
        afterSceneLoadedEvent.OnEventRasied -= OnAfterSceneLodedEvent;
    }

    private void OnAfterSceneLodedEvent()
    {
        GetNewCaremaBounds();
    }

    //private void Start()
    //{
    //    GetNewCaremaBounds();
    //}
    private void GetNewCaremaBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null) 
            return;
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        confiner2D.InvalidateCache();
    }
}
