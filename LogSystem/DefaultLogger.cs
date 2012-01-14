using System;
using System.IO;
using System.Xml.Linq;
using LogSystem.Extensions;
using Microsoft.Xna.Framework;

namespace LogSystem
{
	public static class DefaultLogger
	{
		#region Private static Fields

		private static StreamWriter _defaultLogStream;

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
		public static void Initialize(Game game, string logFilePathAndFilename)
		{
			_game = game;
			_defaultLogStream = new StreamWriter(logFilePathAndFilename, false);
		}



		/// <summary>
		/// Writes the specified message to the default log.
		/// </summary>
		/// <param name="type">The message typetype.</param>
		/// <param name="message">The message.</param>
		/// <param name="preEmptyLines">The pre empty lines.</param>
		/// <param name="postEmptyLines">The post empty lines.</param>
		public static void Write(MessageType type, string message, int preEmptyLines = 0, int postEmptyLines = 0)
		{
			DoWrite(_defaultLogStream, type, false, message, preEmptyLines, postEmptyLines);
		}



		/// <summary>
		/// Writes the specified message to the default log, ending with a \r\n.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="preEmptyLines">The pre empty lines.</param>
		/// <param name="postEmptyLines">The post empty lines.</param>
		public static void WriteLine(MessageType type, string message, int preEmptyLines = 0, int postEmptyLines = 0)
		{
			DoWrite(_defaultLogStream, type, true, message, preEmptyLines, postEmptyLines);
		}



		/// <summary>
		/// Writes an exception then forces the game to quit.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="details">The details.</param>
		public static void WriteExceptionThenQuit(MessageType type, Exception exception, string details = null)
		{
			if (exception != null)
			{
				DoWrite(_defaultLogStream, type, true, exception.ToString(), 1, 1);
			}
			else
			{
				WriteExceptionThenQuit(MessageType.RuntimeException, new ArgumentNullException("exception"));
			}
			DoWrite(_defaultLogStream, MessageType.Continued, true, details);
			FatalExceptionCaught = true;
			_game.Exit();
		}


		/// <summary>
		/// Close down the streams.
		/// </summary>
		public static void Close()
		{
			_defaultLogStream.Close();
			EntityIoLogger.Close();
		}

		#endregion

		#region Private Methods



		/// <summary>
		/// Does the writing to the specified stream, used internally within the Logger assembly only.
		/// </summary>
		/// <param name="streamTarget">The stream target.</param>
		/// <param name="type">The type.</param>
		/// <param name="newline">if set to <c>true</c> [newline].</param>
		/// <param name="originalMessage">The original message.</param>
		/// <param name="preEmptyLines">The pre empty lines.</param>
		/// <param name="postEmptyLines">The post empty lines.</param>
		internal static void DoWrite(StreamWriter streamTarget, MessageType type, bool newline, string originalMessage, 
			int preEmptyLines = 0, int postEmptyLines = 0)
		{
			string messageToWrite = String.Format("{0}[{1}] {2}",
				TimeStampingEnabled ? DateTime.Now.ToString() + " " : string.Empty,
				type != MessageType.None ? type.ToString() : string.Empty,
				originalMessage);

			// Write any prefixed empty lines
			WriteBlanklines(streamTarget, preEmptyLines);

			if (newline)
			{
				streamTarget.WriteLine(messageToWrite);
			}
			else
			{
				streamTarget.Write(messageToWrite);
			}

			// Write any postfixed empty lines
			WriteBlanklines(streamTarget, postEmptyLines);
			
		}

		/// <summary>
		/// Write a series of blank lines using the streamwriter
		/// </summary>
		/// <param name="blankLineCount"></param>
		private static void WriteBlanklines(StreamWriter streamTarget, int blankLineCount)
		{
			streamTarget.Write(new string('\n', blankLineCount));
		}

		#endregion
	}
}
