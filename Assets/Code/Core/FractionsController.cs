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
                if (building.Team == Team.Blue)
                {
                    _blueBuilding = building;
                }
                else if (building.Team == Team.Red)
                {
                    _redBuilding = building;
                }
            }

            
            //TODO: fix this class 
            // _blueBuilding.OnDied += OnBuildingDied;
            // _redBuilding.OnDied += OnBuildingDied;
        }

        // private void OnBuildingDied(ISelectableItem building)
        // {
        //     if (building.Fraction == Team.Blue)
        //     {
        //         Debug.Log("Red win!");
        //     }
        //     else
        //     {
        //         Debug.Log("Blue win!");
        //     }
        // }

        private void OnDestroy()
        {
            // _blueBuilding.OnDied -= OnBuildingDied;
            // _redBuilding.OnDied -= OnBuildingDied;
        }
    }
}