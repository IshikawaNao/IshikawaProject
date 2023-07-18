using UnityEngine;

/// <summary>
/// �J�����̈ړ�
/// </summary>
public class CameraMove 
{
    const float playerVisibilityCheck = 2;
    const float playerAlpha = 0.5f;

    /// <summary>�@�J�����֌�������Ray���΂� </summary>
    public void CameraForwardMove(GameObject cameraParent, GameObject target, LayerMask wall_layerMask, Camera main)
    {
        // �e�I�u�W�F�N�g�̈ʒu���擾
        Vector3 orgin = cameraParent.transform.position;
        // �J�����̈ړ��ʒu
        var cameraPos = target.transform.position;

        RaycastHit hit;
        // Raycast���ǂɂ������������J�����̍��W��Raycast�������������W�ɂ���
        if (Physics.Raycast(orgin, cameraPos - orgin, out hit, Vector3.Distance(orgin, cameraPos), wall_layerMask, QueryTriggerInteraction.Ignore))
        {
            cameraPos = hit.point;
        }
        // Local���W�ɕϊ�����
        cameraPos = cameraParent.transform.InverseTransformPoint(cameraPos);
        main.transform.localPosition = cameraPos;
    }

    /// <summary> �J�������v���C���[�ɋ߂Â������v���C���[�𔼓����ɂ��� </summary>
    public void SetPlayerAlpha(GameObject cameraParent,Material mat,Camera main)
    {
        var dis = Vector3.Distance(cameraParent.transform.position, main.transform.position);
        if(dis < playerVisibilityCheck) 
        {
            mat.SetFloat("_Alpha", playerAlpha); 
        }
        else
        { 
            mat.SetFloat("_Alpha", 1); 
        }
    }
}
