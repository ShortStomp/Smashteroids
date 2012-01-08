using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using EntityLibrary.Components;
using EntityLibrary.Components.Interface;
using EntityLibrary.Components.Objects;
using EntityLibrary.Entity;
using LogSystem;
using Microsoft.Xna.Framework;
using Smashteroids.Data;

namespace EntityLibrary.EntityIO
{
	internal class EntityParser : IEntityParser
	{
		private XDocument _xmlDoc;
		private bool _fileOpened;

		public EntityParser()
		{

		}

		public void OpenFile(string filename)
		{
			_xmlDoc = XDocument.Load(filename, LoadOptions.SetLineInfo);
			_fileOpened = true;
		}

		public IEnumerable<EntityData> LoadEntities()
		{
			if (!_fileOpened)
			{
				Logger.WriteExceptionThenQuit(MessageType.FileIOError,
					new InvalidOperationException("EntityParser cannot load entities before file has been opened."));
			}

			// otherwise file is open
			return ParseEntities(_xmlDoc.Descendants("Entity"));

		}

		private IEnumerable<EntityData> ParseEntities(IEnumerable<XElement> entityCollection)
		{
			ICollection<EntityData> entities = new List<EntityData>(entityCollection.Count());

			foreach (var xEntity in entityCollection)
			{
				entities.Add(ParseEntity(xEntity));
			}

			return entities;
		}


		private EntityData ParseEntity(XElement xEntity)
		{
			Logger.WriteIOMessage(xEntity);
			ICollection<IComponent> components = new List<IComponent>();

			try
			{
				foreach (var xComponent in xEntity.Descendants("Components"))
				{
					components.Add(ParseComponent(xEntity));
				}
			}
			catch (Exception exception)
			{
				Logger.WriteFatalIOException(exception, xEntity);
				throw;
			}

			return new EntityData()
			{
				Components = components
			};
		}


		private IComponent ParseComponent(XElement xComponent)
		{
			Logger.WriteIOMessage(xComponent);

			// TODO: visitor pattern?? builder pattern? think about
			try
			{
				return ParseRenderableComponent(xComponent.Descendants("Renderable")
					.SingleOrDefault());
			}
			catch(Exception exception)
			{
				Logger.WriteFatalIOException(exception, xComponent);
				throw;
			}
		}


		/// <summary>
		/// Parses an xml renderable component.
		/// </summary>
		/// <param name="xRenderableComponent">The xml renderable component.</param>
		/// <returns></returns>
		private RenderableComponent ParseRenderableComponent(XElement xRenderableComponent)
		{
			Logger.WriteIOMessage(xRenderableComponent);

			try
			{
				return new RenderableComponent()
				{
					Position = ParseVector2(xRenderableComponent.Element("Position")),
					Sprite = ParseSprite(xRenderableComponent.Element("sprite"))
				};
			}
			catch (Exception e)
			{
				Logger.WriteFatalIOException(e, xRenderableComponent);
				throw;
			}
		}



		/// <summary>
		/// Parses an xml sprite
		/// </summary>
		/// <param name="spriteElement">The sprite element to parse.</param>
		/// <returns></returns>
		private Sprite ParseSprite(XElement spriteElement)
		{
			Logger.WriteIOMessage(spriteElement);

			try
			{
				return new Sprite(spriteElement.Element("filename").Value);
			}
			catch (Exception e)
			{
				Logger.WriteFatalIOException(e, spriteElement);
				throw;
			}
		}



		/// <summary>
		/// Parses an xml vector2.
		/// </summary>
		/// <param name="xPositionElement">The position element to parse.</param>
		/// <returns></returns>
		private Vector2 ParseVector2(XElement xPositionElement)
		{
			Logger.WriteIOMessage(xPositionElement);
			try
			{
				return new Vector2()
				{
					X = Int32.Parse(xPositionElement.Element("x").Value),
					Y = Int32.Parse(xPositionElement.Element("y").Value)
				};
			}
			catch (Exception e)
			{
				if (e is ArgumentNullException || e is FormatException || e is OverflowException)
				{
					Logger.WriteFatalIOException(e, xPositionElement);
				}
				throw;
			}
		}
	}
}
