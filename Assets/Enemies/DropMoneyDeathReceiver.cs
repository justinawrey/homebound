using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct Drop
{
    public float Chance;
    public GameObject DropObject;

    public bool TryToDrop(Vector3 pos, Transform parent)
    {
        if (Random.value <= Chance)
        {
            GameObject.Instantiate(DropObject, pos, Quaternion.identity, parent);
            return true;
        }

        return false;
    }
}

public class DropMoneyDeathReceiver : MonoBehaviour, IDeathReceiver
{
    [SerializeField] private List<Drop> _drops;
    [SerializeField] private float _dropYPos = 0.75f;

    private Transform _moneyContainer;

    private void Start()
    {
        _moneyContainer = TagUtils.FindWithTag(TagName.MoneyContainer).transform;
    }

    public void OnDeath(GameObject deathDealer)
    {
        // You don't get money if the cause of death was END OF DAY lol
        if (TagUtils.CompareTag(deathDealer, TagName.EnemySpawner))
        {
            return;
        }

        List<Drop> sorted = _drops.OrderBy(drop => drop.Chance).ToList();
        Vector3 dropPos = new Vector3(transform.position.x, _dropYPos, transform.position.z);
        foreach (Drop drop in sorted)
        {
            if (drop.TryToDrop(dropPos, _moneyContainer))
            {
                return;
            }
        }
    }
}