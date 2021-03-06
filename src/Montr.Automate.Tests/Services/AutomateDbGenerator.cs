﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Montr.Automate.Commands;
using Montr.Automate.Impl.Models;
using Montr.Automate.Impl.QueryHandlers;
using Montr.Automate.Impl.Services;
using Montr.Automate.Models;
using Montr.Automate.Queries;
using Montr.Automate.Services;
using Montr.Core.Models;
using Montr.Core.Services;
using Montr.Data.Linq2Db;
using Moq;

namespace Montr.Automate.Tests.Services
{
	public class AutomateDbGenerator
	{
		private readonly IAutomationService _automationService;
		private readonly GetAutomationHandler _getAutomationHandler;

		public AutomateDbGenerator(IDbContextFactory dbContextFactory)
		{
			var jsonSerializer = new NewtonsoftJsonSerializer();

			var acpfMock = new Mock<INamedServiceFactory<IAutomationConditionProvider>>();
			acpfMock.Setup(x => x.Resolve(FieldAutomationCondition.TypeCode))
				.Returns(new NoopAutomationConditionProvider { RuleType = new AutomationRuleType { Type = typeof(FieldAutomationCondition) } });

			var aapfMock = new Mock<INamedServiceFactory<IAutomationActionProvider>>();
			aapfMock.Setup(x => x.Resolve(NotifyByEmailAutomationAction.TypeCode))
				.Returns(new NoopAutomationActionProvider { RuleType = new AutomationRuleType { Type =  typeof(NotifyByEmailAutomationAction) } });

			var automationRepository = new DbAutomationRepository(dbContextFactory, acpfMock.Object, aapfMock.Object, jsonSerializer);
			_automationService = new DefaultAutomationService(dbContextFactory, jsonSerializer);

			_getAutomationHandler = new GetAutomationHandler(automationRepository);
		}

		public string EntityTypeCode { get; set; } = "test_closure";

		public Guid EntityTypeUid { get; set; } = Guid.NewGuid();

		public async Task<ApiResult> InsertAutomation(Automation automation, CancellationToken cancellationToken)
		{
			var result = await _automationService.Insert(new InsertAutomation
			{
				EntityTypeCode = EntityTypeCode,
				EntityTypeUid = EntityTypeUid,
				Item = automation
			}, cancellationToken);

			Assert.IsNotNull(result);
			Assert.AreEqual(true, result.Success);

			return result;
		}

		public async Task<Automation> GetAutomation(Guid uid, CancellationToken cancellationToken)
		{
			return await _getAutomationHandler.Handle(new GetAutomation
			{
				EntityTypeCode = EntityTypeCode,
				EntityTypeUid = EntityTypeUid,
				Uid = uid
			}, cancellationToken);
		}

		private class NoopAutomationActionProvider : IAutomationActionProvider
		{
			public AutomationRuleType RuleType { get; set; }

			public Task Execute(AutomationAction automationAction, AutomationContext context, CancellationToken cancellationToken)
			{
				return Task.CompletedTask;
			}
		}

		private class NoopAutomationConditionProvider : IAutomationConditionProvider
		{
			public AutomationRuleType RuleType { get; set; }

			public Task<bool> Meet(AutomationCondition automationCondition, AutomationContext context, CancellationToken cancellationToken)
			{
				return Task.FromResult(false);
			}
		}
	}
}
