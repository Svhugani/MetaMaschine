using UnityEngine;
using UnityEngine.UI;

public class AbstractCanvasPanel : MonoBehaviour
{
    protected Canvas Canvas { get; private set; }
    protected CanvasScaler CanvasScaler { get; private set; }
    protected GraphicRaycaster GraphicRaycaster { get; private set; }

    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
        CanvasScaler = GetComponent<CanvasScaler>();
        GraphicRaycaster = GetComponent<GraphicRaycaster>();

    }

    public void SetVisibility(bool visibility)
    {
        Canvas.enabled = visibility;
        CanvasScaler.enabled = visibility;
        GraphicRaycaster.enabled = visibility;
    }
}
