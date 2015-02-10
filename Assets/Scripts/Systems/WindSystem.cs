using Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class WindSystem : Framework.Core.System
    {
        public Texture2D texture;
        public List<Sprite> windSprites;
        public int numberOfFrames = 30;
        public float width = 50.0f;
        public float height = 300.0f;
        public float offset = 30.0f;
        public float speed = 0.02f;

        private int current = 0;
        private float currentSpeed = 0.0f; 


        public override void Initialize()
        {
            SetupSpriteFrames();
        }

        public override void OnFixedUpdate()
        {
            if (currentSpeed <= 0.0f)
            {
                foreach (Wind wind in GetListOf<Wind>())
                {
                    wind.spriteRenderer.sprite = windSprites[current % windSprites.Count];
                    wind.spriteRenderer.enabled = true;
                    current++;
                }

                currentSpeed = speed;
            }

            currentSpeed -= FixedDeltaTime;
        }

        private void SetupSpriteFrames()
        {
            windSprites = new List<Sprite>();
            Vector2 pv = new Vector2(0.5f, 0.5f);

            for (int i = numberOfFrames - 1; i >= 0; i--)
            {
                Rect loc = new Rect(0, i * offset, width, height);
                Sprite s = Sprite.Create(texture, loc, pv, 100f);
                windSprites.Add(s);
            }
        }
    }
}
