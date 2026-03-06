const API = "/api/auth";

document.getElementById("registerForm")?.addEventListener("submit", async function (e) {

    e.preventDefault();

    const name = document.getElementById("name").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const role = document.getElementById("role").value;

    const res = await fetch(API + "/register", {

        method: "POST",

        headers: {

            "Content-Type": "application/json"

        },

        body: JSON.stringify({

            name,
            email,
            password,
            role

        })

    });

    const data = await res.text();

    alert(data);

    window.location.href = "/";

});

document.getElementById("loginForm")?.addEventListener("submit", async function (e) {

    e.preventDefault();

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

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

    localStorage.setItem("token", token);

    window.location.href = "/dashboard.html";

});

function logout() {

    localStorage.removeItem("token");

    window.location.href = "/";

}