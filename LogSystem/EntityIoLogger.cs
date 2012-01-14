using System;
using System.IO;
using System.Xml.Linq;
using LogSystem.Extensions;
using Microsoft.Xna.Framework;

namespace LogSystem
{
	public static class EntityIoLogger
	{
		#region Fields

		internal static StreamWriter _entityIoStream;

		#endregion

		#region Properties

		public static bool FileWriterActive { get; set; }
		public static bool TimeStampingEnabled { get; set; }
		public static bool FatalExceptionCaught { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Initialize the static instance of the logger.
		/// </summary>
		/// <param name="game"></param>
		public static void Initialize(string _entityIoLogFilePath)
		{
			_entityIoStream = new StreamWriter(_entityIoLogFilePath, false);
		}


		/// <summary>
		/// Write fatal IO exception, then close down the game.
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="xElement"></param>
		public static void WriteFatalIOException(Exception exception, XElement xElement, IoType elementType, int elementNumber)
		{
			DefaultLogger.WriteExceptionThenQuit(
				MessageType.FileIOError,
				exception,
				string.Format("file IO exception: io type {0}, elementNumber {1}, xElementData: {2}, xml line number: {3}",
				elementType.ToString(),
				elementNumber,
				xElement.Value,
				xElement.LineNumber()));
		}


		/// <summary>
		/// Writes the null argument io exception to the stream, then shuts down the game.
		/// </summary>
		/// <param name="ane">The ane.</param>
		/// <param name="elementType">Type of the element.</param>
		/// <param name="elementNumber">The element number.</param>
		public static void WriteNullArgumentIoException(ArgumentNullException ane, IoType elementType, int elementNumber)
		{
			DefaultLogger.WriteExceptionThenQuit(
				MessageType.FileIOError,
				ane,
				string.Format("file IO argument null exception: io type {0}, elementNumber {1}, xml-line number: {2}",
				elementType.ToString(),
				elementNumber));
		}



		/// <summary>
		/// Writes an IO message.
		/// </summary>
		/// <param name="xElement">The x element.</param>
		/// <param name="entityNumber">The entity number.</param>
		public static void WriteIoInformation(XElement xElement, IoType elementType, int elementTypeCount)
		{
			// can't write an IO message if the xElement is null
			if (xElement == null)
			{
				WriteFatalIOException(new ArgumentNullException(xElement.ToString()), xElement, elementType, elementTypeCount);
			}

			DefaultLogger.DoWrite(
				_entityIoStream, 
				MessageType.FileIOInfo,
				true, 
				string.Format("Parsing element, iotype: {0}, element name {1}, xml-line number {2}", 
					elementType.ToString(), xElement.Name, xElement.LineNumber())
			);
		}


		public static void WriteIoUnspecifiedComponentProperty(XElement xParentComponent, string propertyName, int componentCount)
		{
			// can't write IO message if xElement or parent is null
			if (xParentComponent == null)
			{
				WriteFatalIOException(new ArgumentNullException(xParentComponent.ToString()), xParentComponent, IoType.Component, componentCount);
			}

			DefaultLogger.DoWrite(
				_entityIoStream,
				MessageType.FileIOInfo,
				true,
				string.Format("Component property [{0}] for component [{1}] (xml-line number: {2}) was not specified, using default value",
					propertyName, xParentComponent.Name, componentCount)
			);
		}

		/// <summary>
		/// Close down the streams. (Should only be called from DefaultLogger)
		/// </summary>
		internal static void Close()
		{
			_entityIoStream.Close();
		}

		#endregion
	}
}
