using UnityEngine;

[ExecuteInEditMode]
public class GroundCheck : MonoBehaviour
{
    // mmeberikan maksimum jarak toleransi dari tanah
    [Tooltip("Maximum distance from the ground.")]
    public float distanceThreshold = 0.15f;
    
    [Tooltip("Whether this transform is grounded now.")]
    public bool isGrounded = true;
    
    public event System.Action Grounded;
    // solusi dari masalah raycast menabrak/menembus tanah. dengan menaikan sedikit origin raycast ke atas
    const float OriginOffset = .001f;
    Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;
    float RaycastDistance => distanceThreshold + OriginOffset;

    // gunakan LateUpdate agar pengecekan tanah terjadi setelah semua pergerakan selesai (perframe)
    void LateUpdate()
    {
        // mengecek apakah kita menyentuh tanah
        bool isGroundedNow = Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold * 2);

        // logika untuk memanggil event (jika frame sebelumnya objek belum menyentuh tanah namun setelahnya menyentuh tanah) maka panggil event Grounded
        if (isGroundedNow && !isGrounded)
        {
            Grounded?.Invoke();
        }

        // Update isGrounded.
        isGrounded = isGroundedNow;
    }

    // hanya untuk visualisasi warna gizmos di editor jika objek tidak ditanah atau ditanah
    void OnDrawGizmosSelected()
    {
        Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.down * RaycastDistance, isGrounded ? Color.white : Color.red);
    }
}
