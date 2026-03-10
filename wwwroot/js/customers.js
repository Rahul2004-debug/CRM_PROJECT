const API = "https://localhost:7246/api/customer";

function getToken() {
    return localStorage.getItem("token");
}

<<<<<<< HEAD
async function loadCustomers() {
=======
function getRole() {
    const token = getToken();
    if (!token) return null;

    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
}

async function loadCustomers() {

>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
    const res = await fetch(API, {
        headers: {
            "Authorization": "Bearer " + getToken()
        }
    });

    const data = await res.json();
<<<<<<< HEAD
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
=======

    const table = document.getElementById("customersTable");
    table.innerHTML = "";

    const role = getRole();

    data.forEach(c => {

        let editButton = "";

        if (role === "Admin") {
            editButton = `<button onclick="editCustomer(${c.id},'${c.name}','${c.email}','${c.phone}','${c.address}','${c.company}')">Edit</button>`;
        }

        table.innerHTML += `
<tr>
<td>${c.id}</td>
<td>${c.name}</td>
<td>${c.email}</td>
<td>${c.phone}</td>
<td>${c.company}</td>
<td>
${editButton}
<button onclick="deleteCustomer(${c.id})">Delete</button>
</td>
</tr>
`;

    });

}

async function addCustomer() {

    const customer = {

        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        phone: document.getElementById("phone").value,
        address: document.getElementById("address").value,
        company: document.getElementById("company").value

>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
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
<<<<<<< HEAD
}

async function deleteCustomer(id) {
=======

}

function editCustomer(id, name, email, phone, address, company) {

    document.getElementById("customerId").value = id;
    document.getElementById("name").value = name;
    document.getElementById("email").value = email;
    document.getElementById("phone").value = phone;
    document.getElementById("address").value = address;
    document.getElementById("company").value = company;

}

async function updateCustomer() {

    const id = document.getElementById("customerId").value;

    const customer = {

        id: id,
        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        phone: document.getElementById("phone").value,
        address: document.getElementById("address").value,
        company: document.getElementById("company").value

    };

    await fetch(API, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + getToken()
        },
        body: JSON.stringify(customer)
    });

    loadCustomers();

}

async function deleteCustomer(id) {

>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
    await fetch(`${API}/${id}`, {
        method: "DELETE",
        headers: {
            "Authorization": "Bearer " + getToken()
        }
    });

    loadCustomers();
<<<<<<< HEAD
=======

>>>>>>> c276e3515c42623d54b4dbbb041f63bed09192cc
}

window.onload = loadCustomers;