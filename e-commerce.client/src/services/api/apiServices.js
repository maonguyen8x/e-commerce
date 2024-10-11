// import axios from "axios";
// import { axiosInstance } from "../utils/axiosInstance";
import axiosInstance from "../utils/axiosInstance";
import axios from "axios";
import toast from "react-hot-toast";
import { SIGNIN } from "@/constants/routes";

import { PATH_AFTER_REGISTER, REGISTER_URL } from "../utils/globalConfig";

// class apiServices {
// 	// Register Method
// 	// const api = axios.create({
// 	// 	baseURL: "https://localhost:7098/api", // URL của backend ASP.NET Core
// 	// });
// 	// AUTH ACTIONS ------------
// 	// export const createAccount = async () => {
// 	// 	const response = await api.get("/users");
// 	// 	return response.data;
// 	// };
// 	// Register Method
// 	createAccount = async (
// 		firstName,
// 		lastName,
// 		userName,
// 		email,
// 		password,
// 		address
// 	) => {
// 		const response = await API.post(REGISTER_URL, {
// 			firstName,
// 			lastName,
// 			userName,
// 			email,
// 			password,
// 			address,
// 		});
// 		console.log("Register Result:", response);
// 		toast.success("Register Was Successfull. Please Login.");
// 		navigate(PATH_AFTER_REGISTER);
// 	};
// }

const apiServices = {
	createAccount: async (fullname, username, email, password) => {
		try {
			debugger;
			const response = await axios.post(
				"https://localhost:7006/api/Auth/register",
				{
					fullname,
					username,
					email,
					password,
				}
			);
			toast.success("Register Was Successfull. Please Login.");
			navigate(SIGNIN);
		} catch (error) {
			if (error.response) {
				if (error.response.status === 409) {
					toast.error("Email or username already exists.");
				} else {
					toast.error(`Error: ${error.response.data.Message}`);
				}
			} else {
				toast.error("Network error. Please try again.");
			}
		}
	},
};

export default apiServices;
