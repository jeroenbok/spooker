using System;
using System.Runtime.CompilerServices;

namespace Spooker.Web.Infrastructure.Extensions
{
    public static class ExtensionsToEventHandler
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Raise(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
                return;
            eventHandler(sender, e);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            if (eventHandler == null)
                return;
            eventHandler(sender, e);
        }
    }
}