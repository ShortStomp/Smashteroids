using EntityLibrary.Components.Interface;
using EntityLibrary.Components.Objects;
using Microsoft.Xna.Framework;

namespace EntityLibrary.Components
{
	internal class RenderableComponent : IComponent
	{
		internal Sprite Sprite{ get; set; }
	}
}
