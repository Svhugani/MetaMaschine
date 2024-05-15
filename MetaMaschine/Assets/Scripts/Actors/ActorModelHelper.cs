using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorModelHelper : MonoBehaviour
{
    [SerializeField] private bool initializeFromContainer = false;
    public Actor Actor { get; set; }
    private MeshRenderer[] _renderers;
    private MeshCollider[] _colliders;
    private GameObject _defaultModel;

    public Bounds Bounds { get; private set; }

    private void Start()
    {
        if(initializeFromContainer)
        {
            GameObject initModel = Actor.DefaultModelContainer.GetChild(0).gameObject;
            SetModel(initModel);
            Destroy(initModel);
        }
    }

    public void SetModel(GameObject newModel)
    {
        if (newModel == null) return;
        if(_defaultModel != null) Destroy(_defaultModel);

        _defaultModel = Instantiate(newModel, Actor.DefaultModelContainer);
        _renderers = _defaultModel.GetComponentsInChildren<MeshRenderer>();
        _colliders = new MeshCollider[_renderers.Length];

        for(int i = 0; i < _renderers.Length; i++)
        {
            _colliders[i] = _renderers[i].gameObject.AddComponent<MeshCollider>();
            _renderers[i].AddComponent<ActorCollider>().Parent = Actor ;
        }

        CalculateBounds();

        Actor.SetLabelValue(_defaultModel.name);
        
    }

    public void SetMaterial(Material material)
    {
        foreach(var renderer in _renderers) renderer.material = material;
    }

    private void CalculateBounds()
    {
        if (_renderers.Length == 0)
        {
            Bounds = new Bounds();
            return;
        }

        Bounds bounds = new Bounds(_renderers[0].bounds.center, Vector3.zero);
        foreach (var r in _renderers)
        {
            bounds.Encapsulate(r.bounds);
        }

        Bounds = bounds;
        Actor.LabelContainer.position = Actor.transform.position + (2 * Bounds.extents.y + 1) * Vector3.up;
    }

    private void OnDrawGizmos()
    {
        if (Actor == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Bounds.center, 2);
    }
}
