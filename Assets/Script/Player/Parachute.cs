using UnityEngine;

public class Parachute : MonoBehaviour
{
    public void Para(Rigidbody rb, bool trigger)
    {
        if(!trigger)
        {
            rb.drag = 5;
        }
        else
        {
            rb.drag = 0;
        }
    }

    public bool ParaFrag(Rigidbody rb)
    {
        if(rb.drag == 0)
        {
            return false;
        }
        return true;
    }
}
