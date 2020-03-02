using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform target;
    public bool hasTarget;

    // Start is called before the first frame update
    void Start()
    {
        hasTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTarget)
        {
            Destroy(gameObject);
            return;
        }

        if (target == null)
            Destroy(gameObject);
    }
}
