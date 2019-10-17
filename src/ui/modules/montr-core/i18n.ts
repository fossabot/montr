import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import i18nextBackend from "i18next-xhr-backend";
import i18nextLanguageDetector from "i18next-browser-languagedetector";

const defaultNS = "common";

i18n
	.use(i18nextBackend)
	.use(i18nextLanguageDetector)
	.use(initReactI18next)
	.init({
		// https://www.i18next.com/overview/configuration-options
		ns: defaultNS,
		defaultNS: defaultNS,
		fallbackNS: defaultNS,
		fallbackLng: "en",
		keySeparator: false,
		debug: true, // todo: use env var
		saveMissing: false,
		interpolation: {
			escapeValue: false,
		},
		react: {
			useSuspense: true,
		},
		// https://github.com/i18next/i18next-xhr-backend
		backend: {
			loadPath: "/api/locale/strings/{{lng}}/{{ns}}"
		},
		// https://github.com/i18next/i18next-browser-languageDetector
		detection: {
			order: ["cookie", "header"],
			lookupCookie: "lang",
			caches: ["cookie"],
		}
	});

export default i18n;
