using UnityEngine;

/// <summary>
/// �}�E�X����o��Ray�ƃI�u�W�F�N�g�̏Փˍ��W��Shader�ɓn��
/// �K���ȃI�u�W�F�N�g�ɃA�^�b�`
/// </summary>
public class CollisionRipple : MonoBehaviour
{
    /// <summary>
    /// �|�C���^�[���o�������I�u�W�F�N�g�̃����_���[
    /// �O��FShader�͍��W�󂯎��ɑΉ��������̂�K�p
    /// </summary>
    [SerializeField] private Renderer _renderer;

    /// <summary>
    /// Shader���Œ�`�ς݂̍��W���󂯎��ϐ�
    /// </summary>
    private string propName = "_PlayerPosition";

    private Material mat;

    void Start()
    {
        mat = _renderer.material;
    }

    void Update()
    {

            //Ray�o��
            Ray ray =new Ray (this.gameObject.transform.position, Vector3.down);
            RaycastHit hit_info = new RaycastHit();
            float max_distance = 100f;

            bool is_hit = Physics.Raycast(ray, out hit_info, 1f);

            //Ray�ƃI�u�W�F�N�g���Փ˂����Ƃ��̏���������
            if (is_hit)
            {
                //�Փ�
                Debug.Log(hit_info.point);
                //Shader�ɍ��W��n��
                mat.SetVector(propName, hit_info.point);
            }
        
    }
}