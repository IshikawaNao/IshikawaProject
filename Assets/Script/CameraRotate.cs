using UnityEngine;
using System.Collections;
using Cinemachine;

/// <summary>
/// ÉJÉÅÉâÇÃà⁄ìÆ
/// </summary>
public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    GameObject mainCM;
    GameObject playerPos;
    KeyInput input;

    Vector3 target = new Vector3(0,1,0);

    float sensitivity = 0.1f;

    float verticalValue = -30;
    public float VerticalVal { get { return verticalValue; }}

    void Start()
    {
        playerPos = GameObject.Find("Player");
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
        mainCM.transform.localPosition = new Vector3(0, 1, 8);
    }

    void Update()
    {
        this.transform.position = playerPos.transform.position;
        Pcm();
       // CmPos();
    }

    void CmPos()
    {
        mainCM.transform.localPosition = Vector3.MoveTowards(mainCM.transform.localPosition, target,0.1f);
    }
   
    void Pcm()
    {
        float horizontalInput = input.CameraPos.x;
        float verticalInput = input.CameraPos.y;

        Vector3 rot = transform.localRotation.eulerAngles;
        float horizontalValue = rot.y + horizontalInput * sensitivity;

        verticalValue  -= verticalInput * sensitivity;
        verticalValue = Mathf.Clamp(verticalValue, -50f, 10f);

        transform.localRotation = Quaternion.Euler(verticalValue, horizontalValue, 0);
    }
}
