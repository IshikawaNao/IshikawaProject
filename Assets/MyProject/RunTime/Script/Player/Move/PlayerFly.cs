using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : IFly
{
    RaycastHit hit;

    public void Fly(Rigidbody _rb, bool isFly, Animator anim)
    {
        if(isFly)
        {
            //anim.SetBool("IsJump", true);
            _rb.AddForce(0, 20, 0,ForceMode.Impulse);
        }
    }

    public bool FlyFrag(GameObject _plyer)
    {
        Vector3 rayPosition = _plyer.transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        if(Physics.Raycast(ray, out hit, 0.7f))
        {
            if(hit.collider.gameObject.tag.Contains("Fly"))
            {
                return true;
            }
        }
        Debug.DrawRay(rayPosition, Vector3.down * 0.7f, Color.red,1f);
        return false;
    }
}
