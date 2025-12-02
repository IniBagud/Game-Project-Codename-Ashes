using UnityEngine;

public class Barang : MonoBehaviour
{
    [Header("Item Settings")]
    public string id;           // ID unik item 
    public string itemName;     // Nama item yang akan muncul di inventory
    

    // Method ini dipanggil saat item diambil
    public void OnPickedUp()
    {
        
        Destroy(gameObject); // Hancurkan objek setelah diambil

    }
}