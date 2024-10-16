import { ArrowRightOutlined, LoadingOutlined } from "@ant-design/icons";
import { SocialLogin } from "@/components/common";
import { CustomInput } from "@/components/formik";
import { FORGOT_PASSWORD, SIGNUP } from "@/constants/routes";
import { Field, Form, Formik } from "formik";
import { useDocumentTitle, useScrollTop } from "@/hooks";
import PropType from "prop-types";
import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { signIn } from "@/redux/actions/authActions";
import { setAuthenticating, setAuthStatus } from "@/redux/actions/miscActions";
import * as Yup from "yup";
import { useTranslation } from "react-i18next";

const SignIn = ({ history }) => {
	const { t } = useTranslation();
	const SignInSchema = Yup.object().shape({
		// email: Yup.string()
		// 	.email(t("email_not_invalid"))
		// 	.required(t("email_required")),
		username: Yup.string().required(t("user_name_required")),
		password: Yup.string().required(t("password_required")),
	});

	const { user, authStatus, isAuthenticating } = useSelector((state) => ({
		user: state.auth.user,
		authStatus: state.app.authStatus,
		isAuthenticating: state.app.isAuthenticating,
	}));

	const dispatch = useDispatch();

	useScrollTop();
	useDocumentTitle("Sign In | ITAGE SHOP");

	useEffect(
		() => () => {
			dispatch(setAuthStatus(null));
			dispatch(setAuthenticating(false));
		},
		[]
	);

	useEffect(() => {
		if (user) {
			// Redirect to dashboard or other page after successful login
			history.push("/admin/dashboard");
		}
	}, [user, history]);

	const onSignUp = () => history.push(SIGNUP);

	const onSubmitForm = (form) => {
		dispatch(signIn(form.username, form.password));
	};

	const onClickLink = (e) => {
		if (isAuthenticating) e.preventDefault();
	};

	return (
		<div className="auth-content">
			{authStatus?.success && (
				<div className="loader">
					<h3 className="toast-success auth-success">
						{authStatus.message}
						<LoadingOutlined />
					</h3>
				</div>
			)}
			{authStatus?.message && !authStatus?.success && (
				<h5 className="text-center toast-error">{authStatus?.message}</h5>
			)}
			{!authStatus?.success && (
				<>
					{authStatus?.message && (
						<h5 className="text-center toast-error">{authStatus?.message}</h5>
					)}
					<div
						className={`auth ${
							authStatus?.message && !authStatus?.success && "input-error"
						}`}>
						<div className="auth-main">
							<h3>{t("sign_in_itage_shop")}</h3>
							<br />
							<div className="auth-wrapper">
								<Formik
									initialValues={{
										username: "",
										password: "",
									}}
									validateOnChange
									validationSchema={SignInSchema}
									onSubmit={onSubmitForm}>
									{() => (
										<Form>
											<div className="auth-field">
												<Field
													disabled={isAuthenticating}
													name="username"
													type="text"
													label={t("username")}
													placeholder={t("enter_user_name")}
													component={CustomInput}
												/>
											</div>
											<div className="auth-field">
												<Field
													disabled={isAuthenticating}
													name="password"
													type="password"
													label={t("password")}
													placeholder={t("enter_your_password")}
													component={CustomInput}
												/>
											</div>
											<br />
											<div className="auth-field auth-action">
												<Link
													onClick={onClickLink}
													style={{ textDecoration: "underline" }}
													to={FORGOT_PASSWORD}>
													<span>{t("forgot_your_password")}</span>
												</Link>
												<button
													className="button auth-button"
													disabled={isAuthenticating}
													type="submit">
													{isAuthenticating ? "Signing In" : "Sign In"}
													&nbsp;
													{isAuthenticating ? (
														<LoadingOutlined />
													) : (
														<ArrowRightOutlined />
													)}
												</button>
											</div>
										</Form>
									)}
								</Formik>
							</div>
						</div>
						<div className="auth-divider">
							<h6>OR</h6>
						</div>
						<SocialLogin isLoading={isAuthenticating} />
					</div>
					<div className="auth-message">
						<span className="auth-info">
							<strong>{t("dont_account")}</strong>
						</span>
						<button
							className="button button-small button-border button-border-gray button-icon"
							disabled={isAuthenticating}
							onClick={onSignUp}
							type="button">
							{t("sign_up")}
						</button>
					</div>
				</>
			)}
		</div>
	);
};

SignIn.propTypes = {
	history: PropType.shape({
		push: PropType.func,
	}).isRequired,
};

export default SignIn;
