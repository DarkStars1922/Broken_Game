using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private bool canPress;

    public InputJson inputJson;

    private IInteractable targetItem;

    private void Awake()
    {
        inputJson =new InputJson();
        inputJson.Enable();
    }
    private void OnEnable()
    {
        inputJson.Basic.Confirm.started += OnConfirm;
    }

    private void OnDisable()
    {
        inputJson.Basic.Confirm.started -= OnConfirm;
    }
    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();
        }
               
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }
}
