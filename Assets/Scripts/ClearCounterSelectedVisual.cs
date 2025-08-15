using UnityEngine;

public class ClearCounterSelectedVisual : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedVisual;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter != clearCounter)
        {
            Hide();
        }
        else 
        {
            Show();
        }
    }

    private void Show() 
    {
        selectedVisual.SetActive(true);
    }

    private void Hide()
    {
        selectedVisual.SetActive(false);
    }
}
