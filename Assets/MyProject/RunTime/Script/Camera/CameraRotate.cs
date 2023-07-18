using UnityEngine;

/// <summary>
/// �J�����̉�]
/// </summary>
public class CameraRotate 
{ 
    // �J�����p�x
    float verticalValue = -30;
    const float verticalMaxValue = 50f;
    const float verticalMinValue = -80f;

    /// <summary> �J�����̉�] </summary>
   �@public Quaternion RotateCameraBy(Vector2 _cameraInput, GameObject _controller, float _sensitivity)
    {
        // ����
        float horizontalInput = _cameraInput.x;
        float verticalInput = _cameraInput.y;

        Vector3 rot = _controller.transform.localRotation.eulerAngles;
        float horizontalValue = rot.y + horizontalInput * _sensitivity;

        verticalValue -= verticalInput * _sensitivity;
        // �J�����̉�]������𒴂��Ȃ��悤�ɂ���
        verticalValue = Mathf.Clamp(verticalValue, verticalMinValue, verticalMaxValue);

        var cameraRot = Quaternion.Euler(verticalValue, horizontalValue, 0);

        return cameraRot;
    }

    /// <summary> �v���C���[�̔w�ʂփJ�������ړ�������</summary>
    public void GimmickCP(GameObject _controller, GameObject _lookon)
    {
        Vector3 rot = new Vector3(-_lookon.transform.forward.x, -_lookon.transform.forward.y, -_lookon.transform.forward.z);
        verticalValue = rot.x;
        _controller.transform.localRotation = Quaternion.LookRotation(rot);
    }

    public void DropCamera()
    {
        verticalValue = verticalMinValue;
    }
}
