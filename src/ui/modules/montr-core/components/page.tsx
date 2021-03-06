import * as React from "react";
import { PageHeader } from ".";

interface IProps {
	title?: string | React.ReactNode;
}

export class Page extends React.Component<IProps> {
	render = () => {

		const { title, children } = this.props;

		return (
			<div>
				{/* <Affix> */}
				{(typeof title === "string") ? <PageHeader>{title}</PageHeader> : title}
				{/* </Affix> */}
				{children}
			</div>
		);
	}
};
