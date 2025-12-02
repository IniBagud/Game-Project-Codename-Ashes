using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JumpscareManager : MonoBehaviour
{
    [Header("Observer Settings")]
    [Tooltip("Drag objek yang memiliki script ZoneTrigger atau ItemTrigger ke sini")]
    public JumpscareSubject subjectToObserve;

    [Header("Jumpscare Effects")]
    public GameObject objectToSpawn; // Prefab hantu/monster
    public Transform spawnLocation;  // Titik munculnya hantu
    public AudioClip scareSound;     // Suara kaget

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    

    private void OnEnable()
    {
        // posisi standby dan mulai mendengarkan event dari subject
        if (subjectToObserve != null)
        {
            subjectToObserve.OnJumpscareTriggered += ExecuteJumpscare;
        }
    }

    private void OnDisable()
    {
        // posisi tidak aktif dan berhenti mendengarkan event dari subject
        if (subjectToObserve != null)
        {
            subjectToObserve.OnJumpscareTriggered -= ExecuteJumpscare;
        }
    }

    // Fungsi yang dijalankan saat Trigger aktif
    void ExecuteJumpscare()
    {
        // 1. Mainkan Suara
        if (scareSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scareSound);
        }

        // 2. Spawn Objek
        if (objectToSpawn != null && spawnLocation != null)
        {
            Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
        }
       

        Debug.Log("JUMPSCARE BERHASIL DIAKTIFKAN!");
    }
}