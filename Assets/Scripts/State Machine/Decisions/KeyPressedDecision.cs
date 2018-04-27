using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Music/Decisions/KeyPressed")]
public class KeyPressedDecision : MusicDecision {

    public KeyCode key;

    void  Start()
    {

    }

    public override bool Decide(MusicStateController controller)
    {
        return KeyPressed(controller);
    }

    private bool KeyPressed(MusicStateController controller)
    {
        return Input.GetKeyDown(key);
    }
}
