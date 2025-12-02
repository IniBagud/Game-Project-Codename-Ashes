using System.Linq;
using UnityEngine;

public class FirstPersonAudio : MonoBehaviour
{
    public FirstPersonMovement character; // mengambil daya dari FirstPersonMovement
    public GroundCheck groundCheck; // mengambil daya dari GroundCheck

    // untuk memasukan audio source jalan
    [Header("Step")]
    public AudioSource stepAudio;
    public AudioSource runningAudio;
    [Tooltip("Minimum velocity for moving audio to play")]
    // kecepatan minimum agar audio bergerak dapat diputar
    public float velocityThreshold = 0.01f;
    Vector2 lastCharacterPosition;
    Vector2 CurrentCharacterPosition => new Vector2(character.transform.position.x, character.transform.position.z);
    // untuk memasukan audio source landing
    [Header("Landing")]
    public AudioSource landingAudio;
    public AudioClip[] landingSFX;
    // untuk memasukan audio source jump
    [Header("Jump")]
    public Jump jump;
    public AudioSource jumpAudio;
    public AudioClip[] jumpSFX;

    // array untuk menyimpan audio source yang bergerak
    AudioSource[] MovingAudios => new AudioSource[] { stepAudio, runningAudio, };

    // untuk mereset komponen saat ditambahkan
    void Reset()
    {
        // membuat otomatis referensi ke komponen audio
        character = GetComponentInParent<FirstPersonMovement>();
        groundCheck = (transform.parent ?? transform).GetComponentInChildren<GroundCheck>();
        stepAudio = GetOrCreateAudioSource("Step Audio");
        runningAudio = GetOrCreateAudioSource("Running Audio");
        landingAudio = GetOrCreateAudioSource("Landing Audio");

        // Setup untuk jump audio
        jump = GetComponentInParent<Jump>();
        if (jump)
        {
            jumpAudio = GetOrCreateAudioSource("Jump audio");
        }
       
    }

    void OnEnable() => SubscribeToEvents();

    void OnDisable() => UnsubscribeToEvents();

    void FixedUpdate()
    {
        // Jalankan audio jika player bergerak dan berada di tanah.
        float velocity = Vector3.Distance(CurrentCharacterPosition, lastCharacterPosition);
        if (velocity >= velocityThreshold && groundCheck && groundCheck.isGrounded)
        {
           
            if (character.IsRunning)
            {
                SetPlayingMovingAudio(runningAudio);
            }
            else
            {
                SetPlayingMovingAudio(stepAudio);
            }
        }
        else
        {
            SetPlayingMovingAudio(null);
        }

        // untuk menginupdate posisi karakter terakhir
        lastCharacterPosition = CurrentCharacterPosition;
    }


    // logika untuk mengatur audio bergerak, hanya satu audio bergerak yang dimainkan pada satu waktu, jika lari maka audio jalan di pause jika berhenti maka audio lari dan jalan di pause
    void SetPlayingMovingAudio(AudioSource audioToPlay)
    {
        // 1.Matikan(Pause) semua audio kecuali yang akan dimainkan
        // Menggunakan LINQ (.Where) untuk memfilter list
        foreach (var audio in MovingAudios.Where(audio => audio != audioToPlay && audio != null))
        {
            audio.Pause();
        }

        // matikan audio yang sedang dimainkan jika tidak dimainkan
        if (audioToPlay && !audioToPlay.isPlaying)
        {
            audioToPlay.Play();
        }
    }

    #region Play instant-related audios.
    // / Memainkan audio landing dan jump secara acak dari array clip
    void PlayLandingAudio() => PlayRandomClip(landingAudio, landingSFX);
    void PlayJumpAudio() => PlayRandomClip(jumpAudio, jumpSFX);

    #endregion

    #region Subscribe/unsubscribe to events.

    // logika untuk subscribe dan unsubscribe ke event dari groundcheck dan jump agar dapat memainkan audio dengan benar
    void SubscribeToEvents()
    {
        // mainkan PlayLandingAudio ketika Grounded.
        groundCheck.Grounded += PlayLandingAudio;

        // mainkan PlayJumpAudio ketika Jumped.
        if (jump)
        {
            jump.Jumped += PlayJumpAudio;
        }

        
        
    }

    void UnsubscribeToEvents()
    {
        // Undo PlayLandingAudio when Grounded.
        groundCheck.Grounded -= PlayLandingAudio;

        // Undo PlayJumpAudio when Jumped.
        if (jump)
        {
            jump.Jumped -= PlayJumpAudio;
        }

        
        
    }
    #endregion

    #region Utility.
    /// <summary>
    /// Get an existing AudioSource from a name or create one if it was not found.
    /// </summary>
    /// <param name="name">Name of the AudioSource to search for.</param>
    /// <returns>The created AudioSource.</returns>
    AudioSource GetOrCreateAudioSource(string name)
    {
        // Try to get the audiosource.
        AudioSource result = System.Array.Find(GetComponentsInChildren<AudioSource>(), a => a.name == name);
        if (result)
            return result;

        // Audiosource does not exist, create it.
        result = new GameObject(name).AddComponent<AudioSource>();
        result.spatialBlend = 1;
        result.playOnAwake = false;
        result.transform.SetParent(transform, false);
        return result;
    }

    static void PlayRandomClip(AudioSource audio, AudioClip[] clips)
    {
        if (!audio || clips.Length <= 0)
            return;

        // Get a random clip. If possible, make sure that it's not the same as the clip that is already on the audiosource.
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        if (clips.Length > 1)
            while (clip == audio.clip)
                clip = clips[Random.Range(0, clips.Length)];

        // Play the clip.
        audio.clip = clip;
        audio.Play();
    }
    #endregion 
}
