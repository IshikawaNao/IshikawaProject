using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    HouseRoom hr;
    [SerializeField]
    Collider collider;

    private void Update()
    {
        if(hr.Placed)
        {
            anim.SetBool("Open", true);
            collider.isTrigger = true;
        }
        else
        {
            anim.SetBool("Open", false);
            collider.isTrigger = false;
        }
    }
}
