using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicTransition
{
    public MusicDecision decision;
    public MusicState trueState;
    public MusicState falseState;
}
