using UnityEngine;

public class GroundRay 
{
    float distance = 1.05f;
    GameObject player;
    public GroundRay(GameObject _player)
    {
        player = _player;
    }
    public bool IsGround()
    {
        Vector3 rayPosition = player.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        bool isGround = Physics.Raycast(ray, distance);
        return isGround;
    }
}
