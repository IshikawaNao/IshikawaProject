using UnityEngine;

/// <summary>
/// �S�[������
/// </summary>
public class GoalCheck : MonoBehaviour
{
    [SerializeField,Header("�X�e�[�W�}�l�[�W���[")]
    StageManager sm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            sm.Goal = true;
        }
    }
}
