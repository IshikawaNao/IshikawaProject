using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// �I�u�W�F�N�g������
/// </summary>
public class PushObject 
{
    // �v����
    const float speed = 2.95f;

    const float rayLength = 1f;
    bool isPush = false;        // �I�u�W�F�N�g�������t���O
    public bool IsPush { get { return isPush; } }

    Vector3 checkVec = new(1, 0, 1);
    
    RaycastHit hit;
    Ray ray;

    // Push�I�u�W�F�N�g
    GameObject pushObj = null;
    public GameObject PushObj { get {return pushObj; } }
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

    // �v�b�V���I�u�W�F�N�g�ړ�
    public void Move(Vector2 move) 
    {
        if(move != Vector2.zero && isPush)
        {
            Vector3 playerForward = Vector3.Scale(player.transform.forward, checkVec);
            Vector3 moveForward = playerForward * move.y + player.transform.right * move.x;
            Vector3 moveVector = moveForward.normalized * speed;   //�ړ����x
            rb.velocity = moveVector;
        }
    }


    // player�̐��ʂɓ�������I�u�W�F�N�g�����邩
    public bool CanPush()
    {
        Vector3 orgin = player.transform.position;
        Vector3 direction = Vector3.Scale(player.transform.forward, checkVec);
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // ��������I�u�W�F�N�g�̏ꍇ���̃I�u�W�F�N�g�̏����擾
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                pushObj = hit.collider.gameObject;
                rb = hit.rigidbody;
                return true;
            }
        }
        pushObj = null;
        rb = null;
        return false;
    }

    // �v�b�V�����I�����ꂽ���̏���
    public void PushAnimationChange(bool ground, bool isClimb, bool isPushAction)
    {
        if (CanPush() && ground && !isClimb)
        {
            if (isPushAction)
            {
                isPush = true;
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
