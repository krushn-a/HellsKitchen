using UnityEngine;

public class ClearCounterSelectedVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedVisualArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter != baseCounter)
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
        foreach (GameObject selectedVisual in selectedVisualArray)
        {
            selectedVisual.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject selectedVisual in selectedVisualArray)
        {
            selectedVisual.SetActive(false);
        }
    }
}
