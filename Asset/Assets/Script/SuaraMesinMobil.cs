using UnityEngine;

public class SuaraMesinMobil : MonoBehaviour
{
    [Header("Pengaturan Audio")]
    public AudioSource mesinAudioSource;
    
    [Tooltip("Pitch saat mobil diam (suara berat)")]
    public float pitchDiam = 0.5f;   
    
    [Tooltip("Pitch maksimal saat ngegas penuh")]
    public float pitchGas = 1.6f;    
    
    [Tooltip("Kecepatan transisi naik/turun suara.")]
    public float kecepatanTransisi = 5f; 

    private bool sedangNgegas = false;
    private bool isKuisAktif = false;
    private float volumeAwal = 1f;
    private bool isGameFinished = false; // [TAMBAHAN] Flag untuk mendeteksi game selesai

    void Start()
    {
        if (mesinAudioSource != null)
        {
            mesinAudioSource.pitch = pitchDiam;
            volumeAwal = mesinAudioSource.volume; 
            mesinAudioSource.Play();
        }
    }

    void Update()
    {
        // [TAMBAHAN] Jika game sudah finish atau AudioSource kosong, langsung hentikan fungsi Update
        if (isGameFinished || mesinAudioSource == null) return;

        float delta = Time.unscaledDeltaTime;

        if (isKuisAktif)
        {
            mesinAudioSource.volume = Mathf.Lerp(mesinAudioSource.volume, 0f, delta * 10f);
            mesinAudioSource.pitch = Mathf.Lerp(mesinAudioSource.pitch, pitchDiam, delta * kecepatanTransisi);
        }
        else
        {
            mesinAudioSource.volume = Mathf.Lerp(mesinAudioSource.volume, volumeAwal, delta * 5f);
            float targetPitch = sedangNgegas ? pitchGas : pitchDiam;
            mesinAudioSource.pitch = Mathf.Lerp(mesinAudioSource.pitch, targetPitch, delta * kecepatanTransisi);
        }
    }

    public void GasDitekan()
    {
        if (!isKuisAktif && !isGameFinished) sedangNgegas = true;
    }

    public void GasDilepas()
    {
        sedangNgegas = false;
    }

    public void SenyapkanMesin(bool senyap)
    {
        isKuisAktif = senyap;
        if (senyap)
        {
            sedangNgegas = false;
        }
    }

    // [TAMBAHAN] Fungsi khusus untuk dipanggil saat menyentuh Finish
    public void MatikanMesinTotal()
    {
        isGameFinished = true;
        sedangNgegas = false;
        if (mesinAudioSource != null)
        {
            mesinAudioSource.Stop(); // Matikan suara seketika
        }
        this.enabled = false; // Nonaktifkan script
    }
}