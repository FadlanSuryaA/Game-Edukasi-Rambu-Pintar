using UnityEngine;
using UnityEngine.SceneManagement; 

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("--- Komponen Audio ---")]
    public AudioSource bgmSource; 
    public AudioSource sfxSource; 

    [Header("--- File Audio (Audio Clip) ---")]
    public AudioClip musikLatar;  
    public AudioClip suaraKlik;   

    private void Awake()
    {
        // Sistem keamanan agar AudioManager tidak dobel saat pindah-pindah scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void OnEnable()
    {
        // Mendaftarkan fungsi CekScene setiap kali scene baru selesai dimuat
        SceneManager.sceneLoaded += CekScene;
    }

    private void OnDisable()
    {
        // Membersihkan event agar tidak terjadi error memori
        SceneManager.sceneLoaded -= CekScene;
    }

    // ==========================================
    // INI BAGIAN PENTINGNYA
    // ==========================================
    private void CekScene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Pindah ke scene: " + scene.name);

        // 1. Jika masuk ke gameplay (Stage 1, Stage 2, ATAU Stage 3), MATIKAN musik
        // Tambahkan || scene.name == "Stage 3" di sini
        if (scene.name == "Stage 1" || scene.name == "Stage 2" || scene.name == "Stage 3")
        {
            if (bgmSource != null && bgmSource.isPlaying)
            {
                bgmSource.Stop(); 
            }
        }
        // 2. Jika kembali ke "Stage" atau "Main Menu", HIDUPKAN musik
        else if (scene.name == "Stage" || scene.name == "Main Menu")
        {
            if (musikLatar != null && bgmSource != null && !bgmSource.isPlaying)
            {
                bgmSource.clip = musikLatar;
                bgmSource.loop = true;
                bgmSource.Play(); 
            }
        }
    }

    private void Start()
    {
        // Putar musik pertama kali saat game baru dibuka
        if (musikLatar != null && bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.clip = musikLatar;
            bgmSource.loop = true;        
            bgmSource.volume = 0.4f;      
            bgmSource.Play();             
        }
    }

    public void PutarSuaraKlik()
    {
        if (suaraKlik != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(suaraKlik); 
        }
    }
}