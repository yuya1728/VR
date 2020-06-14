using UnityEngine;

[RequireComponent(typeof(VRIKScaler))]
class VRIKCalibrator : MonoBehaviour
{
    [SerializeField, Range(0.5f, 2.5f)]
    float modelEyeHeight = 1.5f;
    [SerializeField]
    Transform hmd;

    VRIKScaler scaler;

    void Awake()
    {
        scaler = GetComponent<VRIKScaler>();
    }

    [ContextMenu("Calibrate")]
    public void Calibrate()
    {
        scaler.scale = hmd.position.y / modelEyeHeight;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.up * (modelEyeHeight / 2f), new Vector3(0.2f, modelEyeHeight, 0.2f));
    }
}