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


		private IEnumerable<EntityData> ParseEntities(IEnumerable<XElement> entityCollection)
		{
			ICollection<EntityData> entities = new List<EntityData>(entityCollection.Count());

			foreach (var xEntity in entityCollection)
			{
				entities.Add(ParseEntity(xEntity));
				++_entityNumber;
			}

			return entities;
		}


		private EntityData ParseEntity(XElement xEntity)
		{
			if (xEntity == null)
			{
				EntityIoLogger.WriteNullArgumentIoException(new ArgumentNullException(xEntity.ToString()), IoType.Component, _entityNumber);
			}
			EntityIoLogger.WriteIoInformation(xEntity, IoType.Entity, _entityNumber);
			ICollection<IComponent> components = new List<IComponent>();


			foreach (var xComponent in xEntity.Descendants("Components"))
			{
				components.Add(ParseComponent(xComponent));
			}

			return new EntityData() { Components = components };

		}


		private IComponent ParseComponent(XElement xComponent)
		{
			EntityIoLogger.WriteIoInformation(xComponent, IoType.Component, _entityNumber);

			try
			{
				return _componentFactory
					.CreateComponent<RenderableComponent>(xComponent.Descendants("Renderable")
					.SingleOrDefault());
			}
			catch (Exception exception)
			{
				EntityIoLogger.WriteFatalIOException(exception, xComponent, IoType.Component, _entityNumber);
				throw;
			}
		}
	}
}
