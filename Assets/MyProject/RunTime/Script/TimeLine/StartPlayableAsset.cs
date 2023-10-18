using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class StartPlayableAsset : PlayableAsset
{
    [SerializeField]
    public int mIntSample; // �N���b�v���N���b�N�����ۂ�, Inspector �r���[�Ŏw��\.
    [SerializeField]
    public ExposedReference<StageManager> mGameObjectSample;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        StartPlayableBehaviour behaviour = new StartPlayableBehaviour();
        behaviour.IntSample = mIntSample;
        behaviour.Gameobj = mGameObjectSample.Resolve(graph.GetResolver());

        return Playable.Create(graph);
    }
}
