using Components;
using UnityEngine;

namespace Systems
{
    public class PlayerLoseSystem : Framework.Core.System
    {
        public Sprite openExit;
        public Sprite closeExit;
        public int numberOfCoins;

        private string[] levelsOrder = new string[] { "scene0", "scene0rock", "scene1rock", "scene1gum", "scene2" };
        private int currentLevelNumber = 0;

        public override void Initialize()
        {
            SubscribeEvent(Event.PlayerLose, OnPlayerLose);

            foreach (Exit exit in GetListOf<Exit>())
            {
                exit.GetComponent<SpriteRenderer>().sprite = closeExit;
            }
        }

        public override void OnUpdate()
        {
            Player player = GetFirstOrNull<Player>();

            if (player.score == numberOfCoins)
            {
                foreach (Exit exit in GetListOf<Exit>())
                {
                    if (!exit.isOpen)
                    {
                        exit.GetComponent<SpriteRenderer>().sprite = openExit;
                        exit.isOpen = true;
                    }

                    if (exit.isReached)
                    {
                        currentLevelNumber++;
                        currentLevelNumber %= levelsOrder.Length;

                        OnPlayerLose();
                    }
                }
            }
        }

        private void OnPlayerLose()
        {
            Application.LoadLevel(levelsOrder[currentLevelNumber]);
        }
    }
}
