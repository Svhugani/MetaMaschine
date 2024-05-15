using UnityEngine;
using Zenject;

public class ActorMarker : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] corners;
    [SerializeField] private Transform visualContainer;

    private IVisualManager _visualManager;
    private Material _currentMaterial;

    [Inject] public void Construct(IVisualManager visualManager)
    {
        _visualManager = visualManager;
    }
    public void Mark(IActor actor)
    {
        Bounds bounds = actor.GetBounds();
        visualContainer.position = bounds.center;
        Vector3 ext = bounds.extents;

        corners[0].transform.localPosition = new Vector3(ext.x, ext.y, ext.z);
        corners[1].transform.localPosition = new Vector3(-ext.x, ext.y, ext.z);
        corners[2].transform.localPosition = new Vector3(-ext.x, ext.y, -ext.z);
        corners[3].transform.localPosition = new Vector3(ext.x, ext.y, -ext.z);
        corners[4].transform.localPosition = new Vector3(ext.x, -ext.y, ext.z);
        corners[5].transform.localPosition = new Vector3(-ext.x, -ext.y, ext.z);
        corners[6].transform.localPosition = new Vector3(-ext.x, -ext.y, -ext.z);
        corners[7].transform.localPosition = new Vector3(ext.x, -ext.y, -ext.z);

        switch (actor.ActorState)
        {
            case ActorState.Hovered: SetMaterial(_visualManager.GetMarkerHoverMaterial()); break;
            case ActorState.Selected: SetMaterial(_visualManager.GetMarkerSelectMaterial()); break;
            default: Debug.Log("no state");  break;
        }

        SetScaleToActor(actor);

        visualContainer.gameObject.SetActive(true); 
    }

    public void Unmark()
    {
        visualContainer.gameObject.SetActive(false);    
    }

    private void SetMaterial(Material material)
    {
        //if(material == _currentMaterial) return;
        Debug.Log("Material: " + material.name);
        foreach (var c in corners) c.material = material;
        _currentMaterial = material;
    }

    private void SetScaleToActor(IActor actor)
    {
        Vector3 size = actor.GetBounds().extents;
        
        SetScale(Mathf.Min(Mathf.Min(size.x, size.y), size.z));
    }

    private void SetScale(float scale)
    {
        foreach (var c in corners) c.transform.localScale = scale * Vector3.one;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(visualContainer.transform.position, 2);
    }
}
