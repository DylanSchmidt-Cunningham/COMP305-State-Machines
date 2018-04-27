using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusicDecision : ScriptableObject {
    public abstract bool Decide(MusicStateController controller);
}
