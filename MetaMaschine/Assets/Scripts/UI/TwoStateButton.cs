using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TwoStateButton : MonoBehaviour
{
    [SerializeField] private bool initStateOn = false;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    [SerializeField] private Color onColor = Color.white;
    [SerializeField] private Color offColor = Color.white;

    private bool _isOn;

    public UnityEvent OnAction;
    public UnityEvent OffAction;

    private void Awake()
    {
        button.onClick.AddListener(() => ToggleState());

        _isOn = !initStateOn;
        ToggleState();
    }

    private void ToggleState()
    {
        _isOn = !_isOn;

        if (_isOn)
        {
            icon.color = onColor;
            OnAction?.Invoke();
        }
        else
        {
            icon.color = offColor;
            OffAction?.Invoke();
        }
        
    }



}
