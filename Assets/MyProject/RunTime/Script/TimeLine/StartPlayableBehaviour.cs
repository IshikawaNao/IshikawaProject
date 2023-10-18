using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Collections.Generic;

// A behaviour that is attached to a playable
public class StartPlayableBehaviour : PlayableBehaviour
{
    private int mIntSample;
    public int IntSample { set { mIntSample = value; } }
    private StageManager mGameObject;
    public StageManager Gameobj { set { mGameObject = value; } }

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        mGameObject.IsTimeLine = true;
        Debug.Log("b");
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        mGameObject.IsTimeLine = false;
        Debug.Log("c");
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        
    }
}
