using System;
using UnityEngine;

public class WeaponColliderCheck : MonoBehaviour
{
    private Collider2D _hitBox;

    public event Action<Collider2D> TriggerEntered;
    public event Action<Collider2D> TriggerExited;

    private void Awake()
    {
        _hitBox = GetComponent<Collider2D>();
        _hitBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEntered?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerExited?.Invoke(other);
    }

    public void SetCheckActive(bool isActive)
    {
        _hitBox.enabled = isActive;
    }
}