using UniRx;
using UnityEngine;

public class SwithingOperationUI : MonoBehaviour
{
    [SerializeField, Header("�L�[�{�[�h�\��")]
    GameObject[] keyOperation;
    [SerializeField, Header("Pad�\��")]
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

    // ���������Pad�ƃL�[�{�[�h�̕\����؂�ւ���
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
