using System.Collections.Generic;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Views
{
    public class VisualizationSwitchView : MonoBehaviour
    {
        [SerializeField] 
        private List<GameObject> visualizations;

        private int currentIndex;

        private void Awake()
        {
            ResetVisualization();
        }

        public void ResetVisualization()
        {
            currentIndex = 0;
            for (var i = 0; i < visualizations.Count; i++)
            {
                visualizations[i].SetActive(currentIndex == i);
            }
        }

        public void SwitchVisualization()
        {
            visualizations[currentIndex].SetActive(false);
            currentIndex = (currentIndex + 1) % visualizations.Count;
            visualizations[currentIndex].SetActive(true);
        }
    }
}
