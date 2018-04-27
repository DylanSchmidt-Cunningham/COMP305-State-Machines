using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu (menuName = "PluggableAI/Music/Actions/PlayMusic")]
public class PlayMusicAction : MusicAction {

    public AudioMixerSnapshot myGroove;

    public override void Act(MusicStateController controller)
    {
        Play(controller);
    }

    private void Play(MusicStateController controller)
    {
        myGroove.TransitionTo(0.01f);
    }
}
