using UnityEngine;

public class HouseRoom : MonoBehaviour
{
    bool placed = false;
    public bool Placed { get { return placed; } }

    // 1ならソナーオン0ならオフ
    const float sonarOn = 1;
    const float sonarOff = 0;

    [SerializeField]
    Renderer m_Renderer;

    [SerializeField]
    Material[] mat;


    KeyInput input;

    private void Start()
    {
        input = KeyInput.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Move" || other.gameObject.tag == "Player")
        {
            placed = true;
            m_Renderer.material = mat[1];
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Move" || other.gameObject.tag == "Player")
        {
            placed = false;
            m_Renderer.material = mat[0];
        }
    }
}
