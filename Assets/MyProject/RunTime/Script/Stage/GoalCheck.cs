using UnityEngine;

/// <summary>
/// ゴール処理
/// </summary>
public class GoalCheck : MonoBehaviour
{
    // ゴールフラグ
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
