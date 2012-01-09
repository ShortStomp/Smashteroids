using EntityLibrary.Components.Interface;
using EntityLibrary.Components.Objects;
using Microsoft.Xna.Framework;

namespace EntityLibrary.Components
{
	public class RenderableComponent : IComponent
	{
		internal Sprite Sprite{ get; set; }
		internal Vector2 Position { get; set; }
	}
}
