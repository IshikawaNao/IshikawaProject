using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject playerObj;
    Vector3 targetPos;

    InputKey input;
    void Start()
    {
        playerObj = GameObject.Find("Player");
        input = playerObj.GetComponent<InputKey>();
        targetPos = playerObj.transform.position;
    }

    void Update()
    {
        // player�̈ړ��ʕ��A�J�������ړ�
        transform.position += playerObj.transform.position - targetPos;
        targetPos = playerObj.transform.position;

        //�}�E�X�̈ړ���
        float mouseInputX = input.Camera.x;

        // target�̈ʒu��Y���𒆐S�ɁA��]�i���]�j����
        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);
    }
}
