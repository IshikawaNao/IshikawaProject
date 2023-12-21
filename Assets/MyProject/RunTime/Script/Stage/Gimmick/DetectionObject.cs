using SoundSystem;
using UnityEngine;

public class DetectionObject: MonoBehaviour
{
    bool placed = false;
    public bool Placed { get { return placed; } }

    // 1ならソナーオン0ならオフ
    const int SonarOn = 1;
    const int SonarOff = 0;

    [SerializeField]
    Renderer m_Renderer;

    [SerializeField]
    Material[] mat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Move" || other.gameObject.tag == "Player")
        {
            placed = true;
            m_Renderer.material = mat[SonarOn];
            SoundManager.Instance.PlayOneShotSe((int)SEList.PressureSensitive);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Move" || other.gameObject.tag == "Player")
        {
            placed = false;
            m_Renderer.material = mat[SonarOff];
        }
    }
}
