import React from "react";
import { Spin, Drawer } from "antd";
import { FormInstance } from "antd/lib/form";
import { Guid, IApiResult, IDataField } from "@montr-core/models";
import { Toolbar, ButtonCancel, ButtonSave, DataForm } from "@montr-core/components";
import { IAutomation, IGroupAutomationCondition, IFieldAutomationCondition } from "../models";
import { MetadataService } from "@montr-core/services";
import { AutomationService } from "../services/automation-service";

interface IProps {
	entityTypeCode: string;
	entityTypeUid: Guid | string;
	uid?: Guid;
	onSuccess?: () => void;
	onClose?: () => void;
}

interface IState {
	loading: boolean;
	data?: IAutomation;
	fields?: IDataField[];
}

export class PaneEditAutomation extends React.Component<IProps, IState> {

	private _metadataService = new MetadataService();
	private _automationService = new AutomationService();
	private _formRef = React.createRef<FormInstance>();

	constructor(props: IProps) {
		super(props);

		this.state = {
			loading: true,
		};
	}
	componentDidMount = async () => {
		await this.fetchData();
	};

	componentWillUnmount = async () => {
		await this._metadataService.abort();
		await this._automationService.abort();
	};

	fetchData = async () => {
		const { entityTypeCode, entityTypeUid, uid } = this.props;

		const condition1: IGroupAutomationCondition = { type: "group", meet: "all" };
		const condition2: IFieldAutomationCondition = { type: "field" };

		const data: IAutomation = (uid)
			? await this._automationService.get(entityTypeCode, entityTypeUid, uid)
			// todo: load defaults from server
			: {
				conditions: [condition1, condition2],
				actions: [{ type: "set-field" }, { type: "notify-by-email" }]
			};

		const dataView = await this._metadataService.load("Automation/Edit");

		this.setState({
			loading: false,
			data,
			fields: dataView?.fields || [],
		});
	};

	handleSubmitClick = async (e: React.MouseEvent<any>) => {
		await this._formRef.current.submit();
	};

	handleSubmit = async (values: IAutomation): Promise<IApiResult> => {
		const { entityTypeCode, entityTypeUid, uid, onSuccess } = this.props;

		const item = { ...values };

		let result;

		if (uid) {
			result = await this._automationService.update({ entityTypeCode, entityTypeUid, item: { uid, ...item } });
		}
		else {
			result = await this._automationService.insert({ entityTypeCode, entityTypeUid, item });
		}

		if (result.success && onSuccess) {
			onSuccess();
		}

		return result;
	};

	render = () => {
		const { onClose } = this.props,
			{ loading, data, fields } = this.state;

		return (<>
			<Spin spinning={loading}>
				<Drawer
					title="Automation"
					closable={true}
					onClose={onClose}
					visible={true}
					width={800}
					footer={
						<Toolbar clear size="small" float="right">
							<ButtonCancel onClick={onClose} />
							<ButtonSave onClick={this.handleSubmitClick} />
						</Toolbar>}>

					<DataForm
						formRef={this._formRef}
						showControls={false}
						fields={fields}
						data={data}
						onSubmit={this.handleSubmit} />

				</Drawer>
			</Spin>
		</>);
	};

}
