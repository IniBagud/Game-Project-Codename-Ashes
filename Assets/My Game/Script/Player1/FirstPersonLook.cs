using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
        // dapatkan referensi karakter dari parent
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // kunci kursor di tengah layar dan sembunyikan
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // agar dapatkan delta mouse, haluskan, dan tambahkan ke velocity
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // untuk merotasi kamera dan karakter berdasarkan velocity
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}
