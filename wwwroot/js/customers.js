const API = "https://localhost:7246/api/customer";

function getToken() {
    return localStorage.getItem("token");
}

async function loadCustomers() {
    const res = await fetch(API, {
        headers: {
            "Authorization": "Bearer " + getToken()
        }
    });

    const data = await res.json();
    const table = document.getElementById("customersTable");
    table.innerHTML = "";

    data.forEach(c => {
        table.innerHTML += `
        <tr>
            <td>${c.id}</td>
            <td>${c.name}</td>
            <td>${c.email}</td>
            <td>${c.phone}</td>
            <td>${c.company}</td>
            <td>
                <button onclick="deleteCustomer(${c.id})">Delete</button>
            </td>
        </tr>`;
    });
}

async function addCustomer() {
    const customer = {
        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        phone: document.getElementById("phone").value,
        company: document.getElementById("company").value,
        address: document.getElementById("address").value
    };

    await fetch(API, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + getToken()
        },
        body: JSON.stringify(customer)
    });

    loadCustomers();
}

async function deleteCustomer(id) {
    await fetch(`${API}/${id}`, {
        method: "DELETE",
        headers: {
            "Authorization": "Bearer " + getToken()
        }
    });

    loadCustomers();
}

window.onload = loadCustomers;