using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class ConfigurationSystem : Framework.Core.System
    {
        public override void OnStart()
        {

             Screen.autorotateToLandscapeLeft = true;
             Screen.autorotateToLandscapeRight = true;
             Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }
}
