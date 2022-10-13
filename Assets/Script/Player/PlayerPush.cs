using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public bool Push(float rayDistance)
    {
        Vector3 orgin = transform.position;
        Vector3 direction = new Vector3(0, 0, rayDistance);
        Ray ray = new Ray(orgin, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if(hit.collider.gameObject.tag.Contains("Move"))
            {
                return true;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        return false;
    }
}
