﻿using System;
using MediatR;
using Montr.Core.Models;
using Montr.MasterData.Models;

namespace Montr.MasterData.Commands
{
	public class UpdateClassifierType : IRequest<ApiResult>
	{
		public Guid UserUid { get; set; }

		public ClassifierType Item { get; set; }
	}
}
