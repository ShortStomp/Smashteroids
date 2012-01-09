using System;
using System.Xml.Linq;
using EntityLibrary.Components.Interface;
using EntityLibrary.Components.Objects;
using EntityLibrary.Controllers;
using LogSystem;
using Microsoft.Xna.Framework;

namespace EntityLibrary.Components.Factory
{
	internal class ComponentFactory : IComponentFactory
	{
		#region Fields


		#endregion

		#region Constructors

		internal ComponentFactory()
		{
		}

		#endregion


		#region IComponentFactory Members

		public IComponent CreateComponent<T>(XElement xComponent) where T : IComponent
		{
			if (typeof(T) == typeof(RenderableComponent))
			{
				var rc =  ParseRenderableComponent(xComponent);

				// add the texture to the texture repository
				// TODO: send message to the renderable controller, telling it to add the texture.
				//_renderableController.AddTexture(rc.Sprite.Filename, rc.Sprite.Texture);

				return rc;
			}

			throw new InvalidOperationException(
				string.Format("Component factory cannot create component of type {0}.", typeof(T).ToString()));
		}


		/// <summary>
		/// Parses an xml renderable component.
		/// </summary>
		/// <param name="xRenderableComponent">The xml renderable component.</param>
		/// <returns></returns>
		private RenderableComponent ParseRenderableComponent(XElement xRenderableComponent)
		{
			Logger.WriteIOMessage(xRenderableComponent);

			try
			{
				return new RenderableComponent()
				{
					Position = ParseVector2(xRenderableComponent.Element("Position")),
					Sprite = ParseSprite(xRenderableComponent.Element("sprite"))
				};
			}
			catch (Exception e)
			{
				Logger.WriteFatalIOException(e, xRenderableComponent);
				throw;
			}
		}



		/// <summary>
		/// Parses an xml sprite
		/// </summary>
		/// <param name="spriteElement">The sprite element to parse.</param>
		/// <returns></returns>
		private Sprite ParseSprite(XElement spriteElement)
		{
			Logger.WriteIOMessage(spriteElement);

			try
			{
				return new Sprite(spriteElement.Element("filename").Value);
			}
			catch (Exception e)
			{
				Logger.WriteFatalIOException(e, spriteElement);
				throw;
			}
		}



		/// <summary>
		/// Parses an xml vector2.
		/// </summary>
		/// <param name="xPositionElement">The position element to parse.</param>
		/// <returns></returns>
		private Vector2 ParseVector2(XElement xPositionElement)
		{
			Logger.WriteIOMessage(xPositionElement);
			try
			{
				return new Vector2()
				{
					X = Int32.Parse(xPositionElement.Element("x").Value),
					Y = Int32.Parse(xPositionElement.Element("y").Value)
				};
			}
			catch (Exception e)
			{
				if (e is ArgumentNullException || e is FormatException || e is OverflowException)
				{
					Logger.WriteFatalIOException(e, xPositionElement);
				}
				throw;
			}
		}

		#endregion
	}
}
