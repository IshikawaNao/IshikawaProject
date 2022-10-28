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
        //マウスの移動量
        float mouseInputX = input.CameraPos.x;

        // targetの位置のY軸を中心に、回転（公転）する
        transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, mouseInputX * Time.deltaTime * 100f, transform.rotation.z));
        this.transform.position = player.transform.position;
    }
}
