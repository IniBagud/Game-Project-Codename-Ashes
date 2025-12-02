using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public float interactRange = 3f; // Jarak maksimal interaksi pintu
    public LayerMask interactLayer; // Layer khusus benda interaktif (opsional)

    private Camera mainCam; // Referensi ke kamera utama

    void Start()
    {
        mainCam = Camera.main;
    }
    void Update()
    {
        // Jika tombol E ditekan
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformRaycast();
        }
    }

    void PerformRaycast()
    {
        // Membuat Ray (sinar transparan) dari tengah kamera ke depan
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            // Cek apakah benda yang ditembak punya script DoorController
            DoorControl door = hit.collider.GetComponent<DoorControl>();
            // Jika ada, panggil method untuk membuka pintu
            if (door != null)
            {
                door.TryOpenDoor();
            }
        }
    }
}