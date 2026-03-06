const API = "/api/auth";

/* ---------- REGISTER ---------- */

document.getElementById("registerForm")?.addEventListener("submit", async function (e) {

    e.preventDefault();
    clearErrors();

    const name = document.getElementById("name").value.trim();
    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;
    const role = document.getElementById("role").value;

    let valid = true;

    if (name.length < 3) {
        showError("nameError", "Name must be at least 3 characters");
        valid = false;
    }

    if (!validateEmail(email)) {
        showError("emailError", "Please enter a valid email");
        valid = false;
    }

    if (password.length < 8) {
        showError("passwordError", "Password must contain at least 8 characters");
        valid = false;
    }

    if (!valid) return;

    try {

        const res = await fetch(API + "/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ name, email, password, role })
        });

        const data = await res.text();

        if (!res.ok) {
            showError("formError", data);
            return;
        }

        showSuccess("Account created successfully!");

        setTimeout(() => {
            window.location.href = "/";
        }, 1500);

    } catch (err) {

        showError("formError", "Server error. Please try again.");

    }

});


/* ---------- LOGIN ---------- */

document.getElementById("loginForm")?.addEventListener("submit", async function (e) {

    e.preventDefault();
    clearErrors();

    const email = document.getElementById("email").value.trim();
    const password = document.getElementById("password").value;

    let valid = true;

    if (!validateEmail(email)) {
        showError("emailError", "Enter a valid email");
        valid = false;
    }

    if (password.length < 8) {
        showError("passwordError", "Password must contain at least 8 characters");
        valid = false;
    }

    if (!valid) return;

    try {

        const res = await fetch(API + "/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ email, password })
        });

        const token = await res.text();

        if (!res.ok) {
            showError("formError", token);
            return;
        }

        localStorage.setItem("token", token);

        showSuccess("Login successful!");

        setTimeout(() => {
            window.location.href = "/dashboard.html";
        }, 1000);

    } catch {

        showError("formError", "Server error. Try again.");

    }

});


/* ---------- HELPERS ---------- */

function validateEmail(email) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
}

function showError(id, message) {
    const el = document.getElementById(id);
    if (el) el.innerText = message;
}

function clearErrors() {

    const errors = document.querySelectorAll(".error-msg");

    errors.forEach(e => {
        e.innerText = "";
    });

    const formError = document.getElementById("formError");
    if (formError) formError.innerText = "";

}

function showSuccess(message) {

    const box = document.getElementById("successBox");

    if (box) {
        box.style.display = "block";
        box.innerText = message;
    }

}
