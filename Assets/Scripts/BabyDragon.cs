using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDragon : TroopsManager
{
    List<Transform> allBuildings = new List<Transform>();

    List<Transform> remainingBuildings = new List<Transform>();

    Transform firstBuilding;

    float resetSpeed;

    public GameObject spawnPoint;
    //Transform spawnPoint;
    public GameObject projectile;

    public float projectileSpeed;

    bool aloneInRadius;
    public Material[] babyMaterials;

    private enum State
    {
        CHASE,
        ATTACK
    };
    State state;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //isDestroyed = false;

        ClearAddSort();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForAirTroops();

        for (int i = 0; i < allBuildings.Count; i++)
        {
            if (allBuildings[i] != null)
            {
                Renderer renderer = allBuildings[i].GetChild(0).GetComponent<Renderer>();
                float centerToEdgeDistance = renderer.bounds.size.x;

                if (state == State.CHASE && !(((transform.position - allBuildings[i].position).sqrMagnitude) - centerToEdgeDistance <= attackRadius))
                    StartCoroutine(GoToTheBuilding(allBuildings[i]));

                else if (state == State.CHASE && ((transform.position - allBuildings[i].position).sqrMagnitude) - centerToEdgeDistance <= attackRadius)
                {
                    StopCoroutine(GoToTheBuilding(allBuildings[i]));
                    state = State.ATTACK;
                    //Debug.Log(state);
                    transform.position = transform.position;
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
                    StartCoroutine(DamageBuilding(allBuildings[i].gameObject));
                }
            }
        }
    }
    void CheckForAirTroops()
    {
        List<TroopsManager> troops = new List<TroopsManager>();

        foreach(TroopsManager t in FindObjectsOfType<TroopsManager>())
        {
            if (!troops.Contains(t))
            {
                troops.Add(t);
            }

            if(t.troopType != TroopType.AIR)
            {
                troops.Remove(t);
            }
        }

        troops.Remove(this);


        if(troops.Count == 0)
        {
            aloneInRadius = true;
            transform.GetComponentInChildren<MeshRenderer>().material = babyMaterials[1];
        }

        else if(troops.Count >= 1)
        {
            //Debug.Log("More troops");
            foreach(TroopsManager t in troops)
            {
                Vector3 troopPos = t.transform.position;
                Vector3 babyPos = transform.position;

                //Debug.Log(t.transform.name + ((troopPos - babyPos).sqrMagnitude));
                if ((troopPos - babyPos).sqrMagnitude <= 250f && t != null)
                {
                    aloneInRadius = false;
                    transform.GetComponentInChildren<MeshRenderer>().material = babyMaterials[0];
                }

                else if ((troopPos - babyPos).sqrMagnitude > 250f && t != null)
                {
                    aloneInRadius = true;
                    transform.GetComponentInChildren<MeshRenderer>().material = babyMaterials[1];
                }
            }
        }
    }
    IEnumerator GoToTheBuilding(Transform building)
    {
        resetSpeed = 0.02f;

        if (!remainingBuildings.Contains(building))
            remainingBuildings.Add(building);

        //Debug.Log(remainingBuildings.Count);

        while (state == State.CHASE && building != null)
        {
            transform.LookAt(remainingBuildings[0]);

            //transform.position = Vector3.MoveTowards(transform.position, remainingBuildings[0].position, moveSpeed * Time.fixedDeltaTime / 50f);

            //transform.position = Vector3.Lerp(transform.position, remainingBuildings[0].position, moveSpeed / 50f);

            transform.position += transform.forward * moveSpeed * resetSpeed / 50f;

            yield return null;
        }

        //if (remainingBuildings[0] == null)
        //    yield break;

        //if(remainingBuildings[0] == null)
        remainingBuildings.Remove(building);

        yield break;
    }

    IEnumerator DamageBuilding(GameObject building)
    {
        while (building != null && state == State.ATTACK)
        {
            //Vector3 spawnPos = spawnPoint.transform.forward;
            GameObject fireProjectile = Instantiate(projectile, transform) as GameObject;
            fireProjectile.transform.position = spawnPoint.transform.position;

            //fireProjectile.transform.position = spawnPos;

            //pass the damage inside SHOOT script and when projectile reach destination apply damage
            fireProjectile.GetComponent<Shoot>().target = building.transform;
            fireProjectile.GetComponent<Shoot>().projectileSpeed = projectileSpeed;

            if(aloneInRadius)
                fireProjectile.GetComponent<Shoot>().damage = damage * 2f;

            else
                fireProjectile.GetComponent<Shoot>().damage = damage;
            fireProjectile.GetComponent<Shoot>().isBuilding = true;
            //building.GetComponent<BuildingsManager>().TakeDamage(damage);

            for (int i = 1; i < allBuildings.Count; i++)
            {
                if (allBuildings[i] != null && (building.transform.position - allBuildings[i].transform.position).sqrMagnitude <= 0.3f)
                {
                    allBuildings[i].GetComponent<BuildingsManager>().TakeDamage(damage);
                }
            }

            //foreach(BuildingsManager b in allBuildings)
            //{
            //    if((building.transform.position - b.transform.position).sqrMagnitude <= 0.3f)
            //    {
            //        b.GetComponent<>
            //    }
            //}

            yield return new WaitForSeconds(attackSpeed);
        }

        if (building == null && allBuildings.Count > 1)
        {
            state = State.CHASE;
            resetSpeed = 0f;

            ClearAddSort();

            yield break;
            //allBuildings.Sort(SortByDistance);
            //Debug.Log(state);
            //Debug.Log(allBuildings[0].name);
        }
    }

    void ClearAddSort()
    {
        allBuildings.Clear();

        foreach (BuildingsManager t in FindObjectsOfType<BuildingsManager>())
        {
            allBuildings.Add(t.transform);
        }

        if (allBuildings != null)
        {
            allBuildings.Sort(SortByDistance);
            state = State.CHASE;
        }
    }

    int SortByDistance(Transform a, Transform b)
    {
        float squaredRangeA, squaredRangeB;
        //float squaredRangeB = (b.position - transform.position).sqrMagnitude;

        // (a != null)
        {
            squaredRangeA = (a.position - transform.position).sqrMagnitude;
            squaredRangeB = (b.position - transform.position).sqrMagnitude;
        }
        //else
           // squaredRangeA = 0f;

        return squaredRangeA.CompareTo(squaredRangeB);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
