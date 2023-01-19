using UnityEngine;

/// <summary>
/// ゴール処理
/// </summary>
public class GoalCheck : MonoBehaviour
{
    [Header("ステージマネージャー")]
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
