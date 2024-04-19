
public interface IActorSuperState 
{
    public IActorSubState CurrentSubState { get; }
    public void EnterSuperState(IActor actor);
    public void UpdateSuperState(IActor actor);
    public void ExitSuperState(IActor actor);
    public void ChangeSubState(IActorSubState newSubState);
}
