namespace LogSystem
{
	public enum MessageType
	{
		None = 0,
		RuntimeException,
		FileIOError,
		FileIOInfo,
		Warning,
		Information,
		Continued,
		UncaughtRuntimeException
	}

	public enum IoType
	{
		Entity,
		Component,
		XmlDoc
	}
}
