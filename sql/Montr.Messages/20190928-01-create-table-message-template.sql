-- Table: public.message_template

-- DROP TABLE public.message_template;

CREATE TABLE public.message_template
(
    uid uuid NOT NULL,
    subject character varying(2048) COLLATE pg_catalog."default",
    body text COLLATE pg_catalog."default",
    CONSTRAINT message_template_pk PRIMARY KEY (uid)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.message_template TO web;

/*
insert into message_template(uid, subject, body)
values ('4D3C920C-ABFC-4F21-B900-6AFB894413DD',
	   '🔥 Персональное приглашение на Запрос предложений № {{EventNo}}',
	   '![](https://dev.montr.net/favicon.ico)

### Здравствуйте!

**{{CompanyName}}** приглашает вас принять участие в торговой процедуре **Запрос предложений № {{EventNo}}**

**Предмет процедуры:**
{{EventName}}

Дата и время окончания приема заявок: **30.11.2018 15:00 MSK**   
Дата и время рассмотрения заявок: **14.12.2018 15:00 MSK**   
Дата и время подведения результатов процедуры: **31.12.2018 15:00 MSK**   

Ознакомиться с описанием процедуры можно по адресу <{{EventUrl}}>
');
*/


/*
insert into message_template(uid, subject, body)
values ('CEEF2983-C083-448F-88B1-2DA6E6CB41A4',
	   '📧 Confirm your email',
	   '### Hello!

Please confirm your account by clicking here <{{CallbackUrl}}>.
');
*/
