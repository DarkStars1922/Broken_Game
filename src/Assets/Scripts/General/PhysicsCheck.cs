using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D cc;

    public Vector2 bottomOffset;

    public Vector2 leftOffset;

    public Vector2 rightOffset;

    public float checkRaudis;

    public LayerMask groundLayer;

    public bool manual;

    public bool isGround;

    public bool touchLeftWall;

    public bool touchRightWall;

    private void Awake()
    {
        cc = GetComponent<CapsuleCollider2D>();
        if(!manual)
        {
            rightOffset = new Vector2((cc.bounds.size.x + cc.offset.x) / 2, cc.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    public void Update()
    {
        Check();
    }

    public void Check() 
    {
       isGround =  Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRaudis,groundLayer);

        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaudis, groundLayer);

        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaudis, groundLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2 (bottomOffset.x*transform.localScale.x,bottomOffset.y), checkRaudis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaudis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaudis);
    }

}
