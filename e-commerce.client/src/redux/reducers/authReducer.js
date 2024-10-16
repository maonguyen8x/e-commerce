import {
	SIGNIN,
	SIGNIN_SUCCESS,
	SIGNOUT_SUCCESS,
	SIGNIN_FAILURE,
} from "@/constants/constants";

const initialState = {
	isAuthenticating: false,
	authStatus: null,
};

const authReducer = (state = initialState, action) => {
	switch (action.type) {
		case SIGNIN:
			return { ...state, loading: true, isAuthenticating: true };
		case SIGNIN_SUCCESS:
			return {
				...state,
				isAuthenticating: false,
				loading: false,
				authStatus: { success: true, message: "Login successful!" },
			};
		case SIGNOUT_SUCCESS:
			return null;
		case SIGNIN_FAILURE:
			return {
				...state,
				isAuthenticating: false,
				authStatus: { success: false, message: action.error },
				loading: false,
				error: action.error,
			};

		default:
			return state;
	}
};

export default authReducer;
