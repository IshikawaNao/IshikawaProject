using UnityEngine;

/// <summary>
/// �S�[������
/// </summary>
public class GoalCheck : MonoBehaviour
{
    [Header("�X�e�[�W�}�l�[�W���[")]
    StageManager sm;
    private void Start()
    {
        sm = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            sm.Goal = true;
        }
    }
}
