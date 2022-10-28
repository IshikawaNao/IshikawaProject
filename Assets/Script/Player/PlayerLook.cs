using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    KeyInput input;

    private void Start()
    {
        input = player.GetComponent<KeyInput>();
    }

    void Update()
    {
        //�}�E�X�̈ړ���
        float mouseInputX = input.CameraPos.x;

        // target�̈ʒu��Y���𒆐S�ɁA��]�i���]�j����
        transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, mouseInputX * Time.deltaTime * 100f, transform.rotation.z));
        this.transform.position = player.transform.position;
    }
}
