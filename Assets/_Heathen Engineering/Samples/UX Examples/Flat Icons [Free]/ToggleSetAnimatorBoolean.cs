using UnityEngine;

namespace HeathenEngineering.UX.Samples
{
    public class ToggleSetAnimatorBoolean : MonoBehaviour
    {
        public Animator anim;
        public string booleanName;

        public void SetBoolean(bool value)
        {
            anim.SetBool(booleanName, value);
        }
    }
}
