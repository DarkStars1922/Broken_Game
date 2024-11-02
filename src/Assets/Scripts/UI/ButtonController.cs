using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{
    public InputController ic;
    public GameObject flagToShow;
    public GameObject DialogBox;
    public Transform positionToShow;

    private void Awake() {
 
        ic.inputJson.Total.Exit.performed += Exit;
        ic.inputJson.UI.Cancel.performed += ToBasic;
    }

    private void ToBasic(InputAction.CallbackContext context)
    {
        DialogBox.SetActive(false);
        ic.ToBasic();
    }

    public void ToUI(){
        DialogBox.SetActive(true);
        ic.ToUI();
    }

    private void Exit(InputAction.CallbackContext context)
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    private void OnDisable() {
      
        ic.inputJson.Total.Exit.performed -= Exit;
    }
}
