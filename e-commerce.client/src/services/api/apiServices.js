// import { axiosInstance } from "../utils/axiosInstance";
import axiosInstance from "../utils/axiosInstance";
import axios from "axios";
// import { SIGNIN } from "@/constants/routes";

// import { PATH_AFTER_REGISTER, REGISTER_URL } from "../utils/globalConfig";
const apiServices = {
	// Sign Up
	createAccount: async (fullname, username, email, password) => {
		try {
			const response = await axios.post(
				"https://localhost:7006/api/Auth/register",
				{
					fullname,
					username,
					email,
					password,
				}
			);

			return response;
		} catch (error) {
			throw error.response;
		}
	},

	// Sign In
	signIn: async (username, password) => {
		try {
			const response = await axios.post(
				"https://localhost:7006/api/Auth/Login",
				{
					username,
					password,
				}
			);
			return response.data;
		} catch (error) {
			throw error.response ? error.response.data : new Error("API error");
		}
	},

	getByUserName: async (username, password) => {
		try {
			const response = await axios.post(
				"https://localhost:7006/api/Auth/Login",
				{
					username,
					password,
				}
			);

			console.log("response: ", response.data);

			// Save role, newToken into localStorage
			localStorage.setItem("roles", response.data.roles);
			localStorage.setItem("newToken", response.data.newToken);
			localStorage.setItem("userName", response.data.userName);

			return response.data;
		} catch (error) {
			throw error.response ? error.response.data : new Error("API error");
		}
	},
};

export default apiServices;
