using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public enum SpellType
    {
        HEAL,
        RAGE
    };
    public SpellType spellType;
    SpellType selectedSpell;

    public GameObject indicator;
    public GameObject healSpell;
    public GameObject rageSpell;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        selectedSpell = spellType;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool instantiated = false;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Ground") && instantiated == false)
                {
                    instantiated = true;
                    GameObject spellIndicator = Instantiate(indicator, transform) as GameObject;
                    spellIndicator.transform.position = new Vector3(hit.point.x, 0.01f, hit.point.z);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Destroy(gameObject.GetComponentInChildren<ParticleSystem>().gameObject);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    DropSpell(hit.point);
                }
            }
        }
    }

    void DropSpell(Vector3 point)
    {
        if(selectedSpell == SpellType.HEAL)
        {
            GameObject heal = Instantiate(healSpell, transform) as GameObject;

            heal.transform.position = new Vector3(point.x, 0.01f, point.z);
        }

        else if(selectedSpell == SpellType.RAGE)
        {
            GameObject rage = Instantiate(rageSpell, transform) as GameObject;

            rage.transform.position = new Vector3(point.x, 0.01f, point.z);
        }
    }
}
