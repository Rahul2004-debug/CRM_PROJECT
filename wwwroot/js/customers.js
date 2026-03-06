async function loadCustomers() {

    const response = await fetch("/api/customer");

    const customers = await response.json();

    let rows = "";

    customers.forEach(c => {

        rows += `
<tr>
<td>${c.name}</td>
<td>${c.email}</td>
<td>${c.phone}</td>
<td>${c.company}</td>
</tr>
`;

    });

    document.getElementById("customersTable").innerHTML = rows;

}