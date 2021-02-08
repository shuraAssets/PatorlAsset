using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveHandler : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private CharacterController _controller;

    public void Move(Vector3 direction)
    {


        var move = new Vector3(direction.x, 0, direction.y);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        _controller.Move(move * speed * Time.deltaTime);
    }



}
