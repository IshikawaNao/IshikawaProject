using UnityEngine;

public class OperationExplanationMove : MonoBehaviour
{
    [SerializeField]
    GameObject plyer;

    [SerializeField]
    GameObject climb;
    [SerializeField]
    GameObject push;
    [SerializeField]
    GameObject jump;

    private RectTransform myRectTfm;

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
    }

    private void Update()
    {
        myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main ,plyer.transform.position + Vector3.up);
    }

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
