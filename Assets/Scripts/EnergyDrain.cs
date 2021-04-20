using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDrain : MonoBehaviour
{
    Image healthBarImage;
    // Start is called before the first frame update
    void Start()
    {
        healthBarImage = GetComponent<Image>();
        ItemCollector.OnEnergyCollected += Energycollected;
    }

    private void Energycollected(ItemCollector.CollectableItem item)
    {
        if (item == ItemCollector.CollectableItem.EnergyBall)
        {
            healthBarImage.fillAmount += 0.5f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount -= 0.1f * Time.deltaTime;
    }
}
