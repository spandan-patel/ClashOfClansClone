using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageSpell : MonoBehaviour
{
    public float damageIncrease;
    public float speedIncrease;
    int currentPulse = 0;

    public float spellRadius;
    public int numberOfPulses;
    public float timeBTWPulses;

    List<TroopsManager> allTroops = new List<TroopsManager>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (TroopsManager t in FindObjectsOfType<TroopsManager>())
        {
            allTroops.Add(t);
        }

        StartCoroutine(StartRageEffect());
    }

    IEnumerator StartRageEffect()
    {
        while (currentPulse <= numberOfPulses)
        {
            RageEffect();

            
            //foreach (TroopsManager troops in allTroops)
            //{
            //    if(troops != null && (transform.position - troops.transform.position).sqrMagnitude > spellRadius)
            //    { 
            //        //Debug.Log("Dance");
            //        troops.RemoveRageEffect();
            //    }   
            //    
            //
            //}
                //RemoveRage();
            currentPulse++;
            //Debug.Log(currentPulse);
            //Debug.Log(time);

            yield return new WaitForSeconds(timeBTWPulses);
        }
        //Debug.Log("Rage over");
        foreach (TroopsManager troops in allTroops)
        {
            if (troops != null)
            {
                troops.RemoveRageEffect();
            }                
        }

        Destroy(gameObject);
    }

    void RageEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spellRadius);

        //List<TroopsManager> troops = new List<TroopsManager>();

        foreach (Collider insideCollider in colliders)
        {
            TroopsManager troops = insideCollider.GetComponent<TroopsManager>();

            if (troops != null)
            {
                //ragedTroops.Add(troops);
                //troops.inSideAnotherRage = true;
                troops.Rage(damageIncrease, speedIncrease, this.gameObject);
            }

            //else if(troops != null && (transform.position - troops.transform.position).sqrMagnitude > spellRadius)
            //{
            //    Debug.Log("moved");
            //    troops.RemoveRageEffect();
            //}
        }

        //foreach(TroopsManager troops in allTroops)
        //{
        //    if (troops != null && (transform.position - troops.transform.position).sqrMagnitude > spellRadius)
        //    {
        //        //Debug.Log("moved");
        //        troops.RemoveRageEffect();
        //    }
        //}
    }
}
