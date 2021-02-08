using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMoveHandler _playerMove;

    private PlayerInput _playerInput;

    #region Enable/Disable

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    #endregion

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Update()
    {
        _playerMove.Move(_playerInput.PlayerAction.Move.ReadValue<Vector2>());
    }
}
