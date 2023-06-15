using System;
using UnityEngine;

namespace _GameData.Scripts.Managers
{
    public static class EventManager 
    {

        public static event Action<GemController> OnGemSold;
        public static void RaiseOnGemSold(GemController gemController) => OnGemSold?.Invoke(gemController);
    }
}
