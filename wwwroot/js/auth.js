const API = "/api/auth";

/* PASSWORD RULE */

function validPassword(password) {

    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$/;

    return regex.test(password);

}

function googleLogin() {
    window.location.href = "/api/auth/google-login";
}
function googleLogin() {
    window.location.href = "https://localhost:7246/api/auth/google-login";
}
function linkedinLogin() {
    window.location.href = "/api/auth/linkedin-login";
}

/* NAME RULE */

function validName(name) {

    const regex = /^[A-Za-z0-9_]+$/;

    return regex.test(name);

}

/* GMAIL RULE */

function isGmail(email) {

    return /^[a-zA-Z0-9._%+-]+@gmail\.com$/.test(email);

}

/* PASSWORD TOGGLE */

function togglePassword() {

    const pass = document.getElementById("password");

    pass.type = pass.type === "password" ? "text" : "password";

}

/* REGISTER */

const registerForm = document.getElementById("registerForm");

if (registerForm) {

    registerForm.addEventListener("submit", async function (e) {

        e.preventDefault();

        const name = document.getElementById("name").value;
        const email = document.getElementById("email").value;
        const password = document.getElementById("password").value;
        const confirmPassword = document.getElementById("confirmPassword").value;
        const role = document.getElementById("role").value;

        if (!validName(name)) {
            document.getElementById("registerError").innerText =
                "Name can contain only letters, numbers and underscore (_)";
            return;
        }

        if (!isGmail(email)) {
            document.getElementById("registerError").innerText =
                "Only Gmail addresses allowed";
            return;
        }

        if (!validPassword(password)) {
            document.getElementById("registerError").innerText =
                "Password must contain 8 characters, 1 uppercase, 1 number and 1 special character";
            return;
        }

        if (password !== confirmPassword) {
            document.getElementById("registerError").innerText =
                "Passwords do not match";
            return;
        }

        const res = await fetch(API + "/register", {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                name,
                email,
                password,
                confirmPassword,
                role
            })

        });

        const data = await res.text();

        alert(data);

        if (res.ok) {

            window.location.href = "index.html";

        }

    });

}

/* LOGIN */

const loginForm = document.getElementById("loginForm");

if (loginForm) {

    loginForm.addEventListener("submit", async function (e) {

        e.preventDefault();

        const email = document.getElementById("email").value;
        const password = document.getElementById("password").value;

        if (!isGmail(email)) {
            document.getElementById("loginError").innerText = "Only Gmail allowed";
            return;
        }

        const res = await fetch(API + "/login", {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                email,
                password
            })

        });

        const token = await res.text();

        if (!res.ok) {

            document.getElementById("loginError").innerText = token;

            return;

        }

        localStorage.setItem("token", token);

        window.location.href = "dashboard.html";

    });

}