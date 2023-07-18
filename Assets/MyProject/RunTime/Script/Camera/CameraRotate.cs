using UnityEngine;

/// <summary>
/// カメラの回転
/// </summary>
public class CameraRotate 
{ 
    // カメラ角度
    float verticalValue = -30;
    const float verticalMaxValue = 50f;
    const float verticalMinValue = -80f;

    /// <summary> カメラの回転 </summary>
   　public Quaternion RotateCameraBy(Vector2 _cameraInput, GameObject _controller, float _sensitivity)
    {
        // 入力
        float horizontalInput = _cameraInput.x;
        float verticalInput = _cameraInput.y;

        Vector3 rot = _controller.transform.localRotation.eulerAngles;
        float horizontalValue = rot.y + horizontalInput * _sensitivity;

        verticalValue -= verticalInput * _sensitivity;
        // カメラの回転が上限を超えないようにする
        verticalValue = Mathf.Clamp(verticalValue, verticalMinValue, verticalMaxValue);

        var cameraRot = Quaternion.Euler(verticalValue, horizontalValue, 0);

        return cameraRot;
    }

    /// <summary> プレイヤーの背面へカメラを移動させる</summary>
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
