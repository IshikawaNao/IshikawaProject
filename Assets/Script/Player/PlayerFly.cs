using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour, IFly
{
    RaycastHit hit;

    public void Fly(Rigidbody _rb, bool isFly)
    {
        print("a");
        if(isFly)
        {
            _rb.AddForce(0, 10, 0,ForceMode.Impulse);
        }
    }

    public bool FlyFrag(GameObject _plyer)
    {
        Vector3 rayPosition = _plyer.transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        if(Physics.Raycast(ray, out hit, 1f))
        {
            if(hit.collider.gameObject.tag.Contains("Fly"))
            {
                return true;
            }
        }
        Debug.DrawRay(rayPosition, Vector3.down * 1, Color.red,1f);
        return false;
    }
}
