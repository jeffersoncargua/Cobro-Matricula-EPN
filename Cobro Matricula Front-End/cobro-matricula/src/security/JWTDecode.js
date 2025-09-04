export function JWTDecode(token) {
	const base64Url = token.split(".")[1];
	const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
	const jsonPayload = decodeURIComponent(
		window
			.atob(base64)
			.split("")
			.map((c) => `%${(`00${c.charCodeAt(0).toString(16)}`).slice(-2)}`)
			.join(""),
	);

	const result = JSON.parse(jsonPayload);
	const role = result.role;
	const user = result.unique_name;
	const object = { user, role };

	return object;
}
