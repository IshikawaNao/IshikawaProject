using Unity.VisualScripting;
using UnityEngine;

/// <summary> RayHit�m�F </summary>
public class RayHitDetection : MonoBehaviour 
{
    // �v���C���[
    [SerializeField, Header("Player")]
    GameObject player;

    PlayerTeleporter teleporter;
    BeamIaunch beamIaunch;

    Vector3 checkVec = new(1, 0, 1);

    // �ڒn����p��ray�̒���
    float distance = 0.2f;

    // �ړ�����pray�̒���
    const float RayLength = 1f;

    // �o�蔻��pray�̋���,����
    const float WallCheckOffset = 0.85f;
    const float UpperWallCheckOffset = 5f;
    const float WallCheckDistance = 1f;

    // ray�̋���,����
    const float fallCheckDistance = 0.7f;
    // �r�[���I�u�W�F�N�g�̃��C���[
    const int BeamLayer = 10;

    Rigidbody rb = null;
    public Rigidbody PushRb { get { return rb; } }

    bool isForwardWall;
    public bool IsForwardWall { get { return isForwardWall; } }
    bool isUpperWall;
    public bool IsUpperWall { get { return isUpperWall; } }
    bool isBeamRotate;
    public bool IsBeamRotate { get { return isBeamRotate; } }

    /// <summary> �ڒn���Ă��邩 </summary>
    public bool IsGround()
    {
        Vector3 rayPosition = player.transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red, 1f);
        return Physics.Raycast(ray, distance);
    }

    /// <summary> �e���|�[�g�o���邩 </summary>
    public bool IsTeleport()
    {
        Vector3 rayPosition = player.transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, distance))
        {
            if (hit.collider.gameObject.tag.Contains("Teleport"))
            {
                teleporter = hit.collider.gameObject.GetComponent<PlayerTeleporter>();
                return teleporter.Activated;
            }
        }
        return false;
    }

    /// <summary> �e���|�[�g���s </summary>
    public void Teleport(){ if(teleporter != null) { teleporter.Teleport(player); } }

    /// <summary> player�̐��ʂɓ�������I�u�W�F�N�g�����邩 </summary>
    public bool CanPush()
    {
        Vector3 orgin = player.transform.position;
        Vector3 direction = Vector3.Scale(player.transform.forward, checkVec);
        var ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, RayLength))
        {
            //�@�r�[���I�u�W�F�N�g���m
            if(hit.collider.gameObject.layer == BeamLayer)
            {
                beamIaunch = hit.collider.gameObject.GetComponent<BeamIaunch>();
                isBeamRotate = true;
            }
            // ��������I�u�W�F�N�g�̏ꍇ���̃I�u�W�F�N�g�̏����擾
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                rb = hit.rigidbody;
                return true;
            }
        }
        isBeamRotate = false;
        rb = null;
        return false;
    }

    /// <summary> ��]���s </summary>
    public void BeamRotate() { if (beamIaunch != null) {�@beamIaunch.BeamHeadRotate(); } }

    /// <summary> player�̐��ʂɓo��鍂���̕ǂ����邩 </summary>
    public bool ClimbCheck()
    {
        //  �ǔ���Ɏg�p����ϐ�
        Ray wallCheckRay = new Ray(player.transform.position + Vector3.up * WallCheckOffset, player.transform.forward);
        Ray upperCheckRay = new Ray(player.transform.position + Vector3.up * UpperWallCheckOffset, player.transform.forward);

        //  �ǔ�����i�[
        isForwardWall = Physics.Raycast(wallCheckRay, WallCheckDistance);
        isUpperWall = Physics.Raycast(upperCheckRay, WallCheckDistance);

        //Debug.DrawRay(player.transform.position + Vector3.up * wallCheckOffset, player.transform.forward, Color.red, 3);
        //Debug.DrawRay(player.transform.position + Vector3.up * upperWallCheckOffset, player.transform.forward, Color.red, 3);

        // �O���ɕǂ���㕔�ɕǂ��Ȃ���True
        if (isForwardWall && !isUpperWall) {  return true; }
        return false;
    }

    /// <summary> ���������ۂ� </summary>
    public bool IsFall()
    {
        //  ��������Ɏg�p����ϐ�
        Ray fallCheckRay = new Ray(player.transform.position, Vector3.down);
        return Physics.Raycast(fallCheckRay, fallCheckDistance);
    }
}
