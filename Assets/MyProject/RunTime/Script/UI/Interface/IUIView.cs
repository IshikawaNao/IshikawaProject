using Cysharp.Threading.Tasks;

public interface IUIView
{
    public void EnabldUIAnimation();
    // �������̃A�j���[�V����
    public UniTask DisableAnimation();
}
