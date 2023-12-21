using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    DetectionObject detection;
    [SerializeField]
    Collider collider;

    private void Update()
    {
        if(detection.Placed)
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
