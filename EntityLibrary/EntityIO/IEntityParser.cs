using System.Collections.Generic;
using Smashteroids.Data;
namespace EntityLibrary.EntityIO
{
	internal interface IEntityParser
	{
		void OpenFile(string filename);
		IEnumerable<EntityData> LoadEntities();
	}
}
