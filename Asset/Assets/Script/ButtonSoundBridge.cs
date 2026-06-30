using UnityEngine;

public class ButtonSoundBridge : MonoBehaviour
{
    // Fungsi ini yang akan dipanggil oleh tombol UI
    public void JalankanSuaraKlik()
    {
        // Mencari AudioManager yang aktif di dalam game secara otomatis
        // Diperbarui ke FindFirstObjectByType agar pas dengan Unity 6 kamu
        AudioManager managerAktif = FindFirstObjectByType<AudioManager>();

        if (managerAktif != null)
        {
            managerAktif.PutarSuaraKlik(); // Panggil fungsi suara klik dari AudioManager yang selamat
        }
        else
        {
            Debug.LogWarning("AudioManager tidak ditemukan di scene ini!");
        }
    }
}