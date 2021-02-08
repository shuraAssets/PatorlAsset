using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public delegate void PlayerContact();
    public static PlayerContact WinGame;

    private void OnTriggerEnter(Collider other)
    {
        var component = other.GetComponents<PlayerMoveHandler>();

        if (component != null)
        {
            Debug.Log("Exe");
            WinGame?.Invoke();
        }
    }
}
