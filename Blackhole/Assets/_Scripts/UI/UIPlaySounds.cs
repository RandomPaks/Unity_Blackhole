using Sound;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class UIPlaySounds : MonoBehaviour
{
    [Tooltip("Audio to play on click")]
    [SerializeField] private string _audioName;

    /// <summary>
    /// uses audio manager to play the sound
    /// </summary>

    private void Awake()
    {
        EventTrigger trigger;
        if (!TryGetComponent<EventTrigger>(out trigger))
            trigger = gameObject.AddComponent(typeof(EventTrigger)) as EventTrigger;

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnClick(); });
        trigger.triggers.Add(entry);
    }

    private void OnClick()
    {
        if(AudioManager.Instance != null)
            AudioManager.Instance.PlayOneShot(_audioName);
    }
}