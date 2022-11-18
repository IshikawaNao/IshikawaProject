using UnityEngine;

/// <summary>
/// �J�����̈ړ�
/// </summary>
public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj;
    Vector3 targetPos;

    KeyInput input;
    void Start()
    {
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
        targetPos = playerObj.transform.position;
    }

    void Update()
    {
        // player�̈ړ��ʕ��A�J�������ړ�
        transform.position += playerObj.transform.position - targetPos;
        targetPos = playerObj.transform.position;

        //�}�E�X�̈ړ���
        float mouseInputX = input.CameraPos.x;

        // target�̈ʒu��Y���𒆐S�ɁA��]�i���]�j����
        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);
    }
}
