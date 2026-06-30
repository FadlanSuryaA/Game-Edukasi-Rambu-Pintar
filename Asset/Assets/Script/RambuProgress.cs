using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RambuProgress : MonoBehaviour
{
    public static RambuProgress instance;

    [Header("Progress Rambu")]
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    public int totalRambu = 9; 
    private int currentRambu = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("RambuProgress Berhasil Diinisialisasi di Awake!");
        }
    }

    private void Start()
    {
        currentRambu = 0;
        UpdateProgressUI();
    }

    public void TambahProgress()
    {
        // LOG PELACAK 1: Memastikan fungsi ini terpanggil
        Debug.Log("Fungsi TambahProgress() di RambuProgress DIPANGGIL!");

        if (currentRambu < totalRambu)
        {
            currentRambu++;
            
            // LOG PELACAK 2: Melihat angka perubahan rambu
            Debug.Log("Current Rambu bertambah menjadi: " + currentRambu);
            
            UpdateProgressUI();
        }
    }

    private void UpdateProgressUI()
    {
        if (progressBar != null)
        {
            progressBar.maxValue = totalRambu;
            progressBar.value = currentRambu;
        }

        if (progressText != null)
        {
            progressText.text = currentRambu + " / " + totalRambu;
        }
    }
}