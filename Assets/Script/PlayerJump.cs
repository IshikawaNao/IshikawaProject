using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public void Jump(Rigidbody rb, float jumpPower)
    {
        Vector3 jumpVector = new Vector3(0, jumpPower, 0);
        rb.AddForce(jumpVector, ForceMode.Impulse);
    }
}
