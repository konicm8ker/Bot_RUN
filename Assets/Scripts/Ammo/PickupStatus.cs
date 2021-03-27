using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DentedPixel;

public class PickupStatus : MonoBehaviour
{
    [SerializeField] GameObject pickupStatus;
    [SerializeField] TextMeshProUGUI pickupStatusText;
    CanvasGroup pickupStatusCG;
    public bool showStatusText = false;
    public AmmoType ammoType;
    public int ammoAmount;
    public bool isHealth = false;
    string healthText;

    void Start()
    {
        healthText = "+25 Health";
        pickupStatusCG = pickupStatus.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(showStatusText)
        {
            if(isHealth)
            {
                isHealth = false;
                pickupStatusText.text = healthText;
            }
            else
            {
                pickupStatusText.text = "+" + ammoAmount.ToString() + " " + ammoType.ToString();
            }
            ShowStatusText();
        }
    }

    private void ShowStatusText()
    {
        showStatusText = false;
        LeanTween.alphaCanvas(pickupStatusCG, 1f, .25f);
        StartCoroutine(HideStatusText());
    }

    IEnumerator HideStatusText()
    {
        yield return new WaitForSeconds(2f);
        LeanTween.alphaCanvas(pickupStatusCG, 0f, .25f);
    }
}
