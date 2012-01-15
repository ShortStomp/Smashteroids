using System.Collections.Generic;
using EntityLibrary.IOData;

namespace EntityLibrary.EntityIO
{
	internal interface IEntityParser
	{
		void OpenFile(string filename);
		IEnumerable<EntityData> LoadEntities();
	}
}
