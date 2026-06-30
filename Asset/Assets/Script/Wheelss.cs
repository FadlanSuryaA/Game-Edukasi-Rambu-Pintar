using UnityEngine;
using UnityEngine.InputSystem; // Wajib ditambahkan untuk Input System baru

public class Wheelss : MonoBehaviour
{
    public Rigidbody rigid;
    
    [Header("Pengaturan Roda")]
    // Asumsi: wheel1 & wheel2 adalah ban DEPAN, wheel3 & wheel4 adalah ban BELAKANG
    public WheelCollider wheel1, wheel2, wheel3, wheel4; 
    [Header("Pengaturan Tenaga & Kemudi")]
    public float drivespeed = 1500f; // Tenaga mesin (Torsi motor), butuh nilai besar karena pakai fisika
    public float streetspeed = 25f;  // Sudut maksimal belok roda depan (misal: 30 derajat)
    public float kehalusanSetir = 5f; // Semakin kecil, semakin lambat/halus setir berputar
    // Status Input UI (Tombol Mobile)
    private bool gasDitekanUI = false;
    private bool remDitekanUI = false;
    private float inputBelokUI = 0f;
    private float finalVerticalInput = 0f;
    private float belokanHalus = 0f;
void Start()
{
    // Mengubah pusat massa secara lokal di dalam Rigidbody
    // Nilai -0.5f artinya diturunkan ke bawah. Sesuaikan nilainya dengan mobilmu.
    rigid.centerOfMass = new Vector3(0, -0.5f, 0);
}
    void Update()
    {
        // 1. TANGKAP INPUT DARI KEYBOARD (PC)
        float inputMajuPC = 0f;
        float inputBelokPC = 0f;

        if (Keyboard.current != null)
        {
            // Input Maju / Mundur
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) 
                inputMajuPC = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) 
                inputMajuPC = -1f;

            // Input Kanan / Kiri
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) 
                inputBelokPC = 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) 
                inputBelokPC = -1f;
        }

        // 2. GABUNGKAN INPUT PC DAN MOBILE UI
        // Mobil akan merespon entah kamu menekan tombol layar atau tombol keyboard
        bool tekanMaju = gasDitekanUI || inputMajuPC > 0;
        bool tekanMundur = remDitekanUI || inputMajuPC < 0;
        float targetBelok = (inputBelokUI != 0) ? inputBelokUI : inputBelokPC;

        // 3. LOGIKA AUTO-MAJU
        if (tekanMaju)
{
    finalVerticalInput = 1f;
}
else if (tekanMundur)
{
    finalVerticalInput = -1f;
}
else
{
    finalVerticalInput = 0f;
}

        // 4. LOGIKA SETIR HALUS
        // Menggunakan Lerp agar roda berbelok dengan mulus layaknya setir sungguhan
        belokanHalus = Mathf.Lerp(belokanHalus, targetBelok, kehalusanSetir * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // FixedUpdate khusus untuk menjalankan fisika (WheelCollider)

        // Terapkan torsi penggerak ke semua 4 roda (4-Wheel Drive)
        float motor = finalVerticalInput * drivespeed; 
        wheel1.motorTorque = motor;
        wheel2.motorTorque = motor;
        wheel3.motorTorque = motor;
        wheel4.motorTorque = motor;
        
        // Terapkan sudut kemudi (Steer Angle) HANYA untuk ban depan (wheel1 & wheel2)
        float kemudi = streetspeed * belokanHalus;
        wheel1.steerAngle = kemudi;
        wheel2.steerAngle = kemudi;
    }

    // --- EVENT TRIGGERS UNTUK TOMBOL UI MOBILE ---
    public void TekanMaju() { gasDitekanUI = true; }
    public void LepasMaju() { gasDitekanUI = false; }

    public void TekanMundur() { remDitekanUI = true; }
    public void LepasMundur() { remDitekanUI = false; }

    public void TekanKanan() { inputBelokUI = 1f; }
    public void LepasKanan() { inputBelokUI = 0f; }

    public void TekanKiri() { inputBelokUI = -1f; }
    public void LepasKiri() { inputBelokUI = 0f; }
}