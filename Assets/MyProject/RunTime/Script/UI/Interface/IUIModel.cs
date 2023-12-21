using UniRx;
using UnityEngine;

public interface IUIModel 
{
    IReadOnlyReactiveProperty<int> Num { get; }
    int Stet { get; }
    IReadOnlyReactiveProperty<bool> IsSelect { get; }
    void SelectNum(Vector2 value);
    void Decision();
}
