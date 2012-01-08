using EntityLibrary.Components.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EntityLibrary.Components.Objects;

namespace EntityLibrary.Components
{
	internal class RenderableComponent : IComponent
	{
		internal Sprite Sprite{ get; set; }
		internal Vector2 Position { get; set; }
	}
}
