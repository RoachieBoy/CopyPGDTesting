using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject _brokenWallPiece;
    [SerializeField] private bool _keepPlayerSpeed = true;
    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>(); 
    }

    /// <summary>
    ///     When the player collides with a wall it will be replaced by stacked broken pieces
    /// </summary>
    public void BreakWall()
    {
        float basePos = (transform.localPosition.y + transform.localScale.y / 2);
        for (int i = 0; i < transform.localScale.y; i++)
        {
            GameObject brokenPiece = Instantiate(_brokenWallPiece, Vector2.zero, Quaternion.identity);
            brokenPiece.transform.localPosition =
                (Vector2) transform.position + new Vector2(0, i - transform.localScale.y / 2);
        }

        Destroy(gameObject);
    }

    /// <summary>
    ///     Check if there is a collision with a player to break the wall.
    ///     This function sets the player object and you can set if de player maintains its velocity
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.name.Equals("Player")) return;

        _player = other.gameObject.GetComponent<PlayerController>();

        if (!_player.isDashing) return;
        
        BreakWall();

        if (_keepPlayerSpeed)
        {
            _player.Rigidbody.velocity = _player.PrevVelocity;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Equals("Player")) return;
        
        if(collision.gameObject.GetComponent<PlayerController>().isDashing) BreakWall();
    }
}