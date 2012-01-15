using System;
using System.Xml.Linq;
using EntityLibrary.Components.Base;
using EntityLibrary.Controllers;
using EntityLibrary.Extensions;
using EntityLibrary.Message;
using LogSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityLibrary.Components.Factory
{
	internal class ComponentFactory : IComponentFactory
	{
		#region Fields

		private IMessageFactory _messageFactory;
		private IRenderableController _renderableController;
		private IInteractiveController _playerController;
		private XElement _currentComponent;

		#endregion

		#region Constructors

		internal ComponentFactory(IMessageFactory factory, IRenderableController rc, IInteractiveController pc)
		{
			_messageFactory = factory;
			_renderableController = rc;
			_playerController = pc;
		}

		#endregion

		#region IComponentFactory Members


		public Component CreateComponent<T>(XElement xComponent) where T : Component
		{
			if (xComponent == null)
			{
				EntityIoLogger.WriteNullArgumentIoException(new ArgumentNullException("xComponent"), IoType.Component, -1);
			}

			// Reset the component counter and the pointer to the current component we are parsing
			_currentComponent = xComponent;

			if (typeof(T) == typeof(SpriteComponent))
			{
				return ParseSpriteComponent(xComponent);
			}
			else if (typeof(T) == typeof(PlayerComponent))
			{
				var pc = ParsePlayerComponent(xComponent);

				// TODO: going to have to fix this.
				//_playerController.AddPlayer(pc.Player);
				return pc;
			}
			else if (typeof(T) == typeof(PositionComponent))
			{
				return new PositionComponent()
				{
					Position = ParseVector2(xComponent, "Position")
				};
			}
			else if (typeof(T) == typeof(VelocityComponent))
			{
				return new VelocityComponent()
				{
					Velocity = ParseVector2(xComponent, "Velocity")
				};
			}
			else if (typeof(T) == typeof(AccelerationComponent))
			{
				return new AccelerationComponent()
				{
					Acceleration = ParseVector2(xComponent, "Acceleration")
				};
			}

			throw new InvalidOperationException(
				string.Format("Component factory cannot create component of type {0}.", typeof(T).ToString()));
		}


		/// <summary>
		/// Parses the player component.
		/// </summary>
		/// <param name="xPlayerComponent">The x player component.</param>
		/// <returns></returns>
		private PlayerComponent ParsePlayerComponent(XElement xPlayerComponent)
		{
			EntityIoLogger.WriteIoInformation(xPlayerComponent, IoType.Component, _currentComponent.LineNumber());

			try
			{
				return new PlayerComponent()
				{
					Name = xPlayerComponent.Element("name").Value
				};
			}
			catch (Exception e)
			{
				EntityIoLogger.WriteFatalIOException(e, xPlayerComponent, IoType.Component, _currentComponent.LineNumber());
				return default(PlayerComponent);
			}
		}



		/// <summary>
		/// Parses an xml sprite
		/// </summary>
		/// <param name="spriteElement">The sprite element to parse.</param>
		/// <returns></returns>
		private SpriteComponent ParseSpriteComponent(XElement spriteElement)
		{
			EntityIoLogger.WriteIoInformation(spriteElement, IoType.Component, _currentComponent.LineNumber());

			var sprite = new SpriteComponent()
			{
				Height = ParseFloat(spriteElement.Element("height"), "height"),
				Width = ParseFloat(spriteElement.Element("width"), "width"),
				SourceRect = ParseNullableRectangle(spriteElement.Element("texturerect"), "texturerect"),
				Color = ParseColor(spriteElement.Element("color"), "color"),
				Rotatation = ParseFloat(spriteElement.Element("rotation"), "rotation"),
				Scale = ParseVector2(spriteElement.Element("scale"), "scale", Vector2.One),
				SpriteEffect = ParseSpriteEffects(spriteElement.Element("spriteeffects")),
				DepthLayer = ParseFloat(spriteElement.Element("depth"), "depth"),
				Texture = _renderableController.GetTextureByFilename(spriteElement.Element("texture").Value),
			};
			
			/* If the origin is set in the XML file, use that. Otherwise,
			 * default to the middle of the sprite. */
			sprite.Origin = spriteElement.Element("origin") == null
				? new Vector2(sprite.Texture.Width / 2, sprite.Texture.Height / 2)
				: ParseVector2(spriteElement.Element("origin"), "origin");

			return sprite;
		}


		/// <summary>
		/// Parses an xml float.
		/// </summary>
		/// <param name="xFloatElement">The x float element.</param>
		/// <param name="valueName">Name of the value.</param>
		/// <returns></returns>
		private float ParseFloat(XElement xFloatElement, string valueName, float defaultValue = default(float))
		{
			if (xFloatElement == null)
			{
				EntityIoLogger.WriteIoUnspecifiedComponentProperty(_currentComponent, valueName, _currentComponent.LineNumber());
				return defaultValue;
			}
			EntityIoLogger.WriteIoInformation(xFloatElement, IoType.Component, _currentComponent.LineNumber());
			return float.Parse(xFloatElement.Value);
		}


		/// <summary>
		/// Parses the int.
		/// </summary>
		/// <param name="xIntElement">The x int element.</param>
		/// <param name="valueName">Name of the value.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		private float ParseInt(XElement xIntElement, string valueName, float defaultValue = default(float))
		{
			if (xIntElement == null)
			{
				EntityIoLogger.WriteIoUnspecifiedComponentProperty(_currentComponent, valueName, _currentComponent.LineNumber());
				return defaultValue;
			}
			EntityIoLogger.WriteIoInformation(xIntElement, IoType.Component, _currentComponent.LineNumber());
			return int.Parse(xIntElement.Value);
		}


		/// <summary>
		/// Parses the xml color.
		/// </summary>
		/// <param name="xColorElement">The x color element.</param>
		/// <returns></returns>
		private Color ParseColor(XElement xColorElement, string valueName)
		{
			if (xColorElement == null)
			{
				EntityIoLogger.WriteIoUnspecifiedComponentProperty(_currentComponent, valueName, _currentComponent.LineNumber());

				// use white here, default color is completely transparent
				return Color.White; 
			}
			EntityIoLogger.WriteIoInformation(xColorElement, IoType.Component, _currentComponent.LineNumber());

			return new Color()
			{
				R = (byte)ParseInt(xColorElement.Element("r"), "r"),
				G = (byte)ParseInt(xColorElement.Element("g"), "g"),
				B = (byte)ParseInt(xColorElement.Element("b"), "b"),
				A = (byte)ParseInt(xColorElement.Element("a"), "a")
			};
		}


		/// <summary>
		/// Parses an xml vector2.
		/// </summary>
		/// <param name="xPositionElement">The position element to parse.</param>
		/// <returns></returns>
		private Vector2 ParseVector2(XElement xPositionElement, string valueName, Vector2? defaultValue = null)
		{
			if (xPositionElement == null)
			{
				EntityIoLogger.WriteIoUnspecifiedComponentProperty(_currentComponent, valueName, _currentComponent.LineNumber());
				return default(Vector2);
			}
			EntityIoLogger.WriteIoInformation(xPositionElement, IoType.Component, _currentComponent.LineNumber());
			return new Vector2()
			{
				X = ParseFloat(xPositionElement.Element("x"), "x"),
				Y = ParseFloat(xPositionElement.Element("y"), "y")
			};
		}


		/// <summary>
		/// Parses an xml Rectangle
		/// </summary>
		/// <param name="xRectangleElement"></param>
		/// <returns></returns>
		private Rectangle? ParseNullableRectangle(XElement xRectangleElement, string valueName)
		{
			if (xRectangleElement == null)
			{
				EntityIoLogger.WriteIoUnspecifiedComponentProperty(_currentComponent, valueName, _currentComponent.LineNumber());
				return default(Rectangle?);
			}

			return ParseRectangle(xRectangleElement, valueName);
		}


		private Rectangle ParseRectangle(XElement xRectangleElement, string valueName)
		{
			if (xRectangleElement == null)
			{
				EntityIoLogger.WriteIoUnspecifiedComponentProperty(_currentComponent, valueName, _currentComponent.LineNumber());
				return default(Rectangle);
			}

			EntityIoLogger.WriteIoInformation(xRectangleElement, IoType.Component, _currentComponent.LineNumber());

			return new Rectangle()
			{
				X = Int32.Parse(xRectangleElement.Element("x").Value),
				Y = Int32.Parse(xRectangleElement.Element("y").Value),

				Height = Int32.Parse(xRectangleElement.Element("h").Value),
				Width = Int32.Parse(xRectangleElement.Element("w").Value)
			};
		}


		private SpriteEffects ParseSpriteEffects(XElement xSpriteEffectsElement)
		{
			if (xSpriteEffectsElement == null)
			{
				EntityIoLogger.WriteIoUnspecifiedComponentProperty(_currentComponent, "spriteEffects", _currentComponent.LineNumber());
				return default(SpriteEffects);
			}
			SpriteEffects se = new SpriteEffects();

			var s = xSpriteEffectsElement.Element("spriteeffects").Value;

			se |= s.Contains("fliphorizontally") ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			se |= s.Contains("flipvertically") ? SpriteEffects.FlipVertically : SpriteEffects.FlipVertically;

			return se;
		}

		#endregion
	}
}
