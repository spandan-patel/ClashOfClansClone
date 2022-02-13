using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : DefenceManager
{
    List<TroopsManager> troops = new List<TroopsManager>();

    List<GameObject> firstTroopInside = new List<GameObject>();

    public GameObject wizard;
    //Transform spawnPoint;
    public GameObject projectile;

    float nextShotTime;
    public float projectileSpeed;

    bool clearTroop = false;

    protected override void Start()
    {
        base.Start();
        //attackSpeed = 1.3f;

        //Mesh mesh = transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        //
        //Debug.Log(mesh.bounds.size.x);

        ClearAddSort();

        //spawnPoint = GetComponentInChildren()

        //.Log("Total troops : " + troops.Count);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ClearAddSort();

        for (int i = 0; i < troops.Count; i++)
        {
            if(troops[i] != null && (transform.position - troops[i].transform.position).sqrMagnitude <= attackRadius)
            {
                DamageTroop(troops[i].gameObject);
            }

            //if(troops[i] == null)
            //{
            //    troops.Remove(troops[i].GetComponent<TroopsManager>());
            //}
        }
    }

    void DamageTroop(GameObject insideTroop)
    {
        if (!firstTroopInside.Contains(insideTroop))
            firstTroopInside.Add(insideTroop);

        if(firstTroopInside[0] != null && firstTroopInside[0] == insideTroop)
        {
            StartCoroutine(ApplyDamage(firstTroopInside[0]));
            //ApplyDamage(firstTroopInside[0]);
        }

        else if(firstTroopInside[0] == null)
        {
            StopAttack();            
        }

        //else if (firstTroopInside[0] != insideTroop)
        //    StopCoroutine(ApplyDamage(firstTroopInside[0]));
        //firstTroopInside.Remove(insideTroop);

        //yield break;         
    }

    IEnumerator ApplyDamage(GameObject first)
    {
         

        //float time = 0;
        //bool attack = false;
        if (first != null)
        {
            //transform.GetChild(1).LookAt(first.transform);
            //transform.GetChild(1).rotation = Quaternion.Euler(0f, transform.GetChild(1).rotation.y, 0f);

            Vector3 dir = first.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            
            //Debug.Log(rotation.y);
            
            wizard.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            

            //wizard.transform.localRotation = Quaternion.Euler(0f, wizard.transform.rotation.y, 0f);

            //Vector3 spawnPos = wizard.transform.forward + wizard.transform.position;
            //spawnPoint.position = spawnPos;

            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + attackSpeed;



                //Instansiate or object pooling of wizard splash
                Vector3 spawnPos = wizard.transform.forward + wizard.transform.up + wizard.transform.position;
                GameObject fireProjectile = Instantiate(projectile, transform) as GameObject;
                fireProjectile.transform.position = spawnPos;
                
                //fireProjectile.transform.position = spawnPos;

                //pass the damage inside SHOOT script and when projectile reach destination apply damage
                fireProjectile.GetComponent<Shoot>().target = first.transform;
                fireProjectile.GetComponent<Shoot>().projectileSpeed = projectileSpeed;
                fireProjectile.GetComponent<Shoot>().damage = damage;
                fireProjectile.GetComponent<Shoot>().isTroop = true;

                

                //first.GetComponent<TroopsManager>().TakeDamage(damage);

                for (int i = 1; i < firstTroopInside.Count; i++)
                {
                    if(firstTroopInside[i] != null && (first.transform.position - firstTroopInside[i].transform.position).sqrMagnitude <= 1f)
                    {
                        firstTroopInside[i].GetComponent<TroopsManager>().TakeDamage(damage);
                    }
                }

                yield return new WaitForSeconds(0.2f);
            }
            //bool attack = false;
            //time += Time.fixedDeltaTime;

            //if(!attack)
            //{
                //attack = true;
                ////time = 0;
                //first.GetComponent<TroopsManager>().TakeDamage(damage);
                //
                //yield return new WaitForSeconds(attackSpeed);
                //
                //attack = false;
            //}
            //wizard.transform.LookAt(first.transform);
            //wizard.transform.rotation = Quaternion.Euler(0f, wizard.transform.rotation.y, 0f);

            //Debug.Log(attackSpeed);
            //Debug.Log(damage);
            //yield return new WaitForSeconds(attackSpeed);

            

            //for (int i = 1; i < firstTroopInside.Count; i++)
            //{
            ////    List<GameObject> insideRadiusOfOne = new List<GameObject>();
            ////    
            //    if(firstTroopInside[i] != null && (first.transform.position - firstTroopInside[i].transform.position).sqrMagnitude <= 1f)
            //    {
            //        firstTroopInside[i].GetComponent<TroopsManager>().TakeDamage(damage);
            //        //if (!insideRadiusOfOne.Contains(firstTroopInside[i]))
            //        //    insideRadiusOfOne.Add(firstTroopInside[i]);
            //    }
            //
            //    //if (insideRadiusOfOne[i] != null)
            //    //{
            //    //    insideRadiusOfOne[i].GetComponent<TroopsManager>().TakeDamage(damage);
            //    //}
            //    //else if(insideRadiusOfOne[i] == null)
            //    //    firstTroopInside.Remove(insideRadiusOfOne[i]);
            //}
            //float time = 0;
            //
            //time += Time.fixedDeltaTime;
            
            //yield return null;

            //Debug.Log(time);
        }

        //firstTroopInside.Remove(first);
        yield break;
    }

    void StopAttack()
    {
        clearTroop = true;
        ClearAddSort();
        //firstTroopInside.Remove(t);
        if (transform.GetComponentInChildren<Shoot>())
            transform.GetComponentInChildren<Shoot>().hasTarget = false;
        //Destroy
    }

    void ClearAddSort()
    {
        //troops.Clear();

        foreach (TroopsManager t in FindObjectsOfType<TroopsManager>())
        {
            if (!troops.Contains(t))
                troops.Add(t);
        }
        if (clearTroop)
        {
            firstTroopInside.Clear();

            if (firstTroopInside != null)
            {
                //Debug.Log(firstTroopInside)
                firstTroopInside.Sort(SortByDistance);
                //state = State.CHASE;
            }

            clearTroop = false;
        }
    }

    int SortByDistance(GameObject a, GameObject b)
    {
        float squaredRangeA, squaredRangeB;
        //float squaredRangeB = (b.position - transform.position).sqrMagnitude;

        //if (a != null)
        {
            squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
            squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        }
        // else
        // squaredRangeA = 0f;

        return squaredRangeA.CompareTo(squaredRangeB);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Vector3 spawnPos = wizard.transform.forward + wizard.transform.up + wizard.transform.position;

        Gizmos.DrawWireSphere(spawnPos, 0.5f);
    }
}
