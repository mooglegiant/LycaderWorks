using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lycader
{
    public static class SceneManager
    {

        public static IScene Current { get; internal set; }

        private static IScene Next { get; set; }

        public static bool IsSceneChanging { get; internal set; } = false;

        static SceneManager()
        {
        }
       
        public static void ChangeScene(IScene next)
        {
            Next = next;
            IsSceneChanging = true;
        }

        internal static void ToggleScene()
        {
            if (IsSceneChanging)
            {
                Current.Unload();
                Next.Load();

                Current = Next;
                IsSceneChanging = false;
            }
        }
    }
}
