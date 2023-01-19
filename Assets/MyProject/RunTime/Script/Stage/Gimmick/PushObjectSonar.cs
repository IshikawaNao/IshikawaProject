using UnityEngine;

public class PushObjectSonar : MonoBehaviour
{
    // 1�Ȃ�\�i�[�I��0�Ȃ�I�t
    const float sonarOn = 1;
    const float sonarOff = 0;

    [SerializeField]
    Renderer rend;
    [SerializeField]
    Material mate;

    KeyInput input;

    private void Start()
    {
        input = KeyInput.Instance;
    }

    void Update()
    {
        if (input.SonarAction && rend.isVisible)
        {
            mate.SetFloat("_Boolean", sonarOn);
        }
        else 
        {
            mate.SetFloat("_Boolean", sonarOff);
        }
        
    }
}
