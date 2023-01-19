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

    private void Update()
    {
        if(!placed && input.SonarAction)
        {
            mat[0].SetFloat("_Boolean", sonarOn);
        }
        else
        {
            mat[0].SetFloat("_Boolean", sonarOff);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Move")
        {
            placed = true;
            m_Renderer.material = mat[1];
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Move")
        {
            placed = false;
            m_Renderer.material = mat[0];
        }
    }
}
