using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 3f; // Jarak maksimal ambil barang
    public LayerMask interactableLayer; // Layer khusus barang 
    public Camera playerCamera;         // Referensi kamera pemain

    

    private void Update()
    {
        CheckForItems();
    }

    void CheckForItems()
    {
        // Membuat Ray (sinar transparan) dari tengah kamera ke depan
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Tembakkan Raycast
        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            // Cek apakah objek yang kena raycast punya script Barang
            Barang item = hit.collider.GetComponent<Barang>();

            if (item != null)
            {
                

                // Input Player menekan E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUpItem(item);
                }
            }
           
        }
        
    }

    void PickUpItem(Barang item)
    {
        // 1. Panggil Singleton InventoryManager untuk simpan item
        InventoryManager.Instance.AddItem(item);

        // 2. Panggil method di item itu sendiri (untuk menghilangkannya dari dunia/game)
        item.OnPickedUp();
    }
}