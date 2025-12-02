using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class InventoryManager : MonoBehaviour
{
    
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        // Jika Instance sudah ada dan bukan script ini, hancurkan script ini
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // List untuk menyimpan item yang diambil
    [Header("Data Inventori")]
    public List<Barang> inventoryItems = new List<Barang>();

    [Header("UI")]
    public GameObject inventoryPanel; // Panel UI Inventory (Background)
    public TMPro.TextMeshProUGUI inventoryText;        // Text UI untuk menampilkan daftar item 
    

    private bool isInventoryOpen = false;

    private void Start()
    {
        // Sembunyikan inventory saat game mulai
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        UpdateInventoryUI();
    }

    private void Update()
    {
        // Tekan 'I' atau 'Tab' untuk buka/tutup Inventory yang bertipe toggle
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    // Fungsi untuk menambah item ke list (Dipanggil oleh PlayerInteraction)
    public void AddItem(Barang item)
    {
        inventoryItems.Add(item);
        Debug.Log("Item ditambahkan: " + item.itemName);

        UpdateInventoryUI(); // Perbarui tampilan UI
    }

   

    // Buka/Tutup UI
    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (inventoryPanel != null)
            inventoryPanel.SetActive(isInventoryOpen);

        // Kunci/Buka kursor mouse
        if (isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

    // Memperbarui list text di UI
    void UpdateInventoryUI()
    {
        if (inventoryText == null) return;

        inventoryText.text = "Inventori :\n";

        foreach (Barang item in inventoryItems)
        {
            inventoryText.text += "- " + item.itemName + "\n";
        }
    }
    // untuk mengecek apakah inventori memiliki item tertentu berdasarkan nama
    public bool HasItem(string itemNameToCheck)
    {
        // Loop mencari apakah ada item dengan nama yang sesuai
        foreach (Barang item in inventoryItems)
        {
            if (item.itemName == itemNameToCheck)
            {
                return true; 
            }
        }
        return false; 
    }
}