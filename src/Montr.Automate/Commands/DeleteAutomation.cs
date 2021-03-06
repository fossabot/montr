﻿using System;
using MediatR;
using Montr.Core.Models;

namespace Montr.Automate.Commands
{
	public class DeleteAutomation : IRequest<ApiResult>
	{
		public string EntityTypeCode { get; set; }

		public Guid EntityTypeUid { get; set; }

		public Guid[] Uids { get; set; }	}
}
