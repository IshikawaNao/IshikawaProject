using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class StartPlayableAsset : PlayableAsset
{
    [SerializeField]
    public int mIntSample; // クリップをクリックした際に, Inspector ビューで指定可能.
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
