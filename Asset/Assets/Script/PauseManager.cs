using UnityEngine;
using UnityEngine.SceneManagement;
// Pastikan namespace ini ada untuk mendukung Input System baru
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("Panel Menu Pause yang akan muncul")]
    public GameObject pausePanel;
    
    [Tooltip("Daftar UI yang ingin disembunyikan saat pause (seperti tombol gas/rem/score)")]
    public GameObject[] uiToHide;

    [Header("Scene Settings")]
    [Tooltip("Nama scene untuk menu pemilihan level")]
    public string menuStageSceneName = "Stage"; 

    private bool isPaused = false;

    void Update()
    {
        // Perbaikan Error: Menggunakan Input System baru secara total
        // Menghapus semua referensi ke 'Input.GetKeyDown'
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // --- FUNGSI UTAMA ---

    public void PauseGame()
    {
        isPaused = true;
        if (pausePanel != null) pausePanel.SetActive(true);
        
        ToggleGameplayUI(false);
        Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false);
        
        ToggleGameplayUI(true);
        Time.timeScale = 1f; 
    }

    public void RestartGame()
    {
        // Pastikan waktu kembali normal sebelum reload scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   public void ExitToStageMenu()
{
    Debug.Log("Mencoba kembali ke Stage Menu: " + menuStageSceneName);
    
    // NYALAKAN KEMBALI WAKTU GAME SEBELUM LOAD SCENE
    // Jika tetap 0f, scene baru bisa ikut membeku saat loading selesai
    Time.timeScale = 1f; 
    
    if (!string.IsNullOrEmpty(menuStageSceneName))
    {
        // Gunakan mode standar untuk memastikan perpindahan scene bersih
        SceneManager.LoadScene(menuStageSceneName, LoadSceneMode.Single);
    }
    else
    {
        Debug.LogError("Nama scene menu stage belum diisi di Inspector!");
    }
}

    // --- HELPER ---

    private void ToggleGameplayUI(bool show)
    {
        if (uiToHide == null) return;

        foreach (GameObject ui in uiToHide)
        {
            if (ui != null) ui.SetActive(show);
        }
    }
}