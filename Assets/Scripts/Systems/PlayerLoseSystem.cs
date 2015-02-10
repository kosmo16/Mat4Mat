using Components;
using UnityEngine;

namespace Systems
{
    public class PlayerLoseSystem : Framework.Core.System
    {
        private string[] levelsOrder = new string[] { "scene0", "scene0rock", "scene1rock", "scene1gum", "scene2" };
        private int currentLevelNumber = 0;

        public override void Initialize()
        {
            SubscribeEvent(Event.PlayerLose, OnPlayerLose);
        }

        public override void OnUpdate()
        {
            foreach (Exit exit in GetListOf<Exit>())
            {
                if (exit.isReached)
                {
                    currentLevelNumber++;
                    currentLevelNumber %= levelsOrder.Length;

                    OnPlayerLose();
                }
            }
        }

        private void OnPlayerLose()
        {
            Application.LoadLevel(levelsOrder[currentLevelNumber]);
        }
    }
}
