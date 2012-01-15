using System.Xml.Linq;
using EntityLibrary.Components.Base;

namespace EntityLibrary.Components.Factory
{
	interface IComponentFactory
	{
		Component CreateComponent<T>(XElement xComponent) where T : Component;
	}
}
