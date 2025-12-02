using UnityEngine;

public class ZoneTrigger : JumpscareSubject
{
    // Ketika player memasuki trigger zone, maka memanggil sinyal observers
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            
            NotifyObservers();
        }
    }
}