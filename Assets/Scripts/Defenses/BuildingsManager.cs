using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsManager : MonoBehaviour
{
    public enum TypeOfBuliding
    {
        Defence,
        NonDefence
    };
    public TypeOfBuliding bulidingType;

    public float startingHealth;
    protected float health;
    protected bool isDestroyed;

    //List<GameObject> troops = new List<GameObject>();    

    public Image healthUI;
    //float fadeTime = 2f;
    //float timeAfterDamage;

    protected virtual void Start()
    {
        health = startingHealth;

        //healthUI.gameObject.transform.LookAt(Camera.main.transform);
    }

    public virtual void TakeDamage(float damage)
    {
        //timeAfterDamage = 0;
        health -= damage;

        healthUI.fillAmount = health / startingHealth;

        if (health <= 0 && !isDestroyed)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDestroyed = true;
        GameObject.Destroy(gameObject);
    }
}


