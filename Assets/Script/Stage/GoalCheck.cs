using UnityEngine;

/// <summary>
/// ƒS[ƒ‹ˆ—
/// </summary>
public class GoalCheck : MonoBehaviour
{
    StageManager sm;

    void Start()
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
