import { createSlice } from "@reduxjs/toolkit";
import { JWTDecode } from "../security/JWTDecode";
import { DeleteToken, GetToken, SendToken } from "../security/TokenSecurity";

//import { JWTDecode } from '../security/JWTDecode';

const userSlice = createSlice({
	name: "user",
	initialState: {
		rol: null,
		user: null,
	},
	reducers: {
		login(state, action) {
			SendToken(action.payload.token);
			const result = JWTDecode(action.payload.token);
			return { ...state, rol: result.role, user: result.user };
			//return {...state, rol:action.payload.role, user:action.payload.email}
		},
		logout(state) {
			DeleteToken();
			return { ...state, rol: null, user: null };
		},
		getToken() {
			const token = GetToken();
			return token;
		},
		getUser(state) {
			const token = GetToken();

			if (token !== null && token !== undefined) {
				const cookieResult = JWTDecode(token);
				return { ...state, rol: cookieResult.role, user: cookieResult.user };
			}
			return { ...state, rol: null, user: null };
		},
	},
});

export const { login, logout, getToken, getUser } = userSlice.actions;
export const userReducer = userSlice.reducer;
