﻿using System;
using MediatR;
using Montr.MasterData.Models;
using Montr.Metadata.Models;

namespace Montr.MasterData.Commands
{
	public class InsertClassifier : IRequest<ApiResult>
	{
		public Guid UserUid { get; set; }

		public Guid CompanyUid { get; set; }

		public string TypeCode { get; set; }

		public Guid? TreeUid { get; set; }

		public Guid? GroupUid { get; set; }

		public Classifier Item { get; set; }
	}
}
