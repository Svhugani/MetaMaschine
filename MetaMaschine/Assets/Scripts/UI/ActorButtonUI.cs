using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActorButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actorName;
    [SerializeField] private RawImage actorStatusIcon;
    [SerializeField] private Button button;

    public IActor Actor { get; set; } // Add a property to store the actor reference

    public event Action<IActor> OnButtonClick; // Change the event to include an IActor parameter

    private void Awake()
    {
        button.onClick.AddListener(() => TriggerOnClick());
        actorStatusIcon.enabled = false;
    }

    public void UpdateComponent(string value, Texture2D texture, Color color, IActor actor)
    {
        actorName.text = value;
        actorName.color = color;
        Actor = actor; // Store the actor reference

        if (texture == null)
        {
            actorStatusIcon.enabled = false;
        }
        else
        {
            actorStatusIcon.enabled = true;
            actorStatusIcon.texture = texture;
            actorStatusIcon.color = color;
        }
    }

    private void TriggerOnClick()
    {
        OnButtonClick?.Invoke(Actor); // Pass the actor to the event
    }
}
