/// <summary>
/// ステージマネージャー
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public bool Goal { get; set; } = false;

    [SerializeField]
    GameObject goalPanale;
    KeyInput input;

    void Start()
    {
        input = GameObject.Find("Player").GetComponent<KeyInput>();
    }

    void Update()
    {
        Cliar();
    }

    void Cliar()
    {
        if(Goal)
        {
            input.enabled = false;
            goalPanale.SetActive(true);
            Invoke("ReStart", 1.5f);
        }
    }

    void ReStart()
    {
        SceneManager.LoadScene("Main");
    }
}
