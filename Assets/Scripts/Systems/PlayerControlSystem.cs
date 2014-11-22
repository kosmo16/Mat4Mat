using Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class PlayerControlSystem : Framework.Core.System
    {
        public override void OnUpdate()
        {
            Player player = GetFirstOrNull<Player>();

            float h = Input.GetAxis("Horizontal");
            player.rigidbody.AddForce(Vector2.right * h);
        }
    }
}
