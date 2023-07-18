using UnityEngine;

/// <summary>
/// �S�[������
/// </summary>
public class GoalCheck : MonoBehaviour
{
    // �S�[���t���O
    bool isGoal = false;
    public bool Goal { get { return isGoal; } }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            isGoal = true;
        }
    }
}
