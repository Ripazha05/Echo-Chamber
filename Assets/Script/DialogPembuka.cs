using UnityEngine;
using TMPro;

public class DialogPembuka : MonoBehaviour
{
    [Header("Komponen UI")]
    public GameObject panelDialog;
    public TextMeshProUGUI teksDialog;
    public GameObject panelQuest;

    [Header("Isi Dialog")]
    [TextArea(2, 5)]
    public string[] daftarDialog;

    private int indeksDialog = 0;

    void Start()
    {
        panelDialog.SetActive(true);
        panelQuest.SetActive(false);
        TampilkanKalimat();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            LanjutDialog();
        }
    }

    void TampilkanKalimat()
    {
        if (daftarDialog.Length > 0)
        {
            teksDialog.text = daftarDialog[indeksDialog];
        }
    }

    void LanjutDialog()
    {
        indeksDialog++;

        if (indeksDialog < daftarDialog.Length)
        {
            TampilkanKalimat();
        }
        else
        {
            panelDialog.SetActive(false);
            panelQuest.SetActive(true); 
        }
    }
}