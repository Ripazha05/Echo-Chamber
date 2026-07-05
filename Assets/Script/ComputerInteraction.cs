using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    [Header("UI Komputer")]
    public GameObject panelUIKomputer;

    [Header("Scene Transition")]
    public SceneTransitionDialog sceneTransition;

    // Flag langsung di script ini, di-set true dari tombol Salin
    [HideInInspector] public bool isPuzzleSolved = false;

    private bool playerDekatMeja = false;

    void Start()
    {
        if (panelUIKomputer != null)
            panelUIKomputer.SetActive(false);
    }

    void Update()
    {
        if (playerDekatMeja && Input.GetKeyDown(KeyCode.E))
        {
            if (panelUIKomputer != null && !panelUIKomputer.activeSelf)
                BukaLayarKomputer();
        }
    }

    void BukaLayarKomputer()
    {
        if (panelUIKomputer != null)
            panelUIKomputer.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

   public void TutupLayarKomputer()
{
    if (panelUIKomputer != null)
        panelUIKomputer.SetActive(false);

    // Langsung tampilkan dialog tanpa cek puzzle
    if (sceneTransition != null)
        sceneTransition.ShowTransitionDialog();
    else
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

    // Panggil fungsi ini dari tombol Salin/Ambil Bukti
    public void SetPuzzleSolved()
    {
        isPuzzleSolved = true;
        Debug.Log("Bukti diamankan! isPuzzleSolved = true");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDekatMeja = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDekatMeja = false;
            TutupLayarKomputer();
        }
    }

    // Tambahkan fungsi ini di ComputerInteraction.cs
public void TriggerInteractMobile()
{
    if (playerDekatMeja)
    {
        if (panelUIKomputer != null && !panelUIKomputer.activeSelf)
            BukaLayarKomputer();
    }
}
}