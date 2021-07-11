using System.Collections.Generic;
using System.Linq;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace InputSystem.UI.View
{
    public class ProductionPanelView : MonoBehaviour
    {
        [SerializeField] private ProductionUnitView _currentProduction;
        [SerializeField] private Image _currentProductionTime;
        [SerializeField] private List<ProductionUnitView> _productionLine;

        private IList<IProductionTask> _productionTasks = new List<IProductionTask>();

        private const int MAX_UNITS_IN_LINE = 12;

        public void SetLine(IList<IProductionTask> productionTasks)
        {
            _productionTasks = productionTasks.ToList();
            UpdateLine();
        }

        private void UpdateLine()
        {
            Debug.Log("DisplayLine");

            _productionTasks = _productionTasks.ToList();
            if (_productionTasks.Count == 0)
            {
                return;
            }

            SetCurrent(_productionTasks.First());

            for (int i = 1; i < _productionTasks.Count && i < MAX_UNITS_IN_LINE; i++)
            {
                var productionTask = _productionTasks[i];
                _productionLine[i - 1].Icon = productionTask.UnitIcon;
                _productionLine[i - 1].Name = productionTask.UnitName;
                _productionLine[i - 1].Activate();
            }
        }

        public void AddNewTask(IProductionTask newTask)
        {
            _productionTasks.Add(newTask);
            UpdateLine();
        }

        public void RemoveCurrentTask()
        {
            _productionTasks.RemoveAt(0);

            ClearView();
            
            UpdateLine();
        }
        
        private void ClearView()
        {
            _currentProduction.Clear();
            foreach (var unitView in _productionLine)
            {
                unitView.Clear();
            }
        }

        public void ClearLine()
        {
            ClearView();
            _productionTasks.Clear();
        }

        private void SetCurrent(IProductionTask task)
        {
            var current = task;
            _currentProduction.Icon = current.UnitIcon;
            _currentProduction.Name = current.UnitName;
            _currentProduction.Activate();
            
            current.ProductionTimeLeft.Subscribe(timeLeft =>
                _currentProductionTime.fillAmount = (float)timeLeft / current.ProductionTime);
        }
    }
}