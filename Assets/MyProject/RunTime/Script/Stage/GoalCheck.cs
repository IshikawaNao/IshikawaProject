using UnityEngine;

/// <summary>
/// ゴール処理
/// </summary>
public class GoalCheck : MonoBehaviour
{
    [SerializeField,Header("ステージマネージャー")]
    StageManager sm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            sm.Goal = true;
        }
    }
}
