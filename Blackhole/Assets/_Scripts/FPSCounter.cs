using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [Tooltip("How fast the text fps updates")]
    public float UpdateInterval = 0.25f;
    [Tooltip("Text of the FPS")]
    [SerializeField] private Text FPSText;

    private float _timeUntilNextUpdate;

    private void Start()
    {
        _timeUntilNextUpdate = UpdateInterval;
    }

    private void Update()
    {
        _timeUntilNextUpdate -= Time.deltaTime;

        if (_timeUntilNextUpdate <= 0.0)
        {
            float fps = (int)(1f / Time.unscaledDeltaTime);
            FPSText.text = string.Format("{0:F0}", fps);

            _timeUntilNextUpdate = UpdateInterval;
        }
    }
}
