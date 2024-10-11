using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownDisplay;
    [SerializeField] private CarController carController;
    [SerializeField] private int countdownTime;

    [Header("Buttons")]
    [SerializeField] private Button gasButton;
    [SerializeField] private Button brakeButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    private IEnumerator CountdownToStart()
    {
        carController.SetMoveInactive();
        SetButtonsInteractable(false);

        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1.0f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1.0f);
        countdownDisplay.gameObject.SetActive(false);
        carController.SetMoveActive();

        SetButtonsInteractable(true);
    }

    private void SetButtonsInteractable(bool interactable)
    {
        gasButton.interactable = interactable;
        brakeButton.interactable = interactable;
        leftButton.interactable= interactable;
        rightButton.interactable= interactable;
    }
}
