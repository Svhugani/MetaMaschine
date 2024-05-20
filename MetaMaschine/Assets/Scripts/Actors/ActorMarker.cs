using UnityEngine;
using Zenject;
using DG.Tweening;

public class ActorMarker : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] corners;
    [SerializeField] private Transform visualContainer;

    private IVisualManager _visualManager;
    private Material _currentMaterial;
    private Tween _currentTween;

    private const float Y_OFFSET = .1f;

    [Inject]
    public void Construct(IVisualManager visualManager)
    {
        _visualManager = visualManager;
    }

    public void Mark(IActor actor)
    {
        // Kill any existing tweens to avoid overlap
        _currentTween?.Kill();

        Bounds bounds = actor.GetBounds();
        visualContainer.position = bounds.center;
        Vector3 ext = bounds.extents;

        corners[0].transform.localPosition = new Vector3(ext.x, ext.y, ext.z);
        corners[1].transform.localPosition = new Vector3(-ext.x, ext.y, ext.z);
        corners[2].transform.localPosition = new Vector3(-ext.x, ext.y, -ext.z);
        corners[3].transform.localPosition = new Vector3(ext.x, ext.y, -ext.z);
        corners[4].transform.localPosition = new Vector3(ext.x, -ext.y + Y_OFFSET, ext.z);
        corners[5].transform.localPosition = new Vector3(-ext.x, -ext.y + Y_OFFSET, ext.z);
        corners[6].transform.localPosition = new Vector3(-ext.x, -ext.y + Y_OFFSET, -ext.z);
        corners[7].transform.localPosition = new Vector3(ext.x, -ext.y + Y_OFFSET, -ext.z);

        switch (actor.ActorState)
        {
            case ActorState.Hovered: SetMaterial(_visualManager.GetMarkerHoverMaterial()); break;
            case ActorState.Selected: SetMaterial(_visualManager.GetMarkerSelectMaterial()); break;
            default: break;
        }

        SetScaleToActor(actor);

        visualContainer.gameObject.SetActive(true);

        // Create a sequence for the wobble effect
        Sequence sequence = DOTween.Sequence();
        sequence.Append(visualContainer.DOScale(Vector3.one * 1.1f, 0.15f).SetEase(Ease.Linear));
        sequence.Append(visualContainer.DOScale(Vector3.one, 0.2f).SetEase(Ease.Linear));

        _currentTween = sequence;
    }

    public void Unmark()
    {
        _currentTween?.Kill();
        visualContainer.gameObject.SetActive(false);

/*        Sequence sequence = DOTween.Sequence();
        sequence.Append(visualContainer.DOScale(Vector3.one * 1.2f, 0.25f).SetEase(Ease.OutBack));
        sequence.Append(visualContainer.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBack));
        sequence.OnComplete(() =>
        {
            visualContainer.gameObject.SetActive(false);
        });

        _currentTween = sequence;*/
    }

    private void SetMaterial(Material material)
    {
        if(material == _currentMaterial) return;
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
