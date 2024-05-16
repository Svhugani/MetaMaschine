using UnityEngine;
using Zenject;

public class DataSimulator : MonoBehaviour
{
    private IActorManager _actorManager;
    private float _timer;
    [Inject] public void Construct(IActorManager actorManager)
    {
        _actorManager = actorManager;   
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_actorManager != null) 
        {
            if(_timer >= 1) 
            {
                _timer = 0;

                
                int index = Random.Range(0, _actorManager.GetActorCount());
                if(_actorManager.GetActor(index, out IActor actor))
                {
                    float r = Random.Range(0.0f, 1.0f);

                    if (r < .2f)
                    {
                        _actorManager.SetErrorInfo(actor);
                    }

                    else if (r < .5f)
                    {
                        _actorManager.SetWarningInfo(actor);
                    }

                    else
                    {
                        _actorManager.SetDefaultInfo(actor);
                    }
                }

            }
        }
    }
}
