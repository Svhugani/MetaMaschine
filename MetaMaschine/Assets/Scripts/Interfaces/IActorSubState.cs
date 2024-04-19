
public interface IActorSubState 
{
    public void EnterSubState(IActor actor);
    public void UpdateSubState(IActor actor);
    public void ExitSubState(IActor actor);
}
