using UnityEngine;
using Zenject;
using DG.Tweening;
using TMPro;

public class ActorInfoHelper : MonoBehaviour
{
    [field: SerializeField] public Transform LabelContainer { get; private set; }
    [field: SerializeField] public Transform IconContainer { get; private set; }
    [field: SerializeField] public TextMeshPro Label { get; private set; }
    [field: SerializeField] public SpriteRenderer Icon { get; private set; }
    public Actor Actor { get; set; }

    private IViewManager _viewManager;
    private IVisualManager _visualManager;

    private Tween _iconMoveTween;
    private Tween _iconFadeTween;
    private Tween _labelFadeTween;

    // Define the target size for the icon in world units
    private readonly Vector2 targetIconSize = new Vector2(1f, 1f); // Change these values to your desired size

    // Define the off-screen position and the target position
    private readonly Vector3 offScreenPosition = new Vector3(-7, 0, 0); // Change this to your off-screen position
    private readonly Vector3 targetPosition = new Vector3(-2.6f, 0, 0); // Change this to your target position

    private SpriteRenderer _iconContainerRenderer;

    [Inject]
    public void Construct(IViewManager viewManager, IVisualManager visualManager)
    {
        _viewManager = viewManager;
        _visualManager = visualManager;
        _iconContainerRenderer = IconContainer.GetComponent<SpriteRenderer>();
    }

    public void SetInfoPriority(InfoPriority priority)
    {
        switch (priority)
        {
            case InfoPriority.Low: SetLayer(0); break;
            case InfoPriority.High: SetLayer(20); break;
            case InfoPriority.Medium: SetLayer(10); break;
        }
    }

    private void SetLayer(int layer)
    {
        Label.sortingOrder = layer;
        Icon.sortingOrder = layer;
        LabelContainer.GetComponent<SpriteRenderer>().sortingOrder = layer;
        _iconContainerRenderer.sortingOrder = layer;
    }

    public void SetTextValue(string text)
    {
        if (_labelFadeTween != null)
        {
            _labelFadeTween.Kill();
        }

        // Fade out the current text
        _labelFadeTween = Label.DOFade(0, 0.25f).OnComplete(() =>
        {
            // Set the new text value
            Label.text = text;

            // Fade in the new text
            _labelFadeTween = Label.DOFade(1, 0.35f).OnComplete(() =>
            {
                _labelFadeTween = null;
                LabelContainer.gameObject.SetActive(true);
            });
        });
    }

    public void SetIcon(string iconID)
    {
        Texture2D tex = _visualManager.GetIcon(iconID);
        if (tex == null) return;

        SetIcon(tex);
    }

    public void SetIcon(Texture2D tex)
    {
        if (_iconMoveTween != null)
        {
            _iconMoveTween.Kill();
        }
        if (_iconFadeTween != null)
        {
            _iconFadeTween.Kill();
        }

        IconContainer.gameObject.SetActive(true);

        // Slide out and fade out the current icon and container
        _iconMoveTween = IconContainer.DOLocalMove(offScreenPosition, 0.25f);
        _iconFadeTween = Icon.DOFade(0, 0.25f);
        _iconContainerRenderer.DOFade(0, 0.25f).OnComplete(() =>
        {
            if (tex != null)
            {
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                Icon.sprite = sprite;

                // Calculate the scaling factor to achieve the target icon size
                Vector2 spriteSize = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
                Vector3 scale = new Vector3(targetIconSize.x / spriteSize.x, targetIconSize.y / spriteSize.y, 1f);
                Icon.transform.localScale = scale;
            }

            // Slide in and fade in the new icon and container
            _iconMoveTween = IconContainer.DOLocalMove(targetPosition, 0.35f);
            _iconFadeTween = Icon.DOFade(1, 0.35f);
            _iconContainerRenderer.DOFade(1, 0.35f).OnComplete(() =>
            {
                _iconMoveTween = null;
                _iconFadeTween = null;
            });
        });
    }

    public void SetIconVisibility(bool visibility)
    {
        if (_iconMoveTween != null)
        {
            _iconMoveTween.Kill();
        }
        if (_iconFadeTween != null)
        {
            _iconFadeTween.Kill();
        }

        if (visibility)
        {
            IconContainer.gameObject.SetActive(true);
            _iconMoveTween = IconContainer.DOLocalMove(targetPosition, 0.5f);
            _iconFadeTween = Icon.DOFade(1, 0.5f);
            _iconContainerRenderer.DOFade(1, 0.5f).OnComplete(() =>
            {
                _iconMoveTween = null;
                _iconFadeTween = null;
            });
        }
        else
        {
            _iconMoveTween = IconContainer.DOLocalMove(offScreenPosition, 0.5f);
            _iconFadeTween = Icon.DOFade(0, 0.5f);
            _iconContainerRenderer.DOFade(0, 0.5f).OnComplete(() =>
            {
                IconContainer.gameObject.SetActive(false);
                _iconMoveTween = null;
                _iconFadeTween = null;
            });
        }
    }

    public void SetLabelVisibility(bool visibility)
    {
        if (_labelFadeTween != null)
        {
            _labelFadeTween.Kill();
        }

        if (visibility)
        {
            LabelContainer.gameObject.SetActive(true);
            _labelFadeTween = Label.DOFade(1, 0.5f).OnComplete(() =>
            {
                _labelFadeTween = null;
            });
        }
        else
        {
            _labelFadeTween = Label.DOFade(0, 0.5f).OnComplete(() =>
            {
                LabelContainer.gameObject.SetActive(false);
                _labelFadeTween = null;
            });
        }
    }

    private void Update()
    {
        Actor.LabelContainer.forward = -_viewManager.GetCurrentCamera().transform.forward;
    }
}
