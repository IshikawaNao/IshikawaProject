using UnityEngine;

/// <summary>
/// ÉJÉÅÉâÇÃà⁄ìÆ
/// </summary>
public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    GameObject mainCM;

    [SerializeField]
    GameObject player;

    KeyInput input;
    Vector3 target = new Vector3(0,1,0);

    float sensitivity = 0.1f;

    float verticalValue = -30;

    // ÉJÉÅÉâäpìx
    const float verticalMaxValue = 10f;
    const float verticalMinValue = -40f;
    const float RestVerticalValue = -15f;

    void Start()
    {
        mainCM.transform.localPosition = new Vector3(0, 1, 10);
        input = KeyInput.Instance;
    }

    void Update()
    {
        this.transform.position = player.transform.position;
        Pcm();
        ResetCamera();
    }

    void Pcm()
    {
        float horizontalInput = input.CameraPos.x;
        float verticalInput = input.CameraPos.y;

        Vector3 rot = transform.localRotation.eulerAngles;
        float horizontalValue = rot.y + horizontalInput * sensitivity;

        verticalValue  -= verticalInput * sensitivity;
        verticalValue = Mathf.Clamp(verticalValue, verticalMinValue, verticalMaxValue);

        transform.localRotation = Quaternion.Euler(verticalValue, horizontalValue, 0);
    }

    void ResetCamera()
    {
        if (input.CameraReset)
        {
            verticalValue = RestVerticalValue;
            this.transform.localRotation = Quaternion.LookRotation(-player.transform.forward);
        }
    }
}
