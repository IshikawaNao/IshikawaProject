using UnityEngine;

public class StageNumberSelect : MonoBehaviour
{
    public static StageNumberSelect Instance { get; private set; }

    // �X�e�[�W�i���o�[
    public int StageNumber { get; set; } = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }
    }
}
