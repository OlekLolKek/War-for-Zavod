using System;
using Abstractions;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Core
{
    public class FractionsController : MonoBehaviour
    {
        private MainBuilding _blueBuilding;
        private MainBuilding _redBuilding;
        
        private void Awake()
        {
            var buildings = Object.FindObjectsOfType<MainBuilding>();

            foreach (var building in buildings)
            {
                if (building.Fraction == Fractions.Blue)
                {
                    _blueBuilding = building;
                }
                else if (building.Fraction == Fractions.Red)
                {
                    _redBuilding = building;
                }
            }

            _blueBuilding.OnDied += OnBuildingDied;
            _redBuilding.OnDied += OnBuildingDied;
        }

        private void OnBuildingDied(ISelectableItem building)
        {
            if (building.Fraction == Fractions.Blue)
            {
                Debug.Log("Red win!");
            }
            else
            {
                Debug.Log("Blue win!");
            }
        }

        private void OnDestroy()
        {
            _blueBuilding.OnDied -= OnBuildingDied;
            _redBuilding.OnDied -= OnBuildingDied;
        }
    }
}