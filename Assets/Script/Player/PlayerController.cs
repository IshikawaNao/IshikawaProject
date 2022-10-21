using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject cameraPos;

    Rigidbody rb;
    GroundLayer gl;
    KeyInput input;

    IPlayerMover iMover;
    IMoveObject iObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gl = GetComponent<GroundLayer>();
        input = GetComponent<KeyInput>();

        iMover = new PlayerMove(rb, this.gameObject);
        iObject = new PushObject();
    }

    
    void Update()
    {
        if (input.InputJump())
        {
            if(gl.IsGround())
            {
                iMover.Jump();
            }
        }

        if (iObject.Push(this.gameObject))
        {
            iObject.Move(iObject.Box_rb(), input.InputMove(), this.gameObject);
            if (input.InputAction())
            {
                print("a");
            }
        }
    }

    private void FixedUpdate()
    {
        iMover.Move(input.InputMove(), cameraPos);
    }
}
