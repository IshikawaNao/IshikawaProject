using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g������
/// </summary>
public class PushObject 
{
    const float RayLength = 1f;
    bool isPush = false;        // �I�u�W�F�N�g�������t���O
    public bool IsPush { get { return isPush; } }

    Vector3 checkVec = new(1, 0, 1);
    
    RaycastHit hit;
    Ray ray;

    // �v���C���[
    GameObject player = null;
    // �v���C���[�A�j���[�V����
    Animator anim;

    Rigidbody rb = null;
    public Rigidbody PushRb { get { return rb; } }

    public PushObject(GameObject _player, Animator _anim)
    {
        player = _player;
        anim = _anim;
    }

    // player�̐��ʂɓ�������I�u�W�F�N�g�����邩
    public bool CanPush()
    {
        Vector3 orgin = player.transform.position;
        Vector3 direction = Vector3.Scale(player.transform.forward, checkVec);
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, RayLength))
        {
            // ��������I�u�W�F�N�g�̏ꍇ���̃I�u�W�F�N�g�̏����擾
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                rb = hit.rigidbody;
                return true;
            }
        }
        rb = null;
        return false;
    }

    // �v�b�V�����I�����ꂽ���̏���
    public void PushAnimationChange(bool canPush ,bool ground, bool isClimb, bool isPushAction)
    {
        // Object���ړ�����
        if (canPush && ground && !isClimb)
        {
            if (isPushAction)
            {
                isPush = true;
                anim.SetBool("IsObjectMove", true);
            }
            else
            {
                anim.SetBool("IsObjectMove", false);
                isPush = false;
            }
        }
        else
        {
            anim.SetBool("IsObjectMove", false);
            isPush = false;
        }
    }
}
