using UnityEngine;

public class GiveExpDeathReceiver : MonoBehaviour, IDeathReceiver
{
    [SerializeField] private int _expContribution = 2;
    private Experience _experience;

    public void OnDeath(GameObject deathDealer)
    {
        // You don't get money if the cause of death was END OF DAY lol
        // TODO: this is duplicated.  is there a better way?
        if (TagUtils.CompareTag(deathDealer, TagName.EnemySpawner))
        {
            return;
        }

        _experience.AddExp(_expContribution);
    }

    private void Start()
    {
        _experience = TagUtils.FindWithTag(TagName.House).GetComponent<Experience>();
    }

    public int GetExpContribution()
    {
        return _expContribution;
    }
}
