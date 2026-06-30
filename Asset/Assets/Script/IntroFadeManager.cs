using UnityEngine;
using System.Collections;

public class IntroFadeManager : MonoBehaviour
{
    [Header("Grup UI Utama")]
    public CanvasGroup panelIntroUtama;
    public GameObject panelKontrolMobil;

    [Header("Gambar Tulisan")]
    public GameObject gambarStage;
    public GameObject gambarMulai;

    [Header("Pengaturan Waktu")]
    public float waktuTampilStage = 1.5f;
    public float kecepatanPudar = 1.5f;
    public float waktuTampilMulai = 1.0f;

    void Start()
    {
        StartCoroutine(MulaiIntro());
    }

    IEnumerator MulaiIntro()
    {
        // 1. Persiapan
        Time.timeScale = 0f;
        panelKontrolMobil.SetActive(false);
        panelIntroUtama.gameObject.SetActive(true);
        panelIntroUtama.alpha = 1f;
        gambarStage.SetActive(true);
        gambarMulai.SetActive(false);

        yield return new WaitForSecondsRealtime(waktuTampilStage);

        // 2. Proses Pudar
        while (panelIntroUtama.alpha > 0)
        {
            panelIntroUtama.alpha -= kecepatanPudar * Time.unscaledDeltaTime;
            yield return null; 
        }

        // 3. MUNCULKAN & ANIMASI MULAI
        gambarStage.SetActive(false);
        gambarMulai.SetActive(true);
        
        // JALANKAN ANIMASI POP UP
        yield return StartCoroutine(AnimasiPopUp(gambarMulai.transform));

        yield return new WaitForSecondsRealtime(waktuTampilMulai);

        // 4. Selesai
        gambarMulai.SetActive(false);
        panelIntroUtama.gameObject.SetActive(false);
        panelKontrolMobil.SetActive(true);
        Time.timeScale = 1f;
    }

    // FUNGSI ANIMASI POP UP (TANPA ANIMATOR)
    IEnumerator AnimasiPopUp(Transform objek)
    {
        float durasi = 0.3f; // Kecepatan animasi
        float waktu = 0;
        
        // Mulai dari ukuran 0
        objek.localScale = Vector3.zero;

        while (waktu < durasi)
        {
            waktu += Time.unscaledDeltaTime;
            // Rumus membesar sampai 1.2 lalu kembali ke 1 (Efek memantul)
            float lerp = waktu / durasi;
            float scale = Mathf.Lerp(0, 1.2f, lerp); 
            objek.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        
        // Pastikan kembali ke ukuran normal (1)
        objek.localScale = Vector3.one;
    }
}

