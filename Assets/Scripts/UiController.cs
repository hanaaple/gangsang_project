using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text creditText;
    public Text calanderText;

    public Image timerSlide;

    public void UpdateCredit(string credit)
    {
        creditText.text = credit;
    }
    
    public void UpdateSlide(float timerPercentage)
    {
        timerSlide.fillAmount = timerPercentage;
    }
}
