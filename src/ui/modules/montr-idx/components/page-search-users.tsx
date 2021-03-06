import React from "react";
import { Link } from "react-router-dom";
import { Page, Toolbar, DataBreadcrumb, PageHeader, DataTable, ButtonAdd } from "@montr-core/components";
import { Api, Views } from "../module";

interface IProps {
}

interface IState {
}

export default class PageSearchUsers extends React.Component<IProps, IState> {
	render = () => {
		return (
			<Page title={<>
				<Toolbar float="right">
					<Link to="/users/new"><ButtonAdd /></Link>
				</Toolbar>

				<DataBreadcrumb items={[]} />
				<PageHeader>Пользователи</PageHeader>
			</>}>

				<DataTable
					rowKey="uid"
					viewId={Views.gridSearchUsers}
					loadUrl={Api.userList} />
			</Page>
		);
	};
};
