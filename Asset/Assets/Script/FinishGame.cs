using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FinishGame : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject finishPanel;
    public GameObject[] uiToHide;

    [Header("Pengaturan Skor Akhir")]
    public TextMeshProUGUI finalScoreText;

    [Header("Bintang")]
    public GameObject star1Full;
    public GameObject star2Full;
    public GameObject star3Full;

    [Header("Pengaturan Suara")]
    public AudioSource finishAudioSource; 
    public AudioSource backsoundAudioSource; // Bisa dibiarkan kosong jika sudah pakai auto-find di bawah

    [Header("Pengaturan Scene")]
    public string namaSceneHome = "Stage";
    public string namaSceneLanjut = "Stage 2";

    private bool isFinished = false; // Mencegah trigger ganda

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFinished)
        {
            isFinished = true;
            Debug.Log("Pemain mencapai Finish! Game dihentikan.");

            // ========================================================
            // 1. MATIKAN SUARA MOBIL & BACKSOUND SEPENUHNYA DI FINISH
            // ========================================================
            
            // --- MATIKAN BGM (Mencari objek BGMManager otomatis) ---
            GameObject bgmObject = GameObject.Find("BGMManager");
            if (bgmObject != null)
            {
                AudioSource bgmAudio = bgmObject.GetComponent<AudioSource>();
                if (bgmAudio != null)
                {
                    bgmAudio.Stop();
                }
            }
            // Fallback jika dimasukkan manual lewat Inspector
            else if (backsoundAudioSource != null)
            {
                backsoundAudioSource.Stop();
            }

            // --- MATIKAN SUARA MESIN MOBIL ---
            // Menggunakan GetComponentInParent agar script tetap ketemu 
            // meski Collider yang menyentuh finish ada di anak objek (child)
            SuaraMesinMobil suaraMesin = other.GetComponentInParent<SuaraMesinMobil>();
            if (suaraMesin != null)
            {
                if (suaraMesin.mesinAudioSource != null)
                {
                    suaraMesin.mesinAudioSource.Stop(); // Suara mati seketika
                }
                suaraMesin.enabled = false; // Mematikan script agar Update() berhenti
            }
            // ========================================================

            // 2. Mainkan suara finish SEBELUM Time.timeScale menjadi 0
            if (finishAudioSource != null)
            {
                finishAudioSource.Play();
            }

            // 3. Sembunyikan UI gameplay
            foreach (GameObject uiElement in uiToHide)
            {
                if (uiElement != null)
                {
                    uiElement.SetActive(false);
                }
            }

            // 4. Panel finish muncul langsung (Tanpa Animasi)
            if (finishPanel != null)
            {
                // Berjaga-jaga mengembalikan skala ke ukuran normal (1,1,1)
                finishPanel.transform.localScale = Vector3.one; 
                finishPanel.SetActive(true);
            }

            // 5. Jalankan animasi skor dan bintang
            int skor = QuizTrigger.globalScore;
            if (finalScoreText != null)
            {
                StartCoroutine(AnimateScore(skor));
            }

            // Pause game
            Time.timeScale = 0f;
        }
    }

    // =========================================
    // COROUTINE ANIMASI SKOR
    // =========================================
    IEnumerator AnimateScore(int targetScore)
    {
        int currentScore = 0;

        // Matikan semua bintang dulu di awal
        star1Full.SetActive(false);
        star2Full.SetActive(false);
        star3Full.SetActive(false);

        // Jika skor awal adalah 0, langsung tampilkan text 0
        finalScoreText.text = currentScore.ToString();

        while (currentScore < targetScore)
        {
            currentScore++;
            finalScoreText.text = currentScore.ToString();

            if (currentScore >= 10 && !star1Full.activeSelf) star1Full.SetActive(true);
            if (currentScore >= 50 && !star2Full.activeSelf) star2Full.SetActive(true);
            if (currentScore >= 80 && !star3Full.activeSelf) star3Full.SetActive(true);

            // Menggunakan WaitForSecondsRealtime karena timeScale = 0
            yield return new WaitForSecondsRealtime(0.01f); 
        }

        finalScoreText.text = targetScore.ToString();
    }

    // =========================
    // TOMBOL PANEL FINISH
    // =========================
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void KeMenuUtama()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(namaSceneHome);
    }

    public void LanjutLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(namaSceneLanjut);
    }
}