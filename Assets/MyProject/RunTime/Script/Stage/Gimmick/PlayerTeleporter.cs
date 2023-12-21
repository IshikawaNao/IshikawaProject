using UnityEngine;
using DG.Tweening;
using UniRx;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField, Header("�e���|�[�g�G�t�F�N�g")]
    GameObject effect;
    [SerializeField, Header("�e���|�[�g��̃e���|�[�^�[")] 
    GameObject teleporter;
    [SerializeField, Header("�I�u�W�F�N�g�̋N���m�F")]
    SonarActivationGimmick sonarActivation;

    const float DelayTime = 1.5f;

    // �e���|�[�g�o���邩�ۂ�
    bool activated = false;
    public bool Activated { get { return activated; } }

    private void Start()
    {
        // �N���I�u�W�F�N�g�̋N�����Ď�
        sonarActivation.Activated
            .Where(x => x)
            .Subscribe(x =>
            {
                activated = x;
                effect.SetActive(true);
            }).AddTo(this);
    }

    public void Teleport(GameObject obj)
    {
        DOVirtual.DelayedCall(DelayTime, () => obj.transform.position = teleporter.transform.position);
    }
}
