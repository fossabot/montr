import * as React from "react";
import { Page } from "@montr-core/components";
import { Spin, Button } from "antd";
import { RouteComponentProps } from "react-router-dom";
import { Translation } from "react-i18next";
import { AccountService } from "../services/account-service";

interface IRouteProps {
	userId: string;
	email: string;
	code: string;
}

interface IProps extends RouteComponentProps<IRouteProps> {
}

interface IState {
	loading: boolean;
}

export default class ConfirmEmailChange extends React.Component<IProps, IState> {

	private _accountService = new AccountService();

	constructor(props: IProps) {
		super(props);

		this.state = {
			loading: true
		};
	}

	componentDidMount = async () => {
		await this.fetchData();
	};

	componentWillUnmount = async () => {
		await this._accountService.abort();
	};

	fetchData = async () => {
		const { userId, email, code } = this.props.match.params;

		const result = await this._accountService.confirmEmailChange({ userId, email, code });

		if (result.success) {
			this.setState({ loading: false });
		}
	};

	handleContinue = async () => {
		// todo: use route const
		this.props.history.push("/dashboard");
	};

	render = () => {
		const { loading } = this.state;

		return (
			<Translation ns="idx">
				{(t) => <Page title={t("page.confirmEmailChange.title")}>
					<Spin spinning={loading}>

						{!loading && <p>Thank you for confirming your email change.</p>}

						<Button disabled={loading} onClick={this.handleContinue}>{t("button.continue")}</Button>
					</Spin>
				</Page>}
			</Translation>
		);
	};
}
