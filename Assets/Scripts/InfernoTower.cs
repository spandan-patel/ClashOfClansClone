using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoTower : DefenceManager
{
    List<TroopsManager> troops = new List<TroopsManager>();

    List<GameObject> troopsInsideAttackRange = new List<GameObject>();

    List<GameObject> firstTroopInside = new List<GameObject>();

    List<GameObject> attackAllTroops = new List<GameObject>();

    List<LineRenderer> laserBeams = new List<LineRenderer>();

    public Transform laserHead;
    public GameObject beam;

    float time;
    bool clearTroop = false;

    // Override the start from DefenceManagaer
    protected override void Start()
    {
        base.Start();

        //Debug.Log(renderer.bounds.size.x);

        ClearAddSort();

        //.Log("Total troops : " + troops.Count);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(damageType == DamageType.SINGLE)
            ClearAddSort();
        //create function for adding troops in list "troops" in real time
        for (int i = 0; i < troops.Count; i++)
        {
            if (troops[i] != null && Vector3.Distance(transform.position, troops[i].transform.position) <= attackRadius)
            {
                if (damageType == DamageType.SINGLE)
                {
                    DamageOverTime(troops[i].gameObject);

                    //StartCoroutine(DamageOverTime(troops[i].gameObject));
                }

                else if (damageType == DamageType.AREA)
                {
                    DamageAllTroops(troops[i].gameObject);

                    troops[i].GetComponent<TroopsManager>().TakeDamage(damage / 50f);
                }
            }

            //if (troops.Count == 0)
            //{
            //    foreach(LineRenderer laser in FindObjectsOfType<LineRenderer>())
            //    {
            //        Destroy(laser);
            //    }
            //}        
            //
            //else if(firstTroopInside.Count == 0 && troops.Count != 0)
            //{
            //    foreach (LineRenderer laser in FindObjectsOfType<LineRenderer>())
            //    {
            //        Destroy(laser);
            //    }
            //}
        }
    }

    void DamageAllTroops(GameObject allTroop)
    {
        //if(!attackAllTroops.Contains(allTroop))
        //    attackAllTroops.Add(allTroop);
        //
        //
        //if (allTroop != null)
        //{
        //    //if(attackAllTroops.Count != transform.childCount - 3)
        //        GameObject laser = Instantiate(beam, transform) as GameObject;
        //
        //    laser.GetComponent<LaserBeam>().target = allTroop.transform;
        //
        //    laser.GetComponent<LineRenderer>().SetPosition(0, laserHead.position);
        //    laser.GetComponent<LineRenderer>().SetPosition(1, allTroop.GetComponentInChildren<MeshRenderer>().gameObject.transform.position);
        //}
        //
        //else if(allTroop == null)
        //{
        //    attackAllTroops.Remove(allTroop);
        //    this.GetComponentInChildren<LaserBeam>().hasTarget = false;
        //}

        //bool laserInstantiated;

        if (!attackAllTroops.Contains(allTroop))
        {
            attackAllTroops.Add(allTroop);
        
            GameObject laser = Instantiate(beam, transform) as GameObject;
        
            //if(!laserBeams.Contains(laser.GetComponent<LineRenderer>()))
            laserBeams.Add(laser.GetComponent<LineRenderer>());
        
            laser.GetComponent<LaserBeam>().target = allTroop.transform;
        
        
            //
            //Debug.Log(laserBeams.Count);
        }
        
        for(int i = 0; i < attackAllTroops.Count; i++)
        {
            //if(!laserBeams.Contains(FindObjectOfType<LineRenderer>()))
            //if(laserBeams[i] == null)
            //laserBeams.Add(gameObject.GetComponentsInChildren<LineRenderer>[i]())
        
        
            //laserBeams.AddRange(FindObjectsOfType<LineRenderer>());
            //if(laserBeams.Count != i + 1)
            //    laserBeams.Add(transform.GetComponentInChildren<LineRenderer>());
        
            //if (laserBeams.Count != attackAllTroops.Count)
                
        
            //Debug.Log(laserBeams.Count);
        
            //beam.positionCount = attackAllTroops.Count * 2;
            if (attackAllTroops[i] != null && laserBeams[i] != null)
            {
                laserBeams[i].SetPosition(0, laserHead.position);
                laserBeams[i].SetPosition(1, attackAllTroops[i].GetComponentInChildren<MeshRenderer>().gameObject.transform.position);
        
                //attackAllTroops[i].GetComponent<TroopsManager>().TakeDamage(damage / 50f);
        
                //yield return null;
            }            
        
            else if(attackAllTroops[i] == null && laserBeams[i] != null)
            {
                //Debug.Log("Kill");
                laserBeams[i].GetComponent<LaserBeam>().hasTarget = false;
                laserBeams.Remove(laserBeams[i]);
        
                attackAllTroops.Remove(attackAllTroops[i]);
                //Debug.Log(attackAllTroops.Count);
            }
        }

        //if (attackAllTroops.Count == 1)
        //    Destroy(GetComponentInChildren<LineRenderer>().gameObject);
    }

    void DamageOverTime(GameObject troopInside)
    {
        //float simpleDamage = 0f;
        //float time = 0f;        

        if(!firstTroopInside.Contains(troopInside))
            firstTroopInside.Add(troopInside);

        //Debug.Log(firstTroopInside.Count);

        //firstTroopInside.Sort()

        if (firstTroopInside[0] != null && firstTroopInside[0] == troopInside)
        {
            if (!transform.GetComponentInChildren<LineRenderer>())
            {
                GameObject laser = Instantiate(beam, transform) as GameObject;

                //LineRenderer laserBeam = laser.GetComponent<LineRenderer>();
                // laserBeam.SetPosition(0, laserHead.position);
                //laserBeam.SetPosition(1, t.GetComponentInChildren<MeshRenderer>().gameObject.transform.position);
            }

            LineRenderer laserBeam = GetComponentInChildren<LineRenderer>();
            //firstTroop = true;
            StartCoroutine(ApplyDamage(firstTroopInside[0], laserBeam));
            //Debug.Log("After Starting coroutine");
        }

        else if(firstTroopInside[0] == null)
        {
            LineRenderer laserBeam = GetComponentInChildren<LineRenderer>();
            //Debug.Log(laserBeam.name);
            StopFirstAttack(laserBeam);
            clearTroop = true;
            ClearAddSort();
            //Debug.Log(troops.Count);
        }
        //else if (firstTroopInside[0] != troopInside || firstTroopInside[0] == null)
        //{
        //    Debug.Log("After Starting coroutine");
        //    firstTroop = false;
        //    //StartCoroutine(ApplyDamage(firstTroopInside[0]));
        //    //StopFirstAttack(firstTroopInside[0]);
        //}
            
        /*while (firstTroopInside[0] != null && troopInside != null)
        {
            //Debug.Log(troopsInsideSingleTarget[0].name);
            time = 0;
            simpleDamage = 0;

            if (time < 1.5f)
            {
                simpleDamage = damage;
                firstTroopInside[0].GetComponent<TroopsManager>().TakeDamage(simpleDamage / 50f);
            }

            else if(time > 1.5f && time < 5.25f)
            {
                simpleDamage = damage * 3f;
                firstTroopInside[0].GetComponent<TroopsManager>().TakeDamage(simpleDamage / 50f);
                
            }

            else
            {
                simpleDamage = damage * 30f;
                firstTroopInside[0].GetComponent<TroopsManager>().TakeDamage(simpleDamage / 50f);
            }

            time += Time.deltaTime;

            yield return null;
        }*/
        
        //if(firstTroopInside.Count > 0
        //else
            //firstTroopInside.Remove(troopInside);
    }

    IEnumerator ApplyDamage(GameObject t, LineRenderer laser)
    {
        float simpleDamage = 0f;
        //float time = 0f;
        //bool newTroop = true;

        //Debug.Log(t.name);

                   

        //float startWidth = beam.startWidth { };
        //float endWidth = beam.endWidth;


        if (t != null)
        {

            laser.GetComponent<LaserBeam>().target = t.transform;
            laser.SetPosition(0, laserHead.position);
            laser.SetPosition(1, t.GetComponentInChildren<MeshRenderer>().gameObject.transform.position);

            //Debug.Log(troopsInsideSingleTarget[0].name);
            time += Time.fixedDeltaTime;
            simpleDamage = 0;

            if (time % 60 < 1.5f)
            {
                simpleDamage = damage;
                laser.startWidth = 0.2f;
                laser.endWidth = 0.2f;
                t.GetComponent<TroopsManager>().TakeDamage(simpleDamage / 50f);
            }

            else if (time % 60 > 1.5f && time % 60 < 5.25f)
            {
                simpleDamage = damage * 3f;
                laser.startWidth = 0.4f;
                laser.endWidth = 0.4f; 
                t.GetComponent<TroopsManager>().TakeDamage(simpleDamage / 50f);
            }

            else
            {
                simpleDamage = damage * 30f;
                laser.startWidth = 0.6f;
                laser.endWidth = 0.6f;
                t.GetComponent<TroopsManager>().TakeDamage(simpleDamage / 50f);
            }

            //time += Time.fixedDeltaTime; ;

            //yield return null;       

            
        }

        //newTroop = true;

        //Destroy(transform.GetComponentInChildren<LineRenderer>().gameObject);
        //firstTroopInside.Remove(t);

        yield break;    
    }

    void StopFirstAttack(LineRenderer laserToDestroy)
    {
        //Debug.Log("Hi");
        time = 0;
        //Debug.Log(transform.GetComponentInChildren<LineRenderer>());
        laserToDestroy.GetComponent<LaserBeam>().hasTarget = false;
        //firstTroopInside.Remove(t);
    }

    void ClearAddSort()
    {
        //troops.Clear();

        foreach (TroopsManager t in FindObjectsOfType<TroopsManager>())
        { 
            if(!troops.Contains(t))
                troops.Add(t);
        }
        if (clearTroop)
        {
            firstTroopInside.Clear();

            if (firstTroopInside != null)
            {
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
    }
}
