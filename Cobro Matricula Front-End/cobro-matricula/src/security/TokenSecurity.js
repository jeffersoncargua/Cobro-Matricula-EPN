// para utilizar js-cookie se debe instalar su paquete mediante el comando: npm install js-cookie --save
import Cookies from "js-cookie";

function SendToken(token) {
	const cookieToken = token;
	Cookies.set("jwtToken", cookieToken, {
		secure: true,
		sameSite: "Strict",
		expires: 30,
	});
}

function GetToken() {
	const token = Cookies.get("jwtToken");
	if (token !== null && token !== undefined) {
		return token;
	}

	return null;
}

function DeleteToken() {
	Cookies.remove("jwtToken", {
		secure: true,
		sameSite: "Strict",
	});
}

export { SendToken, GetToken, DeleteToken };
