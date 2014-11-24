﻿using Components;
using UnityEngine;

namespace Systems
{
    public class TestSystem : Framework.Core.System
    {
        public override void OnFixedUpdate()
        {
            foreach (TestComponent tc in GetListOf<TestComponent>())
            {
                tc.transform.position += tc.speed * Vector3.up;
            }
        }
    }
}
