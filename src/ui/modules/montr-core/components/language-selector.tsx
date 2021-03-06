import * as React from "react";
import { Translation } from "react-i18next";
import { Menu, Dropdown } from "antd";
import { IIndexer } from "../models";
import { Icon } from ".";

export const LanguageSelector = () => {

	// todo: load from server
	const langs: IIndexer = {
		"en": { title: "English" },
		"ru": { title: "русский" },
	};

	return (
		<Translation>
			{(t, { i18n }) => (
				<Dropdown
					trigger={["click"]}
					overlay={
						<Menu>
							{Object.keys(langs).map(x => {
								return (
									<Menu.Item key={x} onClick={() => i18n.changeLanguage(x)}>
										{langs[x].title}
									</Menu.Item>
								);
							})}
						</Menu>}>
					<a className="ant-dropdown-link" href="#">
						{Icon.Global} {langs[i18n.language].title} {Icon.Down}
					</a>
				</Dropdown>
			)}
		</Translation>
	);
};
