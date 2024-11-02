using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public InputController ic;
    public LayerMask targetLayerMask;
    public bool onGround = true;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        ic.inputJson.Basic.Jump.performed += Jump;
        ic.inputJson.Basic.Interact.performed += Interact;
    }

    private void OnDisable() {
        ic.inputJson.Basic.Jump.performed -= Jump;
        ic.inputJson.Basic.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Collider2D[] results = new Collider2D[10];
        Physics2D.OverlapCircle(transform.position, 1.5f, new ContactFilter2D{useLayerMask=true, useTriggers=true, layerMask=targetLayerMask}, results);
        if(results[0]!=null) results[0].GetComponent<ShowContent>()?.ShowIt();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        rb.velocity += new Vector2(0,12);
    }

    private void Update() {
        Vector2 dir = ic.inputJson.Basic.Move.ReadValue<Vector2>();
        rb.velocity = new Vector2(5*dir.x, rb.velocity.y);
    }

}
