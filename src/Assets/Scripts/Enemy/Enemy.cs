using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    protected Animator anim;

    PhysicsCheck physicsCheck;

    [Header("基本属性")]
    public float speed;
    public float currentSpeed;
    public Vector3 faceDir;

    [Header("计时器")]
    public float waitTime;

    public float waitTimeCounter;
    public float hurtForce;

    public bool wait;
    public bool isHurt;
    public bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = speed;
        waitTimeCounter = waitTime;
    }
    private void Update()
    {
        faceDir = new Vector3 (transform.localScale.x, 0, 0);
        if((physicsCheck.touchLeftWall&&faceDir.x<0) || (physicsCheck.touchRightWall&&faceDir.x>0)||(!physicsCheck.isGround))
        {
            wait = true;
            if(!physicsCheck.isGround)
                rb.velocity = Vector2.zero;
        }
        TimeCounter();
    }
    private void FixedUpdate()
    {
        if(!wait&&!isHurt&&!isDead) 
            Move();
    }
    public virtual void Move()
    {
        if (!wait && !isHurt && !isDead)
            rb.velocity = new Vector2 (currentSpeed*faceDir.x, rb.velocity.y);
    }

    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter <=0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(-faceDir.x, 6, 6);
            }
            
        }
    }

    public void OnTakeDamage(Transform attackTrans)
    {
        
        if (attackTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(6, 6, 6);
        else if (attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(-6, 6, 6);

        isHurt = true;
        Vector2 dir = new Vector2(transform.position.x-attackTrans.position.x,0).normalized;
        StartCoroutine(OnHurt(dir));
    }
     private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        isHurt = false;
    }

    public void OnDie()
    {
        rb.velocity = Vector2.zero;
        gameObject.layer = 2;
        anim.SetBool("isDead", true);
        isDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
}
