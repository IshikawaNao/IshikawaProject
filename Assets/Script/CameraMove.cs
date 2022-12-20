using UnityEngine;

public class CameraMove : MonoBehaviour
{
    CameraRotate cm;
    float dis = 0;
    float cmPos = 8;

    Vector3 target = new Vector3(0, 1, 8);

    bool colliderHit = false;
    bool rayHit = false;


    private void Start()
    {
        cm = GameObject.Find("CameraManager").GetComponent<CameraRotate>();
        this.transform.localPosition = new Vector3(0, 1, 8);
    }

    private void Update()
    {
        Avoid();
        Rayhit();
    }

    void Avoid()
    {
        if (colliderHit)
        {
            this.transform.localPosition = new Vector3(0, 1, Mathf.Clamp(cmPos, 3, 8));
        }
        else if (!colliderHit && cm.VerticalVal < -5 && !rayHit)
        {
            cmPos = 8;
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, target, 0.1f);
        }
    }

    void Rayhit()
    {
        RaycastHit hit;
        Vector3 rayPosition = this.transform.position;
        
        if(Physics.SphereCast(rayPosition, 5, Vector3.right, out hit, 10.0f))
        {   
            if(hit.collider.gameObject.name == "Player")
            {
                return;
            }
            rayHit = true;
        }
        else
        {
            rayHit = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position , 1.0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        dis = -0.1f;
        cmPos += dis;
        colliderHit = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliderHit = false;
    }
}
