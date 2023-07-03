using UnityEngine;
using UnityEngine.UI;
using SoundSystem;
using UniRx;
using System;

public class TitleUIPresenter : MonoBehaviour
{
    [SerializeField, Header("�e�X�e�[�g�I�u�W�F�N�g")]
    GameObject[] StateObject;
    [SerializeField, Header("�^�C�g���Z���N�g�{�^��")]
    Image[] TitleButton;
    KeyInput input;
    // View
    TitleView view = new TitleView();
    // Model
    TitleUIModel titleUIModel;

    int modelNum = 0;
    public int ModelNum { get { return modelNum; } }
    // �X�e�[�g�̐؂�ւ������m
    ReactiveProperty<int> stateNum = new ReactiveProperty<int>(0);
    public IObservable<int> StateObservable { get { return stateNum; } }

    bool isTitleUI = true;
    public bool IsTitleUI { get { return isTitleUI; } }

    private void Start()
    {
        input = KeyInput.Instance;
        titleUIModel = new TitleUIModel(TitleButton.Length - 1);
        view.UIMove(TitleButton[0]);
    }

    private void Update()
    {
        SelectNumberChange();
        UiAnimation();
        Decision();
    }

    // ���͂��������ꍇ�A�I����ύX
    private void SelectNumberChange()
    {
        var value = input.InputMove;
        if (value != Vector2.zero && titleUIModel.IsSelect)
        {
            var num = titleUIModel.Num;
            modelNum = titleUIModel.SelectNum(value);
            // �ύX�O�ƕω����Ȃ������ꍇ�Ԃ�
            if (num == modelNum)
            {
                return;
            }
            view.UIExit();
        }
    }
    // �I������Ă���X�e�[�g��UI�A�j���[�V�������Đ�
    private void UiAnimation()
    {
        view.UIMove(TitleButton[titleUIModel.Num]);
    }
    // �I�����̏���
    private void Decision()
    {
        if (input.DecisionInput)
        {
            SoundManager.Instance.PlayOneShotSe("decision");
            view.UIExit();
            stateNum.Value = titleUIModel.StateNum(titleUIModel.Num);
        }
    }

    private void OnEnable()
    {
        stateNum.Value = 0;
    }
}
