using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicStateController : MonoBehaviour {

    public MusicState currentState;
    public MusicState sameState;
    public Text trackDisplay;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState(this);
	}

    public void TransitionToState(MusicState nextState)
    {
        if(nextState != sameState)
        {
            currentState = nextState;
        }
    }
}
