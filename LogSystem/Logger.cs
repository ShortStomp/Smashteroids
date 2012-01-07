using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace LogSystem
{
	public static class Logger
	{
		#region Private static Fields
		//private static bool defaultAppendOption = false;
		private static StreamWriter streamWriter = new StreamWriter("../../../Logfile.txt", false);
		#endregion

		#region Public Static Properties

		public static bool FileWriterActive { get; set; }
		public static bool TimeStampingEnabled { get; set; }
		public static bool FatalExceptionCaught { get; set; }

		#endregion

		#region Private Static Fields

		private static Game _game { get; set; }

		#endregion


		#region Public Methods

		/// <summary>
		/// Initialize the static instance of the logger.
		/// </summary>
		/// <param name="game"></param>
		public static void Initialize(Game game)
		{
			_game = game;
		}

		public static void Write(MessageType type, string message, int preEmptyLines = 0, int postEmptyLines = 0)
		{
			InternalWrite(type, false, message, preEmptyLines, postEmptyLines);
		}

		public static void WriteLine(MessageType type, string message, int preEmptyLines = 0, int postEmptyLines = 0)
		{
			InternalWrite(type, true, message, preEmptyLines, postEmptyLines);
		}


		public static void WriteExceptionThenQuit(MessageType type, Exception exception, string details = null)
		{
			if (exception != null)
			{
				InternalWrite(type, true, exception.ToString(), 1, 1);
			}
			else
			{
				WriteExceptionThenQuit(MessageType.RuntimeException, new ArgumentNullException("exception"));
			}
			InternalWrite(MessageType.Continued, true, details);
			FatalExceptionCaught = true;
			_game.Exit();
		}

		//public static void WriteFatalFileIOException(Exception exception, XElement xElement)
		//{
		//    WriteExceptionThenQuit(
		//        MessageType.FileIOError,
		//        exception, 
		//        string.Format("file IO error, exception: element: {0}, line number: {1}",
		//        xElement.Value,
		//        xElement.LineNumber()));
		//}

		//public static void WriteFileIOMessage(XElement xElement)
		//{
		//    if (xElement == null)
		//    {
		//        WriteFatalFileIOException(new ArgumentNullException("element"), xElement);
		//    }
		//    InternalWrite(MessageType.FileIOInfo,
		//        true, string.Format("Parsing {0}, line #{1}", xElement.Value, xElement.LineNumber()));
		//}

		/// <summary>
		/// Close down the Logger.
		/// </summary>
		public static void Close()
		{
			streamWriter.Close();
		}

		#endregion

		#region Private Methods

		private static void InternalWrite(MessageType type, bool newline, string originalMessage, 
			int preEmptyLines = 0, int postEmptyLines = 0)
		{
			string messageToWrite = String.Format("{0}[{1}] {2}",
				TimeStampingEnabled ? DateTime.Now.ToString() + " " : string.Empty,
				type != MessageType.None ? type.ToString() : string.Empty,
				originalMessage);

			// Write any prefixed empty lines
			WriteBlanklines(preEmptyLines);

			if (newline)
			{
				streamWriter.WriteLine(messageToWrite);
			}
			else
			{
				streamWriter.Write(messageToWrite);
			}

			// Write any postfixed empty lines
			WriteBlanklines(postEmptyLines);
			
		}

		/// <summary>
		/// Write a series of blank lines using the streamwriter
		/// </summary>
		/// <param name="blankLineCount"></param>
		private static void WriteBlanklines(int blankLineCount)
		{
			streamWriter.Write(new string('\n', blankLineCount));
		}

		#endregion
	}
}
