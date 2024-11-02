using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    [Header("人物属性")]
    public float maxHealth;
    public float currentHealth;

    [Header("无敌时间")]
    public float invulnerableDuration;
    public float invulnerableCounter;
    public bool invulnerable;


    public UnityEvent<Transform> OntakeDamage;
    public UnityEvent OnDie;

    public UnityEvent<Character> OnHealthChange;
    public void Start()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }

    public void Update()
    {
        if(invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if(invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Void"))
        {
            currentHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDie?.Invoke();
        }
    }
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
            return;
        if (currentHealth - attacker.damage>0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            OntakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            OnDie?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }

   public void TriggerInvulnerable()
    {
        if(!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

}
