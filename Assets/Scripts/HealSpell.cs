using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    public float healAmount;
    int currentPulse = 0;

    public float spellRadius;
    public int numberOfPulses;
    public float timeBTWPulses;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(StartHealEffect());
    }

    IEnumerator StartHealEffect()
    {
        while(currentPulse <= numberOfPulses)
        {
            HealEffect();
            
            currentPulse++;
            //Debug.Log(currentPulse);
            //Debug.Log(time);

            yield return new WaitForSeconds(timeBTWPulses);
        }

        Destroy(gameObject);
    }

    void HealEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spellRadius);

        //List<TroopsManager> troops = new List<TroopsManager>();

        foreach(Collider insideCollider in colliders)
        {
            TroopsManager troops = insideCollider.GetComponent<TroopsManager>();

            if( troops != null)
            {
                troops.Heal(healAmount);
            }          
        }
    }
}
