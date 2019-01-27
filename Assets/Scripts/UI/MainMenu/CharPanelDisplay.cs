using UnityEngine;

public class CharPanelDisplay : MonoBehaviour
{
    [SerializeField] private GameObject createRoomBtn;
    [SerializeField] private GameObject okBtn;

    private void OnDisable()
    {
        createRoomBtn.SetActive(false);
        okBtn.SetActive(false);
    }
}
