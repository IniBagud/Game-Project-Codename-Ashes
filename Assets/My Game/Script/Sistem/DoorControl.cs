using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [Header("Door Settings")]
    public string requiredKeyName; // Nama item kunci 
    public bool isLocked = true;

    [Header("Animation")]
    public Animator doorAnimator; // komponen Animator pintu
    // mengatur audio dan pastikan Animator memiliki parameter bool "isOpen"
    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip lockedSound;

    private bool isOpen = false;

    // Fungsi ini akan dipanggil oleh Player saat menekan tombol interaksi
    public void TryOpenDoor()
    {
        if (isOpen)
        {
            // pintu bisa ditutup kembali
            CloseDoor();
            return;
        }
        // Cek apakah pintu terkunci
        if (isLocked)
        {
            // Cek ke Inventory apakah punya kunci, jika ada, buka pintu
            if (InventoryManager.Instance.HasItem(requiredKeyName))
            {
                UnlockAndOpen();
            }
            else
            {
                // Pintu terkunci dan tidak punya kunci
                Debug.Log("Pintu Terkunci! Butuh: " + requiredKeyName);
                PlaySound(lockedSound);
            }
        }
        else
        {
            // Pintu tidak terkunci, langsung buka
            OpenDoor();
        }
    }

    // Fungsi untuk membuka pintu setelah menggunakan kunci
    void UnlockAndOpen()
    {
        isLocked = false;
        Debug.Log("Menggunakan Kunci: " + requiredKeyName);
        OpenDoor();
    }
    // Fungsi untuk membuka pintu
    void OpenDoor()
    {
        isOpen = true;
        doorAnimator.SetBool("isOpen", true); 
        PlaySound(openSound);
    }
    // Fungsi untuk menutup pintu
    void CloseDoor()
    {
        isOpen = false;
        doorAnimator.SetBool("isOpen", false);
        PlaySound(openSound); 
    }
    // Fungsi untuk memainkan suara
    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}