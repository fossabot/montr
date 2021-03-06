﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montr.Core.Models;
using Montr.Idx.Services;
using Montr.Kompany.Services;
using Montr.MasterData.Commands;
using Montr.MasterData.Models;
using Montr.MasterData.Queries;

namespace Montr.MasterData.Controllers
{
	[Authorize, ApiController, Route("api/[controller]/[action]")]
	public class ClassifierController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ICurrentCompanyProvider _currentCompanyProvider;
		private readonly ICurrentUserProvider _currentUserProvider;

		public ClassifierController(IMediator mediator,
			ICurrentCompanyProvider currentCompanyProvider, ICurrentUserProvider currentUserProvider)
		{
			_mediator = mediator;
			_currentCompanyProvider = currentCompanyProvider;
			_currentUserProvider = currentUserProvider;
		}

		[HttpPost]
		public async Task<SearchResult<Classifier>> List(GetClassifierList request)
		{
			request.CompanyUid = await _currentCompanyProvider.GetCompanyUid();
			request.UserUid = _currentUserProvider.GetUserUid();

			return await _mediator.Send(request);
		}

		[HttpPost]
		public async Task<FileStreamResult> Export(ClassifierSearchRequest request)
		{
			request.CompanyUid = await _currentCompanyProvider.GetCompanyUid();
			request.UserUid = _currentUserProvider.GetUserUid();

			var result = await _mediator.Send(new ExportClassifierList
			{
				Request = request
			});

			// http://blog.stephencleary.com/2016/11/streaming-zip-on-aspnet-core.html

			return File(result.Stream, result.ContentType, result.FileName);
		}

		[HttpPost]
		public async Task<Classifier> Create(CreateClassifier request)
		{
			request.CompanyUid = await _currentCompanyProvider.GetCompanyUid();
			request.UserUid = _currentUserProvider.GetUserUid();

			return await _mediator.Send(request);
		}

		[HttpPost]
		public async Task<Classifier> Get(GetClassifier request)
		{
			request.CompanyUid = await _currentCompanyProvider.GetCompanyUid();
			request.UserUid = _currentUserProvider.GetUserUid();

			return await _mediator.Send(request);
		}

		[HttpPost]
		public async Task<ApiResult> Insert(InsertClassifier request)
		{
			request.CompanyUid = await _currentCompanyProvider.GetCompanyUid();
			request.UserUid = _currentUserProvider.GetUserUid();

			return await _mediator.Send(request);
		}

		[HttpPost]
		public async Task<ApiResult> Update(UpdateClassifier request)
		{
			request.CompanyUid = await _currentCompanyProvider.GetCompanyUid();
			request.UserUid = _currentUserProvider.GetUserUid();

			return await _mediator.Send(request);
		}

		[HttpPost]
		public async Task<ApiResult> Delete(DeleteClassifier request)
		{
			request.CompanyUid = await _currentCompanyProvider.GetCompanyUid();
			request.UserUid = _currentUserProvider.GetUserUid();

			return await _mediator.Send(request);
		}
	}
}
