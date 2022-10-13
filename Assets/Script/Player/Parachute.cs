using UnityEngine;

public class Parachute : MonoBehaviour
{
    public void Para(Rigidbody rb, float drag , bool trigger)
    {
        if(!trigger)
        {
            rb.drag = drag;
        }
        else
        {
            rb.drag = 0;
        }
    }
}
