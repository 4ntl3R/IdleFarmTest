using TMPro;
using UnityEngine;

public class ResourcesView : MonoBehaviour
{
    private const string CoinTextTemplate = "{0}";
    private const string LootTextTemplate = "{0}/{1}";

    [SerializeField] 
    private TextMeshProUGUI _coinText;

    [SerializeField] 
    private TextMeshProUGUI _lootText;

    public void Inject(int lootCapacity)
    {
        SetCoinValue(0, 0);
        SetLootValue(0, lootCapacity);
    }

    public void SetCoinValue(int startValue, int endValue)
    {
        _coinText.text = string.Format(CoinTextTemplate, endValue);
    }

    public void SetLootValue(int currentValue, int maxValue)
    {
        _lootText.text = string.Format(LootTextTemplate, currentValue, maxValue);
    }
}
