using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrakeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CarController carController;
    
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            carController.DecreaseSpeed();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            carController.ReturnNormalSpeed();
        }
    }
}
