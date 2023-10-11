using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Button interactBtn;
    [SerializeField] private Image reticle;
    [SerializeField] private Color onTargetColor;
    [SerializeField] private Color offTargetColor;
    void Awake()
    {
        if (Instance != null && this != Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
   

    public void HideInteractButton()
    {
        interactBtn.gameObject.SetActive(false);
    }

    public void ShowInteractButton()
    {
        interactBtn.gameObject.SetActive(true);
    }

    public void SetReticleColor(Color targetColor)
    {
        reticle.color = targetColor;
    }

    public void OnInteractionEnter()
    {
        ShowInteractButton();

        SetReticleColor(onTargetColor);
        
    }

    public void OnInteractionExit()
    {
        HideInteractButton();
        SetReticleColor(offTargetColor);
       
    }
}
