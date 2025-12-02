using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndingManager : MonoBehaviour
{
    // untuk mengatur ending game dan duaraasi fade layar hitam
    [Header("UI Settings")]
    public CanvasGroup endingCanvasGroup;
    public float fadeDuration = 2.0f;
    // untuk mengatur fitur-fitur saat ending
    [Header("Feature Settings")]
    public GameObject crosshairUI;   // Crosshair (Image)
    public AudioClip endingSound;    // suara ending 
    public AudioSource audioSource;  // komponen AudioSource

    private bool isEnding = false;

    private void Start()
    {
        // memastikan layar ending di awal transparan
        if (endingCanvasGroup != null)
        {
            endingCanvasGroup.alpha = 0;
            endingCanvasGroup.interactable = false;
            endingCanvasGroup.blocksRaycasts = false;
        }

    }

    // jika sudah ending, tunggu input untuk reset game
    private void Update()
    {
        if (isEnding && endingCanvasGroup.alpha >= 1f)
        {
            if (Input.anyKeyDown)
            {
                ResetGame();
            }
        }
    }
    // mendeteksi tabrakan dengan player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEnding)
        {
            StartEndingSequence();
        }
    }
    // memulai urutan ending
    void StartEndingSequence()
    {
        isEnding = true;

        // 1. Matikan Crosshair
        if (crosshairUI != null)
        {
            crosshairUI.SetActive(false);
        }

        // 2. Mainkan Suara Ending
        if (audioSource != null && endingSound != null)
        {
            // mainkan suara ending satu kali
            audioSource.PlayOneShot(endingSound);
        }

        // 3. Hentikan waktu
        Time.timeScale = 0f;

        // matikan kursor mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 5. Mulai animasi Fade layar hitam
        StartCoroutine(FadeInEnding());
    }
    // animasi fade in layar hitam
    IEnumerator FadeInEnding()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            endingCanvasGroup.alpha = timer / fadeDuration;
            yield return null;
        }
        endingCanvasGroup.alpha = 1f;
    }
    // mereset game dengan memuat ulang scene
    void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}