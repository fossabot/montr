import * as React from "react";
import { Page, DataTable } from "@montr-core/components";
import { RouteComponentProps } from "react-router";
import { Constants } from "@montr-core/.";
import { Icon, Button } from "antd";
import { Link } from "react-router-dom";
import { withCompanyContext, CompanyContextProps } from "@kompany/components";

interface IRouteProps {
	configCode: string;
}

interface IProps extends CompanyContextProps, RouteComponentProps<IRouteProps> {
}

class _SearchClassifier extends React.Component<IProps> {
	render() {
		const { currentCompany } = this.props,
			{ configCode } = this.props.match.params;

		if (!currentCompany) return null;

		return (
			<Page title={configCode} toolbar={
				<Link to={`/classifiers/${configCode}/new`}>
					<Button type="primary"><Icon type="plus" /> Добавить элемент</Button>
				</Link>
			}>

				<DataTable
					viewId={`ClassifierList/Grid/${configCode}`}
					loadUrl={`${Constants.baseURL}/classifier/list/`}
					postParams={{ configCode, companyUid: currentCompany.uid }}
					rowKey="uid"
				/>

			</Page>
		);
	}
}

export const SearchClassifier = withCompanyContext(_SearchClassifier);
