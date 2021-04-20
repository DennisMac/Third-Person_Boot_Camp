using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemCollector : MonoBehaviour
{
    public enum CollectableItem { EnergyBall, Battery, Fuse, Key, Computer }
    public delegate void EnergyCollected(CollectableItem ci);
    public static event EnergyCollected OnEnergyCollected;


    private void OnTriggerEnter(Collider other)
    {
        Collectable collectable = other.gameObject.GetComponent<Collectable>();
        if (collectable != null)
        {
            OnEnergyCollected(collectable.ItemType);
            Destroy(other.gameObject); 
        }
       
      

    }
}
