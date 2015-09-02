using System;

namespace Exercice5
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Exercice5 game = new Exercice5())
            {
                game.Run();
            }
        }
    }
#endif
}

