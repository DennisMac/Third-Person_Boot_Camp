using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    public ItemCollector.CollectableItem ItemType = ItemCollector.CollectableItem.Battery;
}
