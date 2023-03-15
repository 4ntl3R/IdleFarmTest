using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Extensions;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Views
{
    public class ResourcesView : MonoBehaviour
    {
        private const string LootTextTemplate = "{0}/{1}";

        [SerializeField] 
        private TextMeshProUGUI _coinText;

        [SerializeField] 
        private TextMeshProUGUI _lootText;

        private bool _isTextUpdating = false;
        private Sequence _textUpdateSequence;
        private UIAnimationParametersData _uiData;
        private LootDeliverParametersData _deliverData;

        public void Inject(int lootCapacity, UIAnimationParametersData uiData, LootDeliverParametersData deliverData)
        {
            SetLootValue(0, lootCapacity);
            SetCoinValue(0, 0, 0);
            _uiData = uiData;
            _deliverData = deliverData;
        }

        public void SetCoinValue(int startValue, int endValue, int clearedLoot)
        {
            if (_isTextUpdating)
            {
                startValue = int.Parse(_coinText.text);
                _textUpdateSequence.Kill();
            }
            _isTextUpdating = true;
        
            var duration = _deliverData.Duration + _deliverData.BasicTimeInterval * clearedLoot;
            var textData = new TextChangeParametersData(startValue, endValue, duration);
            _textUpdateSequence = _coinText.DOUpdateCounter(() => _isTextUpdating = false, textData, _uiData);
            _textUpdateSequence.Play();
        }

        public void SetLootValue(int currentValue, int maxValue)
        {
            _lootText.text = string.Format(LootTextTemplate, currentValue, maxValue);
        }

        public void AnimateCoin()
        {
            
        }
    }
}
