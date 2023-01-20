using UnityEngine;

public class StageNumberSelect : MonoBehaviour
{
    private static StageNumberSelect instance;
    public static StageNumberSelect Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (StageNumberSelect)FindObjectOfType(typeof(StageNumberSelect));

                if (instance == null)
                {
                    Debug.LogError(typeof(StageNumberSelect) + "is nothing");
                }
            }
            return instance;
        }
    }

    // ステージナンバー
    public int StageNumber { get; set; } = 0;

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
