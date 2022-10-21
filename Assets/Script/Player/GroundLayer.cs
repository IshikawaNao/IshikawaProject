using UnityEngine;

public class GroundLayer : MonoBehaviour
{
    float distance = 1.05f;
    public bool IsGround()
    {
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        bool isGround = Physics.Raycast(ray, distance);
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        return isGround;
    }
}
