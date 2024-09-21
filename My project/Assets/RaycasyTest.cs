using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasyTest : MonoBehaviour
{
    private void Start()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.left,out hit, 10))
        {
            Debug.DrawRay(transform.position, Vector3.left, Color.red, 10f);

            Debug.Log("hit: " + hit.collider.name);
        }
    }
}
