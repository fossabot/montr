﻿using System;
using MediatR;
using Montr.Core.Models;

namespace Montr.Tendr.Commands
{
	public class CancelEvent: IRequest<ApiResult>
	{
		public Guid CompanyUid { get; set; }

		public Guid UserUid { get; set; }

		public Guid Uid { get; set; }
	}
}
