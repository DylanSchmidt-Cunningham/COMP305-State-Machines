using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusicAction : ScriptableObject {
    public abstract void Act(MusicStateController controller);
}
