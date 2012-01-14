using EntityLibrary.Components.Interface;
using EntityLibrary.Components.Objects;

namespace EntityLibrary.Components
{
	internal class RenderableComponent : IComponent
	{
		internal Sprite Sprite{ get; set; }
	}
}
