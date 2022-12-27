using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using SoundSystem;

/// <summary>
/// �X�e�[�W�}�l�[�W���[
/// </summary>
public class StageManager : MonoBehaviour
{
    // �S�[���t���O
    public bool Goal { get; set; } = false;

    
    float intensityVal = 0;

    float gammmaValue = 0;

    Vector4 gammmaVal = Vector4.one;

    const float maxGammma = 0;
    const float minGammma = -0.4f;

    const float waiteTime = 0.02f;

    // ����
    const float gammmaAddend = 0.1f;
    const float intensityAddend = 0.25f;

    bool gammaDown = true;
    bool gammaUp = true;

    const float soundFadeTime = 1f;

    [SerializeField, Header("player")]
    PlayerController player;

    [SerializeField,Header("���U���g�p�l��")]
    GameObject goalPanale;

    [SerializeField,Header("Volume")]
    Volume postVol;

    [SerializeField, Header("OptionPanel")] 
    VolumeConfigUI volumeConfigUI;
    [SerializeField,Header("���͂��擾")]
    KeyInput input;

    LiftGammaGain liftGammaGain;
    ChromaticAberration chromaticAberration;


    void Start()
    {
        // �X���C�_�[�̐��l���f
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // �{�����[���̐ݒ�
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);

        postVol.profile.TryGet(out liftGammaGain);
        postVol.profile.TryGet(out chromaticAberration);

        SoundManager.Instance.PlayBGMWithFadeIn("Main", soundFadeTime);
    }

    void Update()
    {
        Cliar();
        SonarEffect();
    }

    void Cliar()
    {
        if(Goal)
        {
            player.IsMove = false;
            input.InputMove = Vector2.zero;
            input.CameraPos = player.transform.forward;
            goalPanale.SetActive(true);
        }
    }

    void SonarEffect()
    {
        if(input.SonarAction && gammaDown)
        {
            gammaDown = false;
            StartCoroutine(GammaDown());

        }
        else if(gammmaValue < maxGammma && gammaUp && !input.SonarAction)
        {
            gammaUp = false;
            StartCoroutine(GammaUp());
        }

        //gammma�l��intensity��ύX
        gammmaVal.w = gammmaValue;
        liftGammaGain.gamma.value = gammmaVal;
        chromaticAberration.intensity.value = intensityVal;
    }

    void ReStart()
    {
        SceneManager.LoadScene("Title");
    }

    IEnumerator GammaDown()
    {
        if (gammmaValue >= minGammma)
        {
            yield return new WaitForSeconds(waiteTime);
            gammmaValue -= gammmaAddend;
            intensityVal += intensityAddend;
            gammaDown = true;
            gammaUp = true;
        }
    }
    IEnumerator GammaUp()
    {
        yield return new WaitForSeconds(waiteTime);
        gammmaValue += gammmaAddend;
        intensityVal -= intensityAddend;
        gammaUp = true;
        gammaDown = true;
    }
}
