using System;
using EntityLibrary.Controllers;

namespace EntityLibrary.Message
{
	class MessageFactory : IMessageFactory
	{
		#region Fields


		IRenderableController _renderableController;
		IAiController _aiController;
		ICollidableController _collidableController;
		IPriorityMessageQueue _messageQueue;

		#endregion

		#region Constructors

		internal MessageFactory(
			IPriorityMessageQueue messageQueue, 
			IRenderableController renderableController, 
			IAiController aiController, 
			ICollidableController collidableController)
		{
			_messageQueue = messageQueue;

			_renderableController = renderableController;
			_aiController = aiController;
			_collidableController = collidableController;
		}

		#endregion

		#region IMessageFactory Members

		public void CreateAndSendMessage(Delegate method, float msTimeToWait = 0, params object[] args)
		{
			_messageQueue.AddMessage(new Message(method, msTimeToWait, args));
		}

		public IRenderableController RenderableControllerReference()
		{
			return _renderableController;
		}

		#endregion

	}
}
