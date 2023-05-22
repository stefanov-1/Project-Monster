using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void EnterState(PlayerStateManager stateManager);
    public abstract void UpdateState(PlayerStateManager stateManager);
    public abstract void FixedUpdateState(PlayerStateManager stateManager);
    public abstract void ExitState(PlayerStateManager stateManager);

    public override string ToString()
    {
        return GetType().Name;
    }
}
