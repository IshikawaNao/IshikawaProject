using SoundSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PausePresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private Image[] selectButton;
    [SerializeField,Header("CanvasGroup")]
    CanvasGroup canvasGroup;

    private PauseView view;
    private PauseModel model;

    private KeyInput input;

    IUIView uiView;

    private ReactiveProperty<int> stateNum = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> StateObservable { get { return stateNum; } }
    ReactiveProperty<bool> escInputValue = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> EscInputValue { get { return escInputValue; } }
    ReactiveProperty<bool> decisionInputSurveillance = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> DecisionInputSurveillance { get { return decisionInputSurveillance; } }

    private void Start()
    {
        view = new PauseView(canvasGroup);
        model = new PauseModel(selectButton.Length - 1);
        input = KeyInput.Instance;
        view.UIMove(selectButton[0]);

        // UniRx��Subscribe���g���ă��[�U�[�̃A�N�V�������Ď�
        EscInputValue
            .Where(value => input.EscInput)
            .Subscribe(value => Back())
            .AddTo(this);

        DecisionInputSurveillance
            .Where(value => (pausePanel.activeSelf && pauseButton.activeSelf) && input.Inputdetection)
            .Subscribe(value => DecisionPush())
            .AddTo(this);
    }

    private void Update()
    {
        UISelect();
        view.UIMove(selectButton[model.Num]);
        escInputValue.Value = input.EscInput;
        decisionInputSurveillance.Value = input.DecisionInput;
    }

    private void UISelect()
    {
        if (pausePanel.activeSelf && pauseButton.activeSelf)
        {
            var value = input.InputMove;
            if (value != Vector2.zero && model.IsSelect.Value)
            {
                var num = model.Num;
                if (num == model.SelectNum(value)) { return; }
                view.UIExit();
            }
        }
    }

    private void DecisionPush()
    {
        // ���ʉ��̍Đ���UI�̏I������
        SoundManager.Instance.PlayOneShotSe((int)SEList.Decision);
        view.UIExit();
        model.Decision();
        stateNum.Value = (int)model.CurrentState;
        // ��Ԃɉ���������
        switch (model.CurrentState)
        {
            case PauseUIState.Option:
                StateChange();
                break;
            case PauseUIState.MainScene:
                FadeManager.Instance.LoadScene("Main", 1.5f);
                break;
            case PauseUIState.TitleScene:
                FadeManager.Instance.LoadScene("Title", 1.5f);
                break;
                // Add more cases as needed
        }
    }

    // �I���X�e�[�g�ύX
    private async void StateChange()
    {
        // �A�j���[�V�������I���܂ŏ������~�߂�
        await view.DisableAnimation(pauseButton);
        // �I�����ꂽ�X�e�[�g��\������
        optionPanel.SetActive(true);
        // �I�����ꂽObject�̃A�j���[�V������View���擾
        uiView = optionPanel.GetComponent<IUIView>();
    }
    // �^�C�g���Z���N�g�֖߂�
    private async void Back()
    {
        if(pauseButton.activeSelf)
        {
            await view.DisableAnimation(pausePanel);
            pauseButton.SetActive(false);
        }
        else if(optionPanel.activeSelf)
        {
            // �I������Ă����X�e�[�g�̃A�j���[�V����
            await uiView.DisableAnimation();
            // �^�C�g����\������
            pauseButton.SetActive(true);
            view.EnabldUIAnimation();
        }
        else
        {
            pausePanel.SetActive(true);
            pauseButton.SetActive(true);
            view.EnabldUIAnimation();
        }

    }

    private void OnDestroy()
    {
        // PauseView��Disposable�̉��
        view.Dispose();
    }
}
