// import axios from "axios";
// import { axiosInstance } from "../utils/axiosInstance";
import axiosInstance from "../utils/axiosInstance";
import axios from "axios";
import toast from "react-hot-toast";
import { SIGNIN } from "@/constants/routes";

// import { PATH_AFTER_REGISTER, REGISTER_URL } from "../utils/globalConfig";
const apiServices = {
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
};

export default apiServices;
