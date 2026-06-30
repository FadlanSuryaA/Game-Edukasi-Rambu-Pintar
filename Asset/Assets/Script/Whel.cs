using UnityEngine;

public class Whel : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform wheelMesh;
    public bool wheelTurn;

    void Update()
    {
        // 1. Ambil posisi & rotasi dari WheelCollider
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);

        // 2. Apply ke mesh
        wheelMesh.position = pos;
        wheelMesh.rotation = rot;
    }
}