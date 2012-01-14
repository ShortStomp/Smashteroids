using System;
using System.Linq;
using System.Xml.Linq;
using EntityLibrary.Components.Interface;
using EntityLibrary.Components.Objects;
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
		private IPlayerController _playerController;
		private XElement _currentComponent;

		#endregion

		#region Constructors

		internal ComponentFactory(IMessageFactory factory, IRenderableController rc, IPlayerController pc)
		{
			_messageFactory = factory;
			_renderableController = rc;
			_playerController = pc;
		}

		#endregion

		#region IComponentFactory Members


		public IComponent CreateComponent<T>(XElement xComponent) where T : IComponent
		{
			if (xComponent == null)
			{
				EntityIoLogger.WriteNullArgumentIoException(new ArgumentNullException("xComponent"), IoType.Component, -1);
			}

			// Reset the component counter and the pointer to the current component we are parsing
			_currentComponent = xComponent;

			if (typeof(T) == typeof(RenderableComponent))
			{
				var rc = ParseRenderableComponent(xComponent);
				_messageFactory.CreateAndSendMessage(
					(Action<string, Sprite>)_renderableController.CreateNewTextureForSprite, DateTime.Now, rc.Sprite.Filename, rc.Sprite);
				return rc;
			}
			else if (typeof(T) == typeof(PlayerComponent))
			{
				var pc = ParsePlayerComponent(xComponent);
				_messageFactory.CreateAndSendMessage(
					(Action<Player>)_playerController.AddPlayer, DateTime.Now, pc.Player);

				return pc;
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
			EntityIoLogger.WriteIoInformation(xRenderableComponent, IoType.Component, _currentComponent.LineNumber());

			try
			{
				return new RenderableComponent()
				{
					Sprite = ParseSprite(xRenderableComponent.Element("sprite")),
				};
			}
			catch (Exception e)  
			{
				EntityIoLogger.WriteFatalIOException(e, xRenderableComponent, IoType.Component, _currentComponent.LineNumber());
				return default(RenderableComponent);
			}
		}


		private PlayerComponent ParsePlayerComponent(XElement xPlayerComponent)
		{
			EntityIoLogger.WriteIoInformation(xPlayerComponent, IoType.Component, _currentComponent.LineNumber());

			try
			{
				return new PlayerComponent()
				{
					Player = new Player() { _name = xPlayerComponent.Element("name").Value }
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
		private Sprite ParseSprite(XElement spriteElement)
		{
			EntityIoLogger.WriteIoInformation(spriteElement, IoType.Component, _currentComponent.LineNumber());

			var sprite = new Sprite(spriteElement.Element("filename").Value)
			{
				DestRect = ParseRectangle(spriteElement.Element("rectangle"), "rectangle"),
				SourceRect = ParseNullableRectangle(spriteElement.Element("texturerect"), "texturerect"),
				Color = ParseColor(spriteElement.Element("color"), "color"),
				Rotatation = ParseFloat(spriteElement.Element("rotation"), "rotation"),
				Scale = ParseFloat(spriteElement.Element("scale"), "scale", 1.0f),
				SpriteEffect = ParseSpriteEffects(spriteElement.Element("spriteeffects")),
				DepthLayer = ParseFloat(spriteElement.Element("depth"), "depth"),
			};
			
			/* If the origin is set in the XML file, use that. Otherwise,
			 * default to the middle of the sprite. */
			sprite.Origin = spriteElement.Element("origin") == null
				? new Vector2(sprite.DestRect.Width / 2, sprite.DestRect.Height / 2)
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
		private Vector2 ParseVector2(XElement xPositionElement, string valueName)
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
