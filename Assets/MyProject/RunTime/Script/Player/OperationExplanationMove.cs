using UnityEngine;

public class OperationExplanationMove : MonoBehaviour
{
    [SerializeField]
    GameObject plyer;

    [SerializeField]
    GameObject push;
    [SerializeField]
    GameObject climb;
    [SerializeField]
    GameObject jump;


    public void ClimbCheck(bool check)
    {
        climb.SetActive(check);
    }

    public void PushCheck(bool check)
    {
        push.SetActive(check);
    }

    public void JumpCheck(bool check)
    {
        jump.SetActive(check);
    }


}
