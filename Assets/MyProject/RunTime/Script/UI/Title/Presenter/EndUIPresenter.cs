using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EndUIPresenter : MonoBehaviour
{
    [SerializeField, Header("タイトルパネル")]
    GameObject title;
    [SerializeField, Header("終了ボタン")]
    Image[] endButton;
    EndUIModel endUIModel = new EndUIModel();
    EndUIView endUIView = new EndUIView();
    KeyInput input;
    private void Start()
    {
        input = KeyInput.Instance;
    }

    private void Update()
    {
        endUIView.UIMove(endButton[endUIModel.Num]);
        QuitSelect();
        endUIModel.QuitDecision(input.DecisionInput, title, this.gameObject);
    }

    private void QuitSelect()
    {
        var value = input.InputMove;
        if (value != Vector2.zero && endUIModel.IsSelect)
        {
            var num = endUIModel.Num;
            endUIModel.EndSelectNum(value);
            // 変更前と変化がなかった場合返す
            if (num == endUIModel.Num)
            {
                return;
            }
            endUIView.UIExit();
        }
    }

}
