using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopsManager : MonoBehaviour
{
    public enum TroopType
    {
        AIR,
        GROUND
    };
    public TroopType troopType;

    public enum TroopPrefferedTarget
    {
        DEFENCE,
        ALL
    };
    public TroopPrefferedTarget prefferedTarget;

    public enum DamageType
    {
        SINGLE,
        AREA
    };
    public DamageType damageType;

    public float attackRadius;
    public float attackSpeed;
    public float damage;

    float normalDamage;
    float normalMoveSpeed;
    Vector3 normalScale;

    public float startingHealth;
    protected float health;
    bool isDestroyed;

    public float moveSpeed;

    public bool alreadyRaged = false;
    //public bool inSideAnotherRage = false;

    public Image healthBar;
    //float fadeTime = 2f;
    //float timeAfterDamageHeal;

    //public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;

        gameObject.layer = 9;

        normalDamage = damage;
        normalMoveSpeed = moveSpeed;
        normalScale = gameObject.transform.localScale;
        //healthBar.gameObject.transform.LookAt(Camera.main.transform);
    }

    public virtual void TakeDamage(float damage)
    {
        //timeAfterDamageHeal = 0;
        health -= damage;

        healthBar.fillAmount = health / startingHealth;

        if (health <= 0 && !isDestroyed)
        {
            Die();
        }
    }

    public void Heal(float amountHeal)
    {
        //timeAfterDamageHeal = 0;
        health += amountHeal;

        healthBar.fillAmount = health / startingHealth;

        if(health >= startingHealth)
        {
            health = startingHealth;
        }
    }

    public void Rage(float damageIncrease, float speedIncrease, GameObject rageSpell)
    {
        if(alreadyRaged == false)
        {
            
            //inSideAnotherRage = true;

            moveSpeed += moveSpeed * (speedIncrease / 100);

            damage += damage * ((100 + damageIncrease) / 100);

            gameObject.transform.localScale = Vector3.one * 1.2f;
            alreadyRaged = true;
        }

        if ((transform.position - rageSpell.transform.position).sqrMagnitude > rageSpell.GetComponent<RageSpell>().spellRadius)
            RemoveRageEffect();
    }

    public void RemoveRageEffect()
    {
        if(alreadyRaged == true)
        {
            
            //inSideAnotherRage = false;

            moveSpeed = normalMoveSpeed;

            damage = normalDamage;

            gameObject.transform.localScale = normalScale;
            alreadyRaged = false;
        }        
    }
    protected virtual void Die()
    {
        isDestroyed = true;
        
        GameObject.Destroy(gameObject,0.1f);
    }
}
