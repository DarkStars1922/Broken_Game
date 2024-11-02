using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public InputJson inputJson;

    private void Awake() {
        inputJson = new InputJson(); 
    }

    private void OnEnable() {
        inputJson.UI.Enable();
        inputJson.Total.Enable();
    }

    private void OnDisable() {
        inputJson.Disable();    
    }

    public void ToBasic(){
        inputJson.UI.Disable();
        inputJson.Basic.Enable();
    }

    public void ToUI(){
        inputJson.Basic.Disable();
        inputJson.UI.Enable();
    }
}
