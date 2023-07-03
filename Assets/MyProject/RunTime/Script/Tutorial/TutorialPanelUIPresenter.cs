using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

//[ExecuteInEditMode]
public class TutorialPanelUIPresenter : MonoBehaviour
{
    [SerializeField, Header("チュートリアルイメージデータ")]
    TutorialData tutorialData;
    [SerializeField, Header("チュートリアルパネル")]
    Image image;
    [SerializeField, Header("確認UIの親")]
    GameObject selectPanel;
    [SerializeField, Header("確認UIPrefab")]
    GameObject confirmationUI;
    [SerializeField, Header("説明文")]
    TextMeshProUGUI tutorialText;

    // UIView
    TutorialUIView ui_View;
    // UIModel
    TutorialUModel ui_Model;
    // 入力
    KeyInput input;
    // Imageのリスト
    List<Image> list_confirmationUI = new List<Image>();

    private void Awake()
    {
        // 生成した確認UIがLIst以上の場合処理を返す
        if(selectPanel.transform.childCount >= tutorialData.Data.Count)
        {
            // Listに子オブジェクトを格納
            for(int i = 0; i < selectPanel.transform.childCount; i++)
            {
                var child = selectPanel.transform.GetChild(i).gameObject;
                var img = child.GetComponent<Image>();
                // 生成した確認UIをリストに格納
                list_confirmationUI.Add(img);
            }
            return;
        }

        // Listの要素数分確認UIを生成
        foreach (var Item in tutorialData.Data)
        {
            var instance = Instantiate(confirmationUI, this.transform.position, this.transform.localRotation, selectPanel.transform);
            var img = instance.GetComponent<Image>();
            // 生成した確認UIイメージをリストに格納
            list_confirmationUI.Add(img);
        }
    }

    private void Start()
    {
        input = KeyInput.Instance;
        ui_View = new TutorialUIView();
        ui_Model = new TutorialUModel();
        ui_View.PanelSelect(ui_Model.Num, list_confirmationUI);
        image.sprite = tutorialData.Data[ui_Model.Num].sprite;
        tutorialText.text = tutorialData.Data[ui_Model.Num].explanatory;
    }

    void Update()
    {
        SelectNumberChange();
    }

    // 入力があった場合ページを変更
    private void SelectNumberChange()
    {
        var value = input.InputMove.x;
        if (value != 0 && ui_Model.IsSelect)
        {
            var num = ui_Model.Num;
            // 選択制御
            ui_Model.SelectNum(value, list_confirmationUI.Count - 1);
            // 変更前と変化がなかった場合返す
            if (num == ui_Model.Num)
            {
                return;
            }
            // 画面に反映
            ui_View.PanelSelect(ui_Model.Num, list_confirmationUI);
            image.sprite = tutorialData.Data[ui_Model.Num].sprite;
            tutorialText.text = tutorialData.Data[ui_Model.Num].explanatory;
        }
    }
}
