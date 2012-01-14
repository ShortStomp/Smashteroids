using System;
using LogSystem;

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
			using (Instance instance = new Instance())
			{
				try
				{
					instance.Run();
				}
				catch (Exception e)
				{
					if (EntityIoLogger.FatalExceptionCaught)
					{
						DefaultLogger.WriteExceptionThenQuit(MessageType.UncaughtRuntimeException, e, "Caught in main program.cs");
						DefaultLogger.Close();
						instance.Exit();
					}
				}
			}
		}
	}
#endif
}