package main

import (
	"database/sql"
	"fmt"
	"net/http"

	"github.com/JuanSalles/ImersaoFullCycleE-Commerce/goAPI/internal/database"
	"github.com/JuanSalles/ImersaoFullCycleE-Commerce/goAPI/internal/services"
	"github.com/JuanSalles/ImersaoFullCycleE-Commerce/goAPI/internal/webserver"
	"github.com/go-chi/chi"
	"github.com/go-chi/chi/middleware"
	_ "github.com/go-sql-driver/mysql"
)

func main() {
	db, err := sql.Open("mysql", "root:root@tcp(localhost:3306)/imersao_fullcycle")

	if err != nil {
		panic(err.Error())
	}

	defer db.Close()

	categoryDB := database.NewCategoryDB(db)
	categoryService := services.NewCategoryService(*categoryDB)

	productDB := database.NewProductDB(db)
	productService := services.NewProductService(*productDB)

	webCategoryHandler := webserver.NewWebCategoryHandler(categoryService)
	webProductHandler := webserver.NewWebProductHandler(productService)

	c := chi.NewRouter()

	c.Use(middleware.Logger)
	c.Use(middleware.Recoverer)

	c.Get("/categories", webCategoryHandler.GetCategories)
	c.Get("/categories/{id}", webCategoryHandler.GetCategoryByID)
	c.Post("/categories", webCategoryHandler.CreateCategory)

	c.Get("/products", webProductHandler.GetProducts)
	c.Get("/products/{id}", webProductHandler.GetProductByID)
	c.Get("/products/category/{id}", webProductHandler.GetProductsByCategoryID)
	c.Post("/products", webProductHandler.CreateProduct)
	
	fmt.Println("Server running on port 8080")
	http.ListenAndServe(":8080", c)
}