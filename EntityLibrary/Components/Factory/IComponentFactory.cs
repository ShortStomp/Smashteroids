using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Interface;
using System.Xml.Linq;

namespace EntityLibrary.Components.Factory
{
	interface IComponentFactory
	{
		IComponent CreateComponent<T>(XElement xComponent) where T : IComponent;
	}
}
