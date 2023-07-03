using UnityEngine;
using UnityEngine.UI;
using SoundSystem;
using UniRx;
using System;

public class TitleUIPresenter : MonoBehaviour
{
    [SerializeField, Header("各ステートオブジェクト")]
    GameObject[] StateObject;
    [SerializeField, Header("タイトルセレクトボタン")]
    Image[] TitleButton;
    KeyInput input;
    // View
    TitleView view = new TitleView();
    // Model
    TitleUIModel titleUIModel;

    int modelNum = 0;
    public int ModelNum { get { return modelNum; } }
    // ステートの切り替えを検知
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

    // 入力があった場合、選択を変更
    private void SelectNumberChange()
    {
        var value = input.InputMove;
        if (value != Vector2.zero && titleUIModel.IsSelect)
        {
            var num = titleUIModel.Num;
            modelNum = titleUIModel.SelectNum(value);
            // 変更前と変化がなかった場合返す
            if (num == modelNum)
            {
                return;
            }
            view.UIExit();
        }
    }
    // 選択されているステートのUIアニメーションを再生
    private void UiAnimation()
    {
        view.UIMove(TitleButton[titleUIModel.Num]);
    }
    // 選択時の処理
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
