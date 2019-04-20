import { Fetcher } from "@montr-core/services";
import { Constants } from "@montr-core/.";
import { Guid, IDataResult } from "@montr-core/models";
import { IClassifierType } from "../models";

interface GetClassifierType {
	typeCode?: string;
	uid?: Guid;
}

export class ClassifierTypeService extends Fetcher {
	list = async (companyUid: Guid): Promise<IDataResult<IClassifierType>> => {
		return this.post(`${Constants.baseURL}/classifierType/list`, { companyUid });
	};

	get = async (companyUid: Guid, request: GetClassifierType): Promise<IClassifierType> => {
		return this.post(`${Constants.baseURL}/classifierType/get`, { companyUid, ...request });
	};

	insert = async (companyUid: Guid, data: IClassifierType): Promise<Guid> => {
		return this.post(`${Constants.baseURL}/classifierType/insert`, { companyUid, item: data });
	};

	update = async (companyUid: Guid, data: IClassifierType): Promise<number> => {
		return this.post(`${Constants.baseURL}/classifierType/update`, { companyUid, item: data });
	};

	delete = async (companyUid: Guid, uids: string[] | number[]): Promise<number> => {
		return this.post(`${Constants.baseURL}/classifierType/delete`, { companyUid, uids });
	};
}
