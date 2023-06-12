using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

//[ExecuteInEditMode]
public class TutorialPanelUIPresenter : MonoBehaviour
{
    [SerializeField, Header("�`���[�g���A���C���[�W�f�[�^")]
    TutorialData tutorialData;
    [SerializeField, Header("�`���[�g���A���p�l��")]
    Image image;
    [SerializeField, Header("�m�FUI�̐e")]
    GameObject selectPanel;
    [SerializeField, Header("�m�FUIPrefab")]
    GameObject confirmationUI;
    [SerializeField, Header("������")]
    TextMeshProUGUI tutorialText;

    // UIView
    TutorialUIView ui_View;
    // UIModel
    TutorialUModel ui_Model;
    // ����
    KeyInput input;
    // Image�̃��X�g
    List<Image> list_confirmationUI = new List<Image>();

    private void Awake()
    {
        // ���������m�FUI��LIst�ȏ�̏ꍇ������Ԃ�
        if(selectPanel.transform.childCount >= tutorialData.Data.Count)
        {
            // List�Ɏq�I�u�W�F�N�g���i�[
            for(int i = 0; i < selectPanel.transform.childCount; i++)
            {
                var child = selectPanel.transform.GetChild(i).gameObject;
                var img = child.GetComponent<Image>();
                // ���������m�FUI�����X�g�Ɋi�[
                list_confirmationUI.Add(img);
            }
            return;
        }

        // List�̗v�f�����m�FUI�𐶐�
        foreach (var Item in tutorialData.Data)
        {
            var instance = Instantiate(confirmationUI, this.transform.position, this.transform.localRotation, selectPanel.transform);
            var img = instance.GetComponent<Image>();
            // ���������m�FUI�C���[�W�����X�g�Ɋi�[
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

    // ���͂��������ꍇ�y�[�W��ύX
    private void SelectNumberChange()
    {
        var value = input.InputMove.x;
        if (value != 0 && ui_Model.IsSelect)
        {
            var num = ui_Model.Num;
            // �I�𐧌�
            ui_Model.SelectNum(value, list_confirmationUI.Count - 1);
            // �ύX�O�ƕω����Ȃ������ꍇ�Ԃ�
            if (num == ui_Model.Num)
            {
                return;
            }
            // ��ʂɔ��f
            ui_View.PanelSelect(ui_Model.Num, list_confirmationUI);
            image.sprite = tutorialData.Data[ui_Model.Num].sprite;
            tutorialText.text = tutorialData.Data[ui_Model.Num].explanatory;
        }
    }
}
