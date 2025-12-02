using UnityEngine;
using UnityEngine.UI; 

public class CrosshairManager : MonoBehaviour
{
    [Header("UI Settings")]
    public Image crosshairImage;    // Slot untuk komponen Image UI Crosshair
    public Sprite defaultSprite;    // ui 1
    public Sprite interactSprite;   // ui 2
    // sejauh mana raycast mendeteksi objek dan apakah objek tersebut memiliki tag tertentu
    [Header("Raycast Settings")]
    public float detectDistance = 3f; 
    public string targetTag = "Item"; 

    private Camera mainCam;
    // mengambil camera utaman dan ui 1 sebagai default
    void Start()
    {
        mainCam = Camera.main; 
        crosshairImage.sprite = defaultSprite;
    }
    // setiap frame, cek interaksi
    void Update()
    {
        CheckInteraction();
    }

    void CheckInteraction()
    {
        // Buat ray di tengah layar lalu simpan hasil tabrakannya
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);
        Ray ray = mainCam.ViewportPointToRay(screenCenter);
        RaycastHit hit;

        // untuk mendeteksi tabrakan
        if (Physics.Raycast(ray, out hit, detectDistance))
        {
            //  Cek apakah benda yang tertabrak punya Tag "Item"
            if (hit.collider.CompareTag(targetTag))
            {
                // jika menabrak benda dengan tag "Item", ganti sprite crosshair
                ChangeCrosshair(interactSprite);
            }
            else
            {
                // Jika tidak menabrak benda dengan tag "Item", balik ke default
                ChangeCrosshair(defaultSprite);
            }
        }
        else
        {
            // Jika tidak nabrak apapun (lihat langit), balik ke default
            ChangeCrosshair(defaultSprite);
        }
    }

    // Fungsi untuk mengubah sprite agar tidak redundan/berulang
    void ChangeCrosshair(Sprite newSprite)
    {
        if (crosshairImage.sprite != newSprite)
        {
            crosshairImage.sprite = newSprite;
        }
    }
}