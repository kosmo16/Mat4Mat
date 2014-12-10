using Components;
using UnityEngine;

namespace Systems
{
    public class PlayerLoseSystem : Framework.Core.System
    {
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
                    OnPlayerLose();
                }
            }
        }

        private void OnPlayerLose()
        {
            Application.LoadLevel("scene1rock");
        }
    }
}
