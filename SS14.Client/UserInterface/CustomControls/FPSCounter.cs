﻿using SS14.Client.UserInterface.Controls;
using SS14.Shared.Interfaces.Timing;
using SS14.Shared.IoC;
using SS14.Shared.Maths;
using SS14.Shared.Reflection;

namespace SS14.Client.UserInterface.CustomControls
{
    public class FPSCounter : Label
    {
        protected override void Initialize()
        {
            base.Initialize();

            FontColorShadowOverride = Color.Black;
            ShadowOffsetXOverride = 1;
            ShadowOffsetYOverride = 1;

            MouseFilter = MouseFilterMode.Ignore;
        }

        protected override void Update(ProcessFrameEventArgs args)
        {
            if (!VisibleInTree)
            {
                return;
            }

            var fps = IoCManager.Resolve<IGameTiming>().FramesPerSecondAvg;
            Text = $"FPS: {fps:N1}";
        }
    }
}
