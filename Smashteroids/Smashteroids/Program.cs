using System;

namespace Smashteroids
{

#if WINDOWS || XBOX
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			using (Smashteroids instance = new Smashteroids()) 
			{
				instance.Run();
			}
		}
	}
#endif
}

