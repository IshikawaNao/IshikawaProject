using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 7;
    float force = 7;
    float jumpPower = 5;
    Vector2 move;
    Rigidbody rb;
    PlayerJump pj;
    PlayerMove pm;

    #region@InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pj = GetComponent<PlayerJump>();
        pm = GetComponent<PlayerMove>();
    }

    
    void Update()
    {
        move = myInput.Player.Move.ReadValue<Vector2>();

        if (myInput.Player.Jump.triggered)
        {
            pj.Jump(rb, jumpPower);
        }
    }

    private void FixedUpdate()
    {
        pm.Move(rb, move, speed, force);
    }
}
