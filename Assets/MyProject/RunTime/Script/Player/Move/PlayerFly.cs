using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : IFly
{
    RaycastHit hit;
    // �W�����v�����
    private const float jumpPower = 20;
    /// <summary>
    /// 
    /// </summary>
    public void Fly(Rigidbody _rb, bool isFly, Animator anim)
    {
        if(isFly)
        {
            _rb.AddForce(0, jumpPower, 0,ForceMode.Impulse);
        }
    }
    /// <summary>
    /// �W�����v�o���邩
    /// </summary>
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
