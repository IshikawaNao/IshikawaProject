using UnityEngine;

public class PushObjectSonar : MonoBehaviour
{
    // 1ならソナーオン0ならオフ
    const float sonarOn = 1;
    const float sonarOff = 0;

    [SerializeField]
    Renderer rend;
    [SerializeField]
    Material mate;
    [SerializeField]
    KeyInput input;

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
