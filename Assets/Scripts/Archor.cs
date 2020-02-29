using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archor : TroopsManager
{
    List<Transform> allBuildings = new List<Transform>();

    List<Transform> remainingBuildings = new List<Transform>();

    Transform firstBuilding;

    float resetSpeed;

    public float projectileSpeed;

    public GameObject arrowPrefab;
    public Transform spawnPoint;

    private enum State
    {
        IDLE,
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

            //else if(allBuildings[i] == null && state != State.IDLE)
            //{
            //    //Debug.Log("Building destroyed");
            //    
            //}
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
            //Debug.Log("Attack");
            GameObject arrow = Instantiate(arrowPrefab, transform) as GameObject;
            arrow.transform.position = spawnPoint.position;
            //arrow.transform.LookAt(building.transform);
            //arrow.transform.position += Vector3.forward * Time.deltaTime * 10f;

            //pass the damage inside SHOOT script and when projectile reach destination apply damage
            arrow.GetComponent<Shoot>().target = building.transform;
            arrow.GetComponent<Shoot>().projectileSpeed = projectileSpeed;
            arrow.GetComponent<Shoot>().damage = damage;
            arrow.GetComponent<Shoot>().isBuilding = true;

            //building.GetComponent<BuildingsManager>().TakeDamage(damage);

            yield return new WaitForSeconds(attackSpeed);
        }

        if (building == null && allBuildings.Count > 1)
        {
            
            state = State.CHASE;
            resetSpeed = 0f;

            if(transform.GetComponentInChildren<Shoot>())
                transform.GetComponentInChildren<Shoot>().hasTarget = false;
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
