﻿using System.Xml;
using System.Xml.Linq;
using System;

namespace EntityLibrary.Extensions
{
	public static class XmlExtensions
	{
		/// <summary>
		/// Constant number of lines to skip at the top of the file, so the LineNumber 
		/// returns the correct line-number the element is on.
		/// </summary>
		public const int IgnoredLines = 2;


		/// <summary>
		/// Return the Current line number for an xml element
		/// </summary>
		/// <param name="xElement"></param>
		/// <returns></returns>
		public static int LineNumber(this IXmlLineInfo xElement)
		{
			if (xElement == null)
			{
				throw new ArgumentNullException("xElement");
			}
			return xElement.LineNumber + IgnoredLines;
		}
	}
}
