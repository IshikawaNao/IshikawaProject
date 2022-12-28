using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    HouseRoom hr;

    private void Update()
    {
        if(hr.Placed)
        {
            anim.SetBool("Open", true);
        }
        else
        {
            anim.SetBool("Open", false);
        }
    }
}
