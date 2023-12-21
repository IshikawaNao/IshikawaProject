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

    // �X�e�[�W�i���o�[
    private int stageNumber;
    public int StageNumber { get { return stageNumber;} }

    // �X�e�[�W�����肵���Ƃ��ɌĂ΂��
    public void SelectStage(int _stageNumber)
    {
        stageNumber = _stageNumber;
    }

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
