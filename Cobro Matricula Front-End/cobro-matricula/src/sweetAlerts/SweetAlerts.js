import Swal from "sweetalert2";

var result;

export async function SwalSuccess(title, text) {
	result = await Swal.fire({
		title: title,
		text: text,
		confirmButtonText: "Listo",
		imageUrl:
			"https://images.pexels.com/photos/990349/pexels-photo-990349.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
		imageWidth: 400,
		imageHeight: 300,
		imageAlt: "Custom image",
		customClass: "text-sm",
	});

	return result;
	// La respuesta se recibe donde se lo llama por lo que no es necesario emplear esta seccion
	// .then((result) => {
	//     if(result.isConfirmed){
	//        return  true;
	//     }
	// });
}

export async function SwalFailed(title, message, footer = "") {
	result = await Swal.fire({
		icon: "error",
		title: title,
		text:
			message.length > 1
				? message.forEach((element) => {
						element.toString();
					})
				: message[0],
		footer: footer,
		customClass: "text-sm",
	});

	return result;

	// .then(result => {
	//     if(result.isConfirmed){
	//         return '/';
	//     }
	// });
}

export async function SwalUpdated(title, text, imageUrl) {
	result = await Swal.fire({
		title: title,
		text: text,
		imageUrl: imageUrl,
		imageWidth: 300,
		imageHeight: 200,
		imageAlt: "Custom image",
		customClass: "text-sm",
	});

	return result;
	//     .then(result => {
	//     if(result.isConfirmed){
	//         return false;
	//     }
	// });
}

export async function SwalDeleted() {
	result = await Swal.fire({
		title: "Estas Seguro?",
		text: "Se eliminará el registro de la base de datos!",
		icon: "warning",
		showCancelButton: true,
		cancelButtonText: "Cancelar",
		confirmButtonColor: "#3085d6",
		cancelButtonColor: "#d33",
		confirmButtonText: "Sí, borralo",
		customClass: "text-sm",
	});

	return result;

	// .then((result) => {
	//   if (result.isConfirmed) {
	//     return true;
	//   }else if(result.dismiss === Swal.DismissReason.cancel)
	//   {
	//     return false;
	//   }
	// });
}

export async function SwalConfirmed() {
	result = await Swal.fire({
		title: "Eliminado!",
		text: "Se eliminó el registro con éxito.",
		icon: "success",
		confirmButtonText: "Listo",
		customClass: "text-sm",
	});

	return result;
}

export async function SwalCancel() {
	result = await Swal.fire({
		title: "Cancelado",
		text: "El usuario no ha sido eliminado :) ",
		icon: "error",
		customClass: "text-sm",
	});

	return result;
}
