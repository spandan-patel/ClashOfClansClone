using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellIndicator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
                transform.position = new Vector3(hit.point.x, 0.01f, hit.point.z);
        }

        if (Input.GetMouseButtonUp(0))
            Destroy(gameObject);
    }
}
