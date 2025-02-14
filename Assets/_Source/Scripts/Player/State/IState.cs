using System.Collections;

public interface IState
{
    public void Enter();
    public void Update();
    public IEnumerator Coroutine();
    public void FixedUpdate();
    public void ApplyDamage();
    public void Exit();
}