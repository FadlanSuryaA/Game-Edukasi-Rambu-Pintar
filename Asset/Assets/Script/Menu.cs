using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Menu : MonoBehaviour
{
    public GameObject menupanel;
    public GameObject menuperpus;
    public GameObject panelmateri1;
    public GameObject panelmateri2;
    public GameObject panelmateri3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menupanel.SetActive(true);
        menuperpus.SetActive(false);
                // PANEL MATERI OFF
        panelmateri1.SetActive(false);
        panelmateri2.SetActive(false);
        panelmateri3.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton(String scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void Codex()
    {
        menupanel.SetActive(false);
        menuperpus.SetActive(true);
    }
    public void RambuLarangan()
     {

        menuperpus.SetActive(false);
        panelmateri1.SetActive(true);
   
     }
       public void RambuPeringatan()
     {

        menuperpus.SetActive(false);
        panelmateri2.SetActive(true);
   
     }
       public void RambuPerintah()
     {

        menuperpus.SetActive(false);
        panelmateri3.SetActive(true);
   
     }
    public void kembali()
    {
        menupanel.SetActive(true);
        menuperpus.SetActive(false);
    }
    public void kembali1()
    {
        menuperpus.SetActive(true);
        panelmateri1.SetActive(false);
    }
    public void kembali2()
    {
        menuperpus.SetActive(true);
        panelmateri2.SetActive(false);
    }
    public void kembali3()
    {
        menuperpus.SetActive(true);
        panelmateri3.SetActive(false);
    }
    
    public void quit()
    {
        Application.Quit();
    }
}
