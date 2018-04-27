using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu (menuName = "PluggableAI/Music/Actions/ChangeText")]
public class ChangeTExtAction : MusicAction {

    public string trackName;

    public override void Act(MusicStateController controller)
    {
        ChangeText(controller);
    }

    private void ChangeText(MusicStateController controller)
    {
        controller.trackDisplay.text = trackName;
    }
}
