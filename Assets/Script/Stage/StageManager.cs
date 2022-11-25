using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// ステージマネージャー
/// </summary>
public class StageManager : MonoBehaviour
{
    public bool Goal { get; set; } = false;

    public float intensityVal = 0;

    float gammmaValue = 0;

    float waiteTime = 0.02f;

    bool gammadown = true;
    bool gammaup = true;

    [SerializeField]
    GameObject goalPanale;

    [SerializeField]
    Volume postVol;

    LiftGammaGain liftGammaGain;
    ChromaticAberration chromaticAberration;

    KeyInput input;

    void Start()
    {
        postVol.profile.TryGet(out liftGammaGain);
        postVol.profile.TryGet(out chromaticAberration);
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
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
            input.enabled = false;
            input.InputMove = new Vector2(0, 0);
            goalPanale.SetActive(true);
            Invoke("ReStart", 1.5f);
        }
    }

    void SonarEffect()
    {
        if(input.SonarAction)
        {
            if(gammadown)
            {
                gammadown = false;
                StartCoroutine(GammaDown());
            }
            
        }
        else
        {
            if(gammmaValue < 0)
            {
                if(gammaup)
                {
                    gammaup = false;
                    StartCoroutine(GammaUp());
                }
            }
        }
        liftGammaGain.gamma.value = new Vector4(1f, 1f, 1f, gammmaValue);
        chromaticAberration.intensity.value = intensityVal;
    }

    void ReStart()
    {
        SceneManager.LoadScene("Main");
    }

    IEnumerator GammaDown()
    {
        if (gammmaValue >= -0.4f)
        {
            yield return new WaitForSeconds(waiteTime);
            gammmaValue -= 0.1f;
            intensityVal += 0.25f;
            gammadown = true;
            gammaup = true;
        }
    }
    IEnumerator GammaUp()
    {
        yield return new WaitForSeconds(waiteTime);
        gammmaValue += 0.1f;
        intensityVal -= 0.25f;
        gammaup = true;
        gammadown = true;
    }
}
