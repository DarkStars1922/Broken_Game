using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
    public Transform caremaTrans;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private int jumptime;
    private PlayerAnimation playerAnimation;
    public InputController ic;
    public CapsuleCollider2D cc;
    public LayerMask targetLayerMask;
    public bool onGround = true;
    public Vector2 dir;

    public float hurtForce;

    [Header("状态")]
    public bool isHurt;

    public bool isDead;

    public bool isAttack;

    [Header("材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        ic.inputJson.Basic.Attack.started += PlayerAttack;
    }

   
    private void Update()
    {
        if (!isHurt && !isAttack)
        {
            dir = ic.inputJson.Basic.Move.ReadValue<Vector2>();
            rb.velocity = new Vector2(5 * dir.x, rb.velocity.y);
        }
            
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
        if (physicsCheck.isGround)
            jumptime = 1; 
        if (jumptime <= 2)
        {
            rb.velocity = new Vector2(0, 12);
            jumptime++;
        }
    }
    private void FixedUpdate()
    {
        if(!isHurt&&!isAttack)
            Move();
        

    }
    public void Move()
    {
        int faceDir = (int)transform.localScale.x;
        if (dir.x < 0)
            faceDir = -1;
        else if (dir.x > 0)
            faceDir = 1;
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir2 = new Vector2((transform.position.x - attacker.position.x),0).normalized;
        rb.AddForce(dir2 * hurtForce, ForceMode2D.Impulse);

    }

    public void PlayerDead()
    {
        isDead = true;
        caremaTrans.gameObject.SetActive(false);
        ic.inputJson.Disable();
    }

    private void CheckState()
    {
        cc.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }

}
