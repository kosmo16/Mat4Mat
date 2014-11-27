﻿using Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class CameraFollowerSystem : Framework.Core.System
    {
        public override void OnUpdate()
        {
            FollowPlayer(GetFirstOrNull<CameraFollower>());
        }

        private void FollowPlayer(CameraFollower follower)
        {
            float targetX = Mathf.Lerp(
                follower.transform.position.x,
                follower.player.position.x,
                follower.xSmooth * DeltaTime);

            float targetY = Mathf.Lerp(
                follower.transform.position.y,
                follower.player.position.y,
                follower.ySmooth * DeltaTime);

            targetX = Mathf.Clamp(targetX, follower.minX, follower.maxX);
            targetY = Mathf.Clamp(targetY, follower.minY, float.MaxValue);

            follower.transform.position = new Vector3(targetX, targetY, follower.transform.position.z);
        }
    }
}