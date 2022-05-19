using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Update()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        Debug.DrawRay(transform.position,transform.forward * 100,Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Student>().isCheating)
            {
                Destroy(hit.collider.gameObject.GetComponent<Student>().cheatItem1);
            }
        }

    }
}
