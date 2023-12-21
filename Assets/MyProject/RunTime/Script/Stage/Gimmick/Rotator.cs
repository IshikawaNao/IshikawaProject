using UnityEngine;

public class Rotator : MonoBehaviour 
{
    [SerializeField, Header("回転")]
    Vector3 rot;
    [SerializeField, Header("回転するか否か")]
    bool isRotate = true;

    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(rot);
        }
    }
}
