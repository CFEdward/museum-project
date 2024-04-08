using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    [SerializeField] private AttributesManager attributesManager;
    [SerializeField] private Image healthbar;

    private void Start()
    {
        attributesManager.OnHit += AttributesManager_OnHit;

        healthbar.fillAmount = 1f;
    }

    private void AttributesManager_OnHit(object sender, AttributesManager.OnHitHealthChangedEventArgs e)
    {
        healthbar.fillAmount = e.healthNormalized;
    }
}
