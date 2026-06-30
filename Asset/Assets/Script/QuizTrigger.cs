using UnityEngine;
using TMPro; 
using UnityEngine.UI; 
using UnityEngine.InputSystem; 

public class QuizTrigger : MonoBehaviour
{
    [Header("Identitas Rambu Ini")]
    public string idRambu; 

    [Header("Pengaturan UI Panel")]
    public GameObject quizPanel;
    
    [Header("Referensi Teks Kuis (Sambungkan ke UI Text)")]
    public TextMeshProUGUI textPertanyaan;
    public TextMeshProUGUI textPilihanA;
    public TextMeshProUGUI textPilihanB;
    public TextMeshProUGUI textPilihanC;
    public TextMeshProUGUI textPilihanD; 
    
    private SoalRambu soalSaatIni;

    [Header("Referensi Manager")]
    public QuizNotifManager notifManager; 
    public RambuPintarManager bankSoalManager; 

    [Header("Pengaturan Skor")]
    public static int globalScore = 0; 
    public TextMeshProUGUI scoreText; 

    // ==========================================
    // MENGGUNAKAN AUDIOCLIP AGAR BISA AMBIL LANGSUNG DARI ASET
    // ==========================================
    [Header("Pengaturan Suara (SFX)")]
    public AudioClip sfxBenar;
    public AudioClip sfxSalah;

    [Header("Pengaturan Auto Close")]
    public float autoCloseDelay = 0.5f; 
    private bool isShowingNotif = false;
    private float notifTimer = 0f;

    private void Start()
    {
        globalScore = 0;
        UpdateScoreUI();
    }

    private void Update()
    {
        if (isShowingNotif)
        {
            notifTimer += Time.unscaledDeltaTime;

            bool isScreenTapped = false;
            if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
            {
                isScreenTapped = true;
            }

            if (notifTimer >= autoCloseDelay || isScreenTapped)
            {
                CloseNotifSequence();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (RambuProgress.instance != null) RambuProgress.instance.TambahProgress();

            if (bankSoalManager != null)
            {
                soalSaatIni = bankSoalManager.AmbilSatuSoalAcak(idRambu);
                
                if(soalSaatIni != null)
                {
                    textPertanyaan.text = soalSaatIni.pertanyaan;
                    textPilihanA.text = "A. " + soalSaatIni.pilihanA;
                    textPilihanB.text = "B. " + soalSaatIni.pilihanB;
                    textPilihanC.text = "C. " + soalSaatIni.pilihanC;
                    textPilihanD.text = "D. " + soalSaatIni.pilihanD; 
                }
            }

            if (quizPanel != null) quizPanel.SetActive(true);
            
            SuaraMesinMobil suaraMesin = other.GetComponent<SuaraMesinMobil>();
            if (suaraMesin != null) suaraMesin.SenyapkanMesin(true); 

            Time.timeScale = 0f;

            BoxCollider col = GetComponent<BoxCollider>();
            if (col != null) col.enabled = false;
        }
    }

    public void TekanTombolA() { if (soalSaatIni != null) CekJawaban(soalSaatIni.pilihanA); }
    public void TekanTombolB() { if (soalSaatIni != null) CekJawaban(soalSaatIni.pilihanB); }
    public void TekanTombolC() { if (soalSaatIni != null) CekJawaban(soalSaatIni.pilihanC); }
    public void TekanTombolD() { if (soalSaatIni != null) CekJawaban(soalSaatIni.pilihanD); } 

    // ==========================================
    // SISTEM CEK JAWABAN ANTI-ERROR (KEBAL SPASI & HURUF KECIL/BESAR)
    // ==========================================
    private void CekJawaban(string jawabanYangDitekan)
    {
        Debug.Log("Pemain memilih: [" + jawabanYangDitekan + "]");
        Debug.Log("Kunci Jawaban dari Sheet: [" + soalSaatIni.jawabanBenar + "]");

        string teksDitekan = jawabanYangDitekan.Trim().ToLower();
        string teksKunci = soalSaatIni.jawabanBenar.Trim().ToLower();

        if (teksDitekan == teksKunci)
        {
            JawabanBenar();
        }
        else
        {
            JawabanSalah();
        }
    }

    private void JawabanBenar()
    {
        globalScore += 10;
        UpdateScoreUI(); 

        // MEMBUNYIKAN SUARA BENAR LANGSUNG DARI ASET
        if (sfxBenar != null && Camera.main != null) 
        {
            AudioSource.PlayClipAtPoint(sfxBenar, Camera.main.transform.position);
        }

        if (notifManager != null)
        {
            notifManager.ShowCorrect();
            StartNotifTimer();
        }
        else TutupPanelDanLanjut();
    }

    private void JawabanSalah()
    {
        // MEMBUNYIKAN SUARA SALAH LANGSUNG DARI ASET
        if (sfxSalah != null && Camera.main != null) 
        {
            AudioSource.PlayClipAtPoint(sfxSalah, Camera.main.transform.position);
        }

        if (notifManager != null)
        {
            notifManager.ShowWrong();
            StartNotifTimer();
        }
        else TutupPanelDanLanjut();
    }

    private void StartNotifTimer()
    {
        isShowingNotif = true;
        notifTimer = 0f;
    }

    private void CloseNotifSequence()
    {
        isShowingNotif = false;
        TutupPanelDanLanjut();
        
        if (notifManager != null) notifManager.CloseAndResume();
    }

    private void TutupPanelDanLanjut()
    {
        if (quizPanel != null) quizPanel.SetActive(false);
        HidupkanSuaraMesin();
        Time.timeScale = 1f;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = globalScore.ToString();
    }

    private void HidupkanSuaraMesin()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            SuaraMesinMobil suaraMesin = playerObj.GetComponent<SuaraMesinMobil>();
            if (suaraMesin != null) suaraMesin.SenyapkanMesin(false); 
        }
    }
}