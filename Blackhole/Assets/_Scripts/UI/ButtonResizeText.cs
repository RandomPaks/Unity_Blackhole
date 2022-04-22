using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
public class ButtonResizeText : MonoBehaviour
{
    [Tooltip("How big the text will grow")]
    [SerializeField] private int _fontAddSize = 6;

    private int _normFontSize;

    private Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();

        EventTrigger trigger = gameObject.AddComponent(typeof(EventTrigger)) as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { OnEnter(); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener((eventData) => { OnExit(); });
        trigger.triggers.Add(exit);
    }

    private void Start()
    {
        _normFontSize = text.fontSize;
    }

    private void OnEnter() => text.fontSize = _normFontSize + _fontAddSize;

    private void OnExit() => text.fontSize = _normFontSize;
}