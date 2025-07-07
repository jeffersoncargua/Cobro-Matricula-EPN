const message = {
    req: {
            name: "El nombre es requerido",
            lastName: "El apellido es requerido",
            city: "La ciudad es requerida",
            email: "El correo es requerido",
            phone: "El telefono es requerido",
            password: "Debe ingresar una contraseña",
            confirmPass: "Debe ingresar la contraseña",
        },

        name: "El nombre debe contener solo caracteres alfabeticos",
        lastName: "El apellido debe contener solo caracteres alfabeticos",
        email: "Debe ingresar un correo con un formato valido",
        phone: "El telefono debe contener solo numeros",
        password: "La contraseña debe contener al menos 8 caracteres, una letra mayuscula, un numero y un caracter especial",
        confirmPass : "Las contraseñas no coinciden"
}

const patterns = {
    letters: /^[a-zA-Z\s]{2,30}$/,
    email: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
    numbers: /^[0-9]{10}$/,
    password: /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d\s])(?=.{8,}).*$/,
};

export {message, patterns}

