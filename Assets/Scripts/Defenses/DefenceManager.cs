using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceManager : BuildingsManager
{
    public enum TargetType
    {
        AIR,
        GROUND,
        BOTH
    };
    public TargetType attackType;

    public enum DamageType
    {
        SINGLE,
        AREA
    };

    public DamageType damageType;
    public float attackRadius;
    public float attackSpeed;
    public float damage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
