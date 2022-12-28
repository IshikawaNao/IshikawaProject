using UnityEngine;

/// <summary>
/// ���ʃI�u�W�F�N�g���o��邩
/// </summary>
public class PlayerClimb : IClimb
{
    RaycastHit hit;
    Ray ray;

    // �o�鏈��
    public void ClimbPlayer(Rigidbody rb,Animator anim)
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsRan", false);
        anim.SetBool("IsClimb", true);
        rb.velocity = 6 * 0.8f * new Vector2(0, 1);
    }

    // player�̐��ʂɓo���I�u�W�F�N�g�����邩
    public bool Climb(GameObject _player)
    {
        Vector3 orgin = new Vector3(_player.transform.position.x, _player.transform.position.y , _player.transform.position.z);
        Vector3 direction = Vector3.Scale(_player.transform.forward, new Vector3(1, 0, 1));
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                return true;
            }
        }

        Debug.DrawRay(orgin, direction, Color.red, 1f);
        return false;
    }
}
