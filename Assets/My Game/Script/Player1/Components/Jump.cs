using UnityEngine;

public class Jump : MonoBehaviour
{
    // mengatur lompatan karakter
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;
    // untuk mengecek apakah karakter berada di tanah
    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        // mendapatkan groundCheck 
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // mendapatkan rigidbody
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // mengatur agar karakter bisa lompat jika menekan tombol spasi dan berada di tanah
        if (Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded))
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
