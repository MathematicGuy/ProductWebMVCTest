const productList = document.getElementById('product-list');
const createProductForm = document.getElementById('create-product-form');
const updateProductForm = document.getElementById('update-product-form');

// Fetch and Display Products
fetch('https://localhost:7116/api/Hang')
    .then(response => response.json())
    .then(products => displayProducts(products));

// Function to display products
function displayProducts(products) {
    productList.innerHTML = ''; // Clear existing list
    products.forEach(product => {
        const productElement = `
      <div>
        <h3>${product.ten_hang_hoa}</h3>
        <p>ID: ${product.id}</p> 
        <button onclick="showUpdateForm(${product.id})">Edit</button>
        <button onclick="deleteProduct(${product.id})">Delete</button>
      </div>
    `;
        productList.innerHTML += productElement;
    });
}

// Functions for CRUD operations 
function createProduct(formData) {
    // ... (Send POST request to '/api/Hang')
}

function updateProduct(productId, formData) {
    // ... (Send PUT request to '/api/Hang/{productId}')
}

function deleteProduct(productId) {
    // ... (Send DELETE request to '/api/Hang/{productId}')
}

function showUpdateForm(productId) {
    // ... (Fetch product details and populate the update form)
}

// Handle form submissions (Update these to call your CRUD functions) 
createProductForm.addEventListener('submit', (event) => {
    event.preventDefault();
    const formData = new FormData(createProductForm);
    createProduct(formData);
});

updateProductForm.addEventListener('submit', (event) => {
    event.preventDefault();
    const formData = new FormData(updateProductForm);
    const productId = updateProductForm.dataset.productId; // Example of getting product ID
    updateProduct(productId, formData);
});
