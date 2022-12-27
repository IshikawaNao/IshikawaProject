using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    KeyInput input;

    void Update()
    {
        //�}�E�X�̈ړ���
        float mouseInputX = input.CameraPos.x;

        // target�̈ʒu��Y���𒆐S�ɁA��]�i���]�j����
        //transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, mouseInputX * Time.deltaTime * 100f, transform.rotation.z));
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
    }
}
