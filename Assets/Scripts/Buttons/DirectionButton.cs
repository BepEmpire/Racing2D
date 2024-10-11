using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CarController carController;
    [SerializeField] private float direction = 1.0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        carController.ChangeDirection(direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        carController.ChangeDirection(0);
    }
}
