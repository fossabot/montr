import React from "react";
import { Guid } from "@montr-core/models";

export interface AutomationContextProps {
	entityTypeCode: string;
	entityTypeUid: Guid | string;
}

const defaultState: AutomationContextProps = {
	entityTypeCode: undefined,
	entityTypeUid: undefined
};

export const AutomationContext = React.createContext<AutomationContextProps>(defaultState);

export function withAutomationContext<P extends AutomationContextProps>(Component: React.ComponentType<P>) {
	return (props: Pick<P, Exclude<keyof P, keyof AutomationContextProps>>) => (
		<AutomationContext.Consumer>
			{(ctx) => (
				<Component {...props} {...ctx as P} />
			)}
		</AutomationContext.Consumer>
	);
}

export class AutomationContextProvider extends React.Component<AutomationContextProps> {
	render = () => {
		return (
			<AutomationContext.Provider value={this.props}>
				{this.props.children}
			</AutomationContext.Provider>
		);
	};
}
