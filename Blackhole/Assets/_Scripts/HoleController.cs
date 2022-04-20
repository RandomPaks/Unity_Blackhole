using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HoleGenerator))]
public class HoleController : MonoBehaviour
{
    [Tooltip("Size of the hole")]
    public float HoleScale = 0.5f;

    private HoleGenerator _holeGenerator;

    private void Awake()
    {
        _holeGenerator = GetComponent<HoleGenerator>();
    }

    private void Update()
    {
        _holeGenerator.CutHole(new Vector2(transform.position.x, transform.position.z), transform.localScale * HoleScale);
    }
}
