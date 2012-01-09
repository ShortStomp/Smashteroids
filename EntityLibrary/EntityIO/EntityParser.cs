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


		public EntityParser()
		{
			_componentFactory = IocContainer.Resolve<IComponentFactory>();
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

			try
			{
				return _componentFactory
					.CreateComponent<RenderableComponent>(xComponent.Descendants("Renderable")
					.SingleOrDefault());
			}
			catch(Exception exception)
			{
				Logger.WriteFatalIOException(exception, xComponent);
				throw;
			}
		}
	}
}
