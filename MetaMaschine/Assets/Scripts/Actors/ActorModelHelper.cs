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
        Debug.Log("Init model");
        _defaultModel = Instantiate(newModel, Actor.DefaultModelContainer);

        _renderers = _defaultModel.GetComponentsInChildren<MeshRenderer>();
        _colliders = new MeshCollider[_renderers.Length];

        for(int i = 0; i < _renderers.Length; i++)
        {
            _colliders[i] = _renderers[i].gameObject.AddComponent<MeshCollider>();
            _renderers[i].AddComponent<ActorCollider>().Parent = Actor ;
        }
    }

    public void SetMaterial(Material material)
    {
        foreach(var renderer in _renderers) renderer.material = material;
    }
}
