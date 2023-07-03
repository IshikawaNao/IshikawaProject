using UnityEngine;
using UnityEngine.UI;

public class OptionPresenter : MonoBehaviour
{
    [SerializeField,Header("スクリーンマネージャー")]
    ScreenSizeSet screenSize;
    [SerializeField, Header("ビューパネル")]
    GameObject[] viewPanel;

    [SerializeField, Header("Aoudioボタン")]
    Image[] AoudioButton;

    [SerializeField, Header("Systemボタン")]
    Image[] SystemButton;

    KeyInput input;

    // View
    OptionView optionView = new OptionView();

    OptionModel optionModel;

    public Vector2 SelectNum { get { return optionModel.SelectValue; } }

    private void Start()
    {
        input = KeyInput.Instance;
        // オプションステートごとの最大数
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

    // ステートが切り替り
    private void StateChange()
    {
        var value = input.InputMove;
       
        if(optionModel.IsSelect && value != Vector2.zero)
        {
            var num = optionModel.SelectValue;
            optionModel.SelectCahge(value);
            // 変更前と変化がなかった場合返す
            if (num == optionModel.SelectValue)
            {
                return;
            }
            // 選択中のボタンを初期化
            optionView.UIExit();
            // ステートを変更
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
            // スクリーンサイズを切り替え
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
