using Cysharp.Threading.Tasks;

public interface IUIView
{
    public void EnabldUIAnimation();
    // 無効時のアニメーション
    public UniTask DisableAnimation();
}
