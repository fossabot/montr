﻿using System;
using MediatR;
using Montr.MasterData.Models;

namespace Montr.MasterData.Commands
{
	public class InsertClassifier: IRequest<Guid>
	{
		public Guid UserUid { get; set; }

		public Guid CompanyUid { get; set; }

		public Classifier Item { get; set; }
	}
}
