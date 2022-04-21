using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Tooltip("Time before text decay")]
    public float DeathTimer = 5.0f;
    [Tooltip("Where the text will float towards locally")]
    public Vector3 EndOffsetPosition;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Color _originalColor;
    private TextMesh _text;
    private float _timer;

    private void Awake()
    {
        _text = GetComponent<TextMesh>();

        _originalColor = _text.color;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= DeathTimer)
        {
            gameObject.SetActive(false);
        }
        else if (_timer > DeathTimer / 2)
        {
            _text.color = Color.Lerp(_text.color, Color.clear, (_timer - (DeathTimer / 2)) / (DeathTimer - (DeathTimer / 2)));
        }
        
        transform.position = Vector3.Lerp(_startPosition, _endPosition, Mathf.Sin(_timer / DeathTimer));
    }

    public void ResetFloatingText()
    {
        _timer = 0;

        //whenever text gets spawned it spawns from the player 
        _startPosition = GameManager.Instance.Player.transform.position;
        _endPosition = _startPosition + EndOffsetPosition;

        _text.color = _originalColor;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}