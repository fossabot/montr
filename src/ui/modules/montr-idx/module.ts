import React from "react";
import { AppRouteRegistry } from "@montr-core/services/";
import { Layout, Constants } from "@montr-core/constants";

export const StorageNames = {
	email: "email"
};

export const Api = {
	authLinkLogin: `${Constants.apiURL}/authentication/linkLogin`,
	authExternalLogin: `${Constants.apiURL}/authentication/externalLogin`,

	userList: `${Constants.apiURL}/user/list`,
};

export const Patterns = {
	register: "/account/register",
	confirmEmail: "/account/confirm-email/:userId/:code",
	confirmEmailChange: "/account/confirm-email-change/:userId/:email/:code",
	sendEmailConfirmation: "/account/send-email-confirmation",
	login: "/account/login",
	logout: "/account/logout",
	externalLogin: "/account/external-login",
	forgotPassword: "/account/forgot-password",
	resetPassword: "/account/reset-password/:code",

	profile: "/profile",
	profileSecurity: "/profile/security",
	profileExternalLogin: "/profile/external-login",
	profileExternalLoginLink: "/profile/external-login/link",

	searchUsers: "/users",
};

export const Views = {
	formSendEmailConfirmation: "SendEmailConfirmation/Form",
	formLogin: "Login/Form",
	formRegister: "Register/Form",
	formExternalRegister: "ExternalRegister/Form",
	formUpdateProfile: "UpdateProfile/Form",
	formChangeEmail: "ChangeEmail/Form",
	formChangePhone: "ChangePhone/Form",
	formForgotPassword: "ForgotPassword/Form",
	formResetPassword: "ResetPassword/Form",
	formChangePassword: "ChangePassword/Form",
	formSetPassword: "SetPassword/Form",

	gridSearchUsers: "UserSearch/Grid",
};

AppRouteRegistry.add([
	{ path: Patterns.register, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-register")) },
	{ path: Patterns.confirmEmail, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-confirm-email")) },
	{ path: Patterns.confirmEmailChange, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-confirm-email-change")) },
	{ path: Patterns.sendEmailConfirmation, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-send-email-confirmation")) },
	{ path: Patterns.login, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-login")) },
	{ path: Patterns.logout, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-logout")) },
	{ path: Patterns.externalLogin, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-external-login")) },
	{ path: Patterns.forgotPassword, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-forgot-password")) },
	{ path: Patterns.resetPassword, layout: Layout.auth, exact: true, component: React.lazy(() => import("./components/page-reset-password")) },

	{ path: Patterns.profile, component: React.lazy(() => import("./components/page-profile")) },

	{ path: Patterns.searchUsers, exact: true, component: React.lazy(() => import("./components/page-search-users")) },
]);

export const ProfileRoutes = [
	{ path: Patterns.profile, exact: true, component: React.lazy(() => import("./components/pane-edit-profile")) },
	{ path: Patterns.profileSecurity, exact: true, component: React.lazy(() => import("./components/pane-security")) },
	{ path: Patterns.profileExternalLogin, exact: true, component: React.lazy(() => import("./components/pane-external-logins")) },
	{ path: Patterns.profileExternalLoginLink, exact: true, component: React.lazy(() => import("./components/pane-external-login-link")) },
];
