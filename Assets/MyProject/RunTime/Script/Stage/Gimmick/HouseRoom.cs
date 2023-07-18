using UnityEngine;

public class HouseRoom : MonoBehaviour
{
    bool placed = false;
    public bool Placed { get { return placed; } }

    // 1ならソナーオン0ならオフ
    const int sonarOn = 1;
    const int sonarOff = 0;

    [SerializeField]
    Renderer m_Renderer;

    [SerializeField]
    Material[] mat;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Move" || other.gameObject.tag == "Player")
        {
            placed = true;
            m_Renderer.material = mat[sonarOn];
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Move" || other.gameObject.tag == "Player")
        {
            placed = false;
            m_Renderer.material = mat[sonarOff];
        }
    }
}
