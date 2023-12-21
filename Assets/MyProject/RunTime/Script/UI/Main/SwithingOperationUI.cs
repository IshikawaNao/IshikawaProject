using UniRx;
using UnityEngine;

public class SwithingOperationUI : MonoBehaviour
{
    [SerializeField, Header("キーボード表示")]
    GameObject[] keyOperation;
    [SerializeField, Header("Pad表示")]
    GameObject[] padOperation;

    KeyInput input;

    ReactiveProperty<bool> gamepadInputdetection = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> GamepadInputdetection { get { return gamepadInputdetection; } }

    private void Start()
    {
        input = KeyInput.Instance;
        SwithingKeyOperation();

        gamepadInputdetection
            .Where(value => input.Inputdetection)
            .Subscribe(value => SwithingKeyOperation())
            .AddTo(this);
        gamepadInputdetection
            .Where(value => !input.Inputdetection)
            .Subscribe(value => SwithingPadOperation())
            .AddTo(this);
    }

    private void Update()
    {
        gamepadInputdetection.Value = input.Inputdetection;
    }

    // 操作説明のPadとキーボードの表示を切り替える
    void SwithingKeyOperation()
    {
        for (int i = 0; i <= keyOperation.Length - 1; i++)
        {
            keyOperation[i].SetActive(true);
            padOperation[i].SetActive(false);
        }
    }
    void SwithingPadOperation()
    {
        for (int i = 0; i <= keyOperation.Length - 1; i++)
        {
            keyOperation[i].SetActive(false);
            padOperation[i].SetActive(true);
        }
    }
}
