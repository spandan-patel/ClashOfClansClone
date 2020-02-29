using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform target;
    public float projectileSpeed;
    public float damage;

    public bool hasTarget;
    public bool isTroop = false;
    public bool isBuilding = false;

    private void Start()
    {
        hasTarget = true;
    }

    private void Update()
    {
        if (!hasTarget)
        {
            Destroy(gameObject);
            return;
        }

        if (target != null)
        {
            //Destroy(gameObject);

            Transform centerOfTarget = target.GetComponentInChildren<MeshRenderer>().gameObject.transform;
            transform.LookAt(target.transform);
            //transform.localPosition += Vector3.forward * Time.deltaTime * 10f;

            if (transform.position != centerOfTarget.position && target != null)
                transform.position = Vector3.MoveTowards(transform.position, centerOfTarget.position, projectileSpeed);

            else if (transform.position == centerOfTarget.position)
            {
                if (isTroop)
                {
                    target.GetComponent<TroopsManager>().TakeDamage(damage);
                    Destroy(gameObject);
                }

                else if (isBuilding)
                {
                    target.GetComponent<BuildingsManager>().TakeDamage(damage);
                    Destroy(gameObject);

                }
            }
        }        
        //create a bool to destroy projectiles...
        //access this bool on archor.cs 61 and wizardtower.cs 62...
        //check condition here for that bool to destroy this projectile
    }
}
