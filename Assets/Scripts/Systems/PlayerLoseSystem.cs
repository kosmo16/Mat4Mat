using Components;
using System.Linq;
using UnityEngine;

namespace Systems
{
    public class PlayerLoseSystem : Framework.Core.System
    {
        public Sprite openExit;
        public Sprite closeExit;
        public int numberOfCoins;
        public int nextLevelNumber;

        private string[] levelsOrder = new string[] { "scene0", "scene0rock", "scene1rock" ,"scene0electric", "scene1gum", "scene1rock", "scene2" };

        public override void Initialize()
        {
            SubscribeEvent(Event.PlayerLose, OnPlayerLose);

            nextLevelNumber = (levelsOrder.ToList().IndexOf(Application.loadedLevelName) + 1) % 7;

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
                        LoadNextLevel();
                    }
                }
            }
        }

        private void LoadNextLevel()
        {
            Application.LoadLevel(levelsOrder[nextLevelNumber]);
        }

        private void OnPlayerLose()
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}
