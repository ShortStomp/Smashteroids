using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using EntityLibrary.Components;
using EntityLibrary.Components.Factory;
using EntityLibrary.Components.Interface;
using LogSystem;
using Smashteroids.Data;
using EntityLibrary.IOC;

namespace EntityLibrary.EntityIO
{
	internal class EntityParser : IEntityParser
	{
		private IComponentFactory _componentFactory;
		private XDocument _xmlDoc;
		private bool _fileOpened;
		private int _entityNumber;


		public EntityParser()
		{
			_componentFactory = IocContainer.Resolve<IComponentFactory>();
		}


		public void OpenFile(string filename)
		{
			if (filename == null || filename == string.Empty)
			{
				EntityIoLogger.WriteNullArgumentIoException(
				new ArgumentNullException("filename", filename == null ? "filename cannot be null" : "filename cannot be nothing"),
				IoType.XmlDoc,
				_entityNumber);
			}
			_xmlDoc = XDocument.Load(filename, LoadOptions.SetLineInfo);
			_fileOpened = true;
		}



		/// <summary>
		/// Public Interface, for loading all the entities. 
		/// </summary>
		/// <returns></returns>
		public IEnumerable<EntityData> LoadEntities()
		{
			if (!_fileOpened)
			{
				DefaultLogger.WriteExceptionThenQuit(MessageType.FileIOError,
					new InvalidOperationException("EntityParser cannot load entities before file has been opened."));
			}

			// otherwise file is open
			return ParseEntities(_xmlDoc.Descendants("Entity"));

		}



		/// <summary>
		/// Parses all of the entities into a collection of EntityData
		/// </summary>
		/// <param name="entityCollection">The entity collection.</param>
		/// <returns></returns>
		private IEnumerable<EntityData> ParseEntities(IEnumerable<XElement> entityCollection)
		{
			ICollection<EntityData> entities = new List<EntityData>(entityCollection.Count());

			foreach (var xEntity in entityCollection)
			{
				var parsedEntity = ParseEntity(xEntity);
				entities.Add(parsedEntity);
				++_entityNumber;
			}

			return entities;
		}



		/// <summary>
		/// Parses a single entity into an Entitydata object.
		/// </summary>
		/// <param name="xEntity">The x entity.</param>
		/// <returns></returns>
		private EntityData ParseEntity(XElement xEntity)
		{
			if (xEntity == null)
			{
				EntityIoLogger.WriteNullArgumentIoException(new ArgumentNullException(xEntity.ToString()), IoType.Component, _entityNumber);
			}

			EntityIoLogger.WriteIoInformation(xEntity, IoType.Entity, _entityNumber);
			ICollection<IComponent> components = new List<IComponent>();

			var xComponents = xEntity.Descendants("Components");

			var xRenderable = xComponents.Descendants("Renderable");
			var xPlayer = xComponents.Descendants("Player");

			if (xRenderable.Any())
			{
				components.Add(_componentFactory.CreateComponent<RenderableComponent>(xRenderable.FirstOrDefault()));
			}
			if (xPlayer.Any())
			{
				components.Add(_componentFactory.CreateComponent<PlayerComponent>(xPlayer.FirstOrDefault()));
			}

			return new EntityData() { Components = components };
		}
	}
}
