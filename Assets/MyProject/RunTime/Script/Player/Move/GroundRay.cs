using UnityEngine;

/// <summary>
/// �ڒn����
/// </summary>
public class GroundRay 
{
    RaycastHit hit;
    float distance = 1.05f;
    GameObject player;
    public GroundRay(GameObject _player)
    {
        player = _player;
    }
    /// <summary>
    /// �ڒn���Ă��邩
    /// </summary>
    public bool IsGround()
    {
        Vector3 rayPosition = player.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        bool isGround = Physics.Raycast(ray, distance);
        return isGround;
    }
    /// <summary>
    /// �W�����v�o���邩
    /// </summary>
    public bool IsFly()
    {
        Vector3 rayPosition = player.transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.gameObject.tag.Contains("Fly"))
            {
                return true;
            }
        }
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red, 1f);
        return false;
    }
}
