using UnityEngine;

public abstract class UiBaseState
{
    public abstract void EnterState(UiStateManager ui);
    
    public abstract void UpdateState(UiStateManager ui);
    
    public abstract void ExitState(UiStateManager ui);
}
