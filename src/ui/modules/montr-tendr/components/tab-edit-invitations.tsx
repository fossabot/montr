import * as React from "react";
import { Button, Drawer, Alert } from "antd";
import { IPaneProps, Guid, IDataResult, IMenu } from "@montr-core/models";
import { DataTable, Toolbar, DataTableUpdateToken, ButtonAdd, ButtonDelete, Icon } from "@montr-core/components";
import { PaneSearchClassifier } from "@montr-master-data/components";
import { ModalEditInvitation } from "../components";
import { IEvent, IInvitation } from "../models";
import { InvitationService } from "../services";
import { Constants } from "@montr-core/.";
import { OperationService } from "@montr-core/services";

interface IProps extends IPaneProps<IEvent> {
	data: IEvent;
}

interface IState {
	showDrawer?: boolean;
	editData?: IInvitation;
	selectedRowKeys?: string[] | number[];
	updateTableToken: DataTableUpdateToken;
}

export class TabEditInvitations extends React.Component<IProps, IState> {

	private _operation = new OperationService();
	private _invitationService = new InvitationService();

	constructor(props: IProps) {
		super(props);

		this.state = {
			updateTableToken: { date: new Date() }
		};
	}

	componentWillUnmount = async () => {
		await this._invitationService.abort();
	};

	onLoadTableData = async (loadUrl: string, postParams: any): Promise<IDataResult<{}>> => {
		const { data } = this.props;

		if (data) {

			const params = {
				eventUid: data.uid,
				...postParams
			};

			return await this._invitationService.post(loadUrl, params);
		}

		return null;
	};

	onSelectionChange = async (selectedRowKeys: string[] | number[]) => {
		this.setState({ selectedRowKeys });
	};

	refreshTable = async (resetSelectedRows?: boolean) => {
		const { selectedRowKeys } = this.state;

		this.setState({
			updateTableToken: { date: new Date(), resetSelectedRows },
			selectedRowKeys: resetSelectedRows ? [] : selectedRowKeys
		});
	};

	showAddDrawer = () => {
		this.setState({ showDrawer: true });
	};

	onCloseDrawer = async () => {
		this.setState({ showDrawer: false });
	};

	onSelect = async (keys: string[]) => {
		const { data } = this.props;

		await this._invitationService.insert({
			eventUid: data.uid,
			items: keys.map(x => {
				return { counterpartyUid: new Guid(x) };
			})
		});

		await this.onCloseDrawer();

		await this.refreshTable();
	};

	showAddModal = () => {
		this.setState({ editData: {} });
	};

	showEditModal = (data: IInvitation) => {
		this.setState({ editData: data });
	};

	onModalSuccess = async (data: IInvitation) => {
		this.setState({ editData: null });

		await this.refreshTable();
	};

	onModalCancel = () => {
		this.setState({ editData: null });
	};

	delete = async () => {
		await this._operation.execute(async () => {
			const { selectedRowKeys } = this.state;
			const result = await this._invitationService.delete(selectedRowKeys);
			if (result.success) {
				this.refreshTable(true);
			}
			return result;
		}, {
			showConfirm: true,
			confirmTitle: "Вы действительно хотите удалить выбранные приглашения?"
		});
	};

	render() {
		const { data } = this.props,
			{ selectedRowKeys, updateTableToken, editData, showDrawer } = this.state;

		const rowActions: IMenu[] = [
			{ name: "Редактировать", onClick: this.showEditModal }
		];

		return <>
			<Toolbar clear>
				<Button icon={Icon.Plus} onClick={this.showAddDrawer} type="primary">Пригласить</Button>
				<ButtonAdd onClick={this.showAddModal} />
				<ButtonDelete onClick={this.delete} disabled={!selectedRowKeys?.length} />
			</Toolbar>

			<DataTable
				rowKey="uid"
				viewId="Event/Invitation/List"
				loadUrl={`${Constants.apiURL}/invitation/list/`}
				rowActions={rowActions}
				onLoadData={this.onLoadTableData}
				onSelectionChange={this.onSelectionChange}
				updateToken={updateTableToken}
			/>

			<p />

			<Alert type="info" message={
				<ul>
					<li>Manual add</li>
					<li>Import from *.xls etc</li>
					<li>Select from registered companies</li>
					<li>Invite from companies catalogs</li>
					<li>👍 Select from counterparty classifier</li>
					<li>Copy invitation from other event</li>
				</ul>
			} />

			{editData &&
				<ModalEditInvitation
					eventUid={data.uid}
					uid={editData.uid}
					onSuccess={this.onModalSuccess}
					onCancel={this.onModalCancel}
				/>}

			{showDrawer &&
				<Drawer
					// title="Контрагенты"
					closable={false}
					onClose={this.onCloseDrawer}
					visible={true}
					width={1024}
				>
					<PaneSearchClassifier
						mode="Drawer"
						typeCode="counterparty"
						onSelect={this.onSelect}
					/>
				</Drawer>}
		</>;
	}
}
