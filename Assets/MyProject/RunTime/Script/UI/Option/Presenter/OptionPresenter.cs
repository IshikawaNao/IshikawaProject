using UnityEngine;
using UnityEngine.UI;

public class OptionPresenter : MonoBehaviour
{
    [SerializeField,Header("�X�N���[���}�l�[�W���[")]
    ScreenSizeSet screenSize;
    [SerializeField, Header("�r���[�p�l��")]
    GameObject[] viewPanel;

    [SerializeField, Header("Aoudio�{�^��")]
    Image[] AoudioButton;

    [SerializeField, Header("System�{�^��")]
    Image[] SystemButton;

    KeyInput input;

    // View
    OptionView optionView = new OptionView();

    OptionModel optionModel;

    public Vector2 SelectNum { get { return optionModel.SelectValue; } }

    private void Start()
    {
        input = KeyInput.Instance;
        // �I�v�V�����X�e�[�g���Ƃ̍ő吔
        var num = new int[] { AoudioButton.Length - 1, SystemButton.Length - 1 };
        optionModel = new OptionModel(viewPanel.Length - 1, num);

        optionView.ChangeState(0, viewPanel);
        optionView.UIMove(AoudioButton[0]);
    }

    void Update()
    {
        StateChange();
        Decision();
    }

    // �X�e�[�g���؂�ւ�
    private void StateChange()
    {
        var value = input.InputMove;
       
        if(optionModel.IsSelect && value != Vector2.zero)
        {
            var num = optionModel.SelectValue;
            optionModel.SelectCahge(value);
            // �ύX�O�ƕω����Ȃ������ꍇ�Ԃ�
            if (num == optionModel.SelectValue)
            {
                return;
            }
            // �I�𒆂̃{�^����������
            optionView.UIExit();
            // �X�e�[�g��ύX
            switch (optionModel.SelectValue.x)
            {
                case 0:
                    //optionModels = aoudioModel;
                    optionView.UIMove(AoudioButton[(int)optionModel.SelectValue.y]);
                    break;
                case 1:
                    optionView.UIMove(SystemButton[(int)optionModel.SelectValue.y]);
                    break;

            }
            optionView.ChangeState((int)optionModel.SelectValue.x, viewPanel);
        }
    }

    private void Decision()
    {
        if(input.DecisionInput && optionModel.SelectValue == new Vector2(1,1))
        {
            // �X�N���[���T�C�Y��؂�ւ�
            screenSize.SetScreen();
        }
    }

    private void OnDisable()
    {
        optionModel.ReturnOption();
        optionView.UIExit();
        optionView.ChangeState(0, viewPanel);
        optionView.UIMove(AoudioButton[0]);
        SaveDataManager.Instance.Save();
    }
}
