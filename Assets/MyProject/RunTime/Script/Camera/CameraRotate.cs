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

    [SerializeField]
    StageManager sm;

    CameraMove cm;
    PlayerController pc;

    KeyInput input;
    CreateData data;

    float sensitivity = 0.1f;

    float verticalValue = -30;

    // ÉJÉÅÉâäpìx
    const float verticalMaxValue = 10f;
    const float verticalMinValue = -40f;


    void Start()
    {
        mainCM.transform.localPosition = new Vector3(0, 1, 10);
        cm = mainCM.GetComponent<CameraMove>();
        input = KeyInput.Instance;
        data = CreateData.Instance;
        pc = player.GetComponent<PlayerController>();
        data.LoadSensitivity(ref sensitivity);
    }

    void Update()
    {
        this.transform.position = player.transform.position;
        SwitchingCP();
        ResetCamera();
    }


    void SwitchingCP()
    {
        if(pc.Push || sm.IsStart) { GimmickCP(); }
        else {  Pcm(); }
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
            GimmickCP();
        }
    }


    void GimmickCP()
    {
        Vector3 rot = new Vector3 (-player.transform.forward.x, -player.transform.forward.y, -player.transform.forward.z);
        verticalValue = rot.x;
        this.transform.localRotation = Quaternion.LookRotation( rot);
        cm.ResetCamera();
    }
}
