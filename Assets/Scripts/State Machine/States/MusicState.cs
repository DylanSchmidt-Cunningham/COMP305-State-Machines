using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Music/State")]
public class MusicState : ScriptableObject {
    public MusicAction[] actions;
    public MusicTransition[] transitions;

    public void UpdateState(MusicStateController controller)
    {
        // Evaluate our actions and decisions/transitions
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(MusicStateController controller)
    {
        // Loop through all actions associated with this state.
        for (int i = 0; i < actions.Length; i++)
        {
            // Do actions.
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(MusicStateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if(decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
