using UnityEngine;

public class MobileInteractButton : MonoBehaviour
{
    private DoorInteraction doorInteraction;
    private ComputerInteraction computerInteraction;

    void Update()
    {
        // Cari script interaksi yang aktif di sekitar player
        doorInteraction = FindObjectOfType<DoorInteraction>();
        computerInteraction = FindObjectOfType<ComputerInteraction>();
    }

    // Panggil dari OnClick() tombol E di UI
    public void OnInteractButtonClick()
    {
        // Coba interaksi pintu dulu
        if (doorInteraction != null)
        {
            doorInteraction.TriggerInteractMobile();
            return;
        }

        // Kalau tidak ada pintu, coba interaksi komputer
        if (computerInteraction != null)
        {
            computerInteraction.TriggerInteractMobile();
        }
    }
}