using UnityEngine;

public class OperationExplanationMove : MonoBehaviour
{
    [SerializeField,Header("�v���C���[�R���g���[���[")]
    RayHitDetection rayHitDetection;
    [SerializeField, Header("PushImage")]
    GameObject push;
    [SerializeField, Header("ClimbImage")]
    GameObject climb;
    [SerializeField, Header("Teleport�R���g���[���[")]
    GameObject teleport;
    [SerializeField, Header("BeamRotateImage")]
    GameObject beamRotate;

    public void Update()
    {
        ClimbCheck(rayHitDetection.ClimbCheck());
        PushCheck(rayHitDetection.CanPush());
        TeleportCheck(rayHitDetection.IsTeleport());
        BeamRotatetCheck(rayHitDetection.IsBeamRotate);
    }

    void ClimbCheck(bool check)
    {
        climb.SetActive(check);
    }

    void PushCheck(bool check)
    {
        push.SetActive(check);
    }

    void TeleportCheck(bool check)
    {
        teleport.SetActive(check);
    }

    void BeamRotatetCheck(bool check)
    {
        beamRotate.SetActive(check);
    }
}
