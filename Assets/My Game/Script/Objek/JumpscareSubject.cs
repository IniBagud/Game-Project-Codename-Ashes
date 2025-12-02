using UnityEngine;
using System;


public abstract class JumpscareSubject : MonoBehaviour
{
    // Event yang akan didengarkan oleh Observer
    public event Action OnJumpscareTriggered;

    // memastikan agar jumpscare hanya terjadi sekali 
    [SerializeField] protected bool triggerOnce = true;
    protected bool hasTriggered = false;

    // Fungsi untuk memanggil Observer
    protected void NotifyObservers()
    {
        if (triggerOnce && hasTriggered) return;

        // Beritahu semua observer yang subscribe
        if (OnJumpscareTriggered != null)
        {
            OnJumpscareTriggered.Invoke();
            hasTriggered = true;
        }
    }
}