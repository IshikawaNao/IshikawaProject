using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

/// <summary>
/// ステージマネージャー
/// </summary>
public class StageManager : MonoBehaviour
{
    public bool Goal { get; set; } = false;


    [SerializeField]
    GameObject goalPanale;
    KeyInput input;

    void Start()
    {
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
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
            input.InputMove = new Vector2(0, 0);
            goalPanale.SetActive(true);
            Invoke("ReStart", 1.5f);
        }
    }

    void ReStart()
    {
        SceneManager.LoadScene("Main");
    }
}
