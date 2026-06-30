using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SoalRambu
{
    public string idRambu;
    public string namaRambu;
    public string pertanyaan;
    public string jawabanBenar;
    public string pilihanA;
    public string pilihanB;
    public string pilihanC;
    public string pilihanD; // Tambahan Pilihan D
}

public class RambuPintarManager : MonoBehaviour
{
    private string sheetCSVUrl = "https://docs.google.com/spreadsheets/d/1UK8YsG_gWbWXaH7W2j87mi455X0-pzxcyrfl1EvWzrM/export?format=csv";
    
    public List<SoalRambu> semuaSoal = new List<SoalRambu>();

    void Start()
    {
        StartCoroutine(AmbilDataSoal());
    }

    IEnumerator AmbilDataSoal()
    {
        UnityWebRequest www = UnityWebRequest.Get(sheetCSVUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Gagal mengambil data soal: " + www.error);
        }
        else
        {
            ParseCSV(www.downloadHandler.text);
            Debug.Log("Total bank soal berhasil dimuat: " + semuaSoal.Count);
        }
    }

    void ParseCSV(string csvData)
    {
        string[] baris = csvData.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1; i < baris.Length; i++) // Mulai dari 1 untuk melewati Header
        {
            if (string.IsNullOrWhiteSpace(baris[i])) continue;

            string[] kolom = baris[i].Split(',');

            // UBAH DISINI: Sekarang butuh minimal 8 kolom
            if (kolom.Length >= 8) 
            {
                SoalRambu soalBaru = new SoalRambu
                {
                    idRambu = kolom[0].Trim(),
                    namaRambu = kolom[1].Trim(),
                    pertanyaan = kolom[2].Trim(),
                    jawabanBenar = kolom[3].Trim(),
                    pilihanA = kolom[4].Trim(),
                    pilihanB = kolom[5].Trim(),
                    pilihanC = kolom[6].Trim(),
                    pilihanD = kolom[7].Trim() // Menangkap data kolom D
                };
                semuaSoal.Add(soalBaru);
            }
        }
    }

    public SoalRambu AmbilSatuSoalAcak(string targetIdRambu)
    {
        List<SoalRambu> soalTersedia = semuaSoal.Where(s => s.idRambu == targetIdRambu).ToList();

        if (soalTersedia.Count > 0)
        {
            int indexAcak = Random.Range(0, soalTersedia.Count);
            return soalTersedia[indexAcak];
        }
        
        Debug.LogWarning("Soal untuk " + targetIdRambu + " tidak ditemukan di Google Sheet!");
        return null; 
    }
}