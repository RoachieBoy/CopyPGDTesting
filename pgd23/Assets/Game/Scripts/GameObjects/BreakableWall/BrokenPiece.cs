using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _minimumSurvivalTime;

    /// <summary>
    ///     Set variables that are expensive to look up
    /// </summary>
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    ///     Checks every frame if a broken piece is in the bounds of the camera. If not, then it deletes it
    /// </summary>
    private void Update()
    {
            _minimumSurvivalTime -= Time.deltaTime;

        if (_minimumSurvivalTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
