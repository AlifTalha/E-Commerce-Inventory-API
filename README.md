"ConnectionStrings": {
"DefaultConnection": "Server=DESKTOP-1DUMAFQ\\SQLEXPRESS;Database=EcommerceInventory;Trusted\_Connection=true;TrustServerCertificate=true;"
}



add-migratation init
update-database



https://localhost:7068/api/auth/register

{
"username": "testuser",
"email": "test@gmail.com",
"password": "Password123"
}

https://localhost:7068/api/auth/login



https://localhost:7068/api/categories    post method



{
"name": "Man dress",
"description": "All item of man dress"
}

https://localhost:7068/api/categories/47400550-985d-4fcd-8572-7967fbb37555      get by id



https://localhost:7068/api/categories/47400550-985d-4fcd-8572-7967fbb37555      Delete by id



https://localhost:7068/api/categories/47400550-985d-4fcd-8572-7967fbb37555     update by id

https://localhost:7068/api/categories/47400550-985d-4fcd-8572-7967fbb37555



https://localhost:7068/api/products    post method

https://localhost:7068/api/products    get all

https://localhost:7068/api/products/7967fbb37555     Update product



https://localhost:7068/api/products/7967fbb37555     Delete product



https://localhost:7068/api/products?page=1\&limit=10   pagination get method





https://localhost:7068/api/products?minPrice=100       Price range Get method 

