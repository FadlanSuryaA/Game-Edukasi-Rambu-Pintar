using UnityEngine;

public class QuizNotifManager : MonoBehaviour
{
    [Header("Panel Notifikasi")]
    public GameObject correctNotifPanel; 
    public GameObject wrongNotifPanel;

    [Header("Animator Pop")]
    public Animator correctAnimator;
    public Animator wrongAnimator;

    private void Start()
    {
        // Pastikan semua notif mati saat awal
        if (correctNotifPanel != null)
            correctNotifPanel.SetActive(false);

        if (wrongNotifPanel != null)
            wrongNotifPanel.SetActive(false);
    }

    // Fungsi khusus munculkan notif benar
    public void ShowCorrect()
    {
        if (correctNotifPanel != null)
        {
            correctNotifPanel.SetActive(true);

            // Mainkan animasi pop dari awal
            if (correctAnimator != null)
            {
                // DISAMAKAN: Mengubah "PopBenar" menjadi "PopUp" jika nama statusnya sama
                correctAnimator.Play("PopNotif", 0, 0f);
            }
        }
    }

    // Fungsi khusus munculkan notif salah
    public void ShowWrong()
    {
        if (wrongNotifPanel != null)
        {
            wrongNotifPanel.SetActive(true);

            // Mainkan animasi pop dari awal
            if (wrongAnimator != null)
            {
                // PERBAIKAN: Mengubah "PopSalah" menjadi "PopUp" sesuai jendela Animator kamu
                wrongAnimator.Play("PopNotif", 0, 0f);
            }
        }
    }

    // Fungsi untuk tombol OK / Lanjut
    public void CloseAndResume()
    {
        if (correctNotifPanel != null)
            correctNotifPanel.SetActive(false);

        if (wrongNotifPanel != null)
            wrongNotifPanel.SetActive(false);

        Time.timeScale = 1f; // Jalankan game kembali
    }
}