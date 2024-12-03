using CapaBL;
using CapaEN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DavidVasquez._2024.PruebaTecnica.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoBL _productoBL;
        private readonly CategoriaBL _categoriaBL;

        // Constructor que inyecta la capa de lógica de negocio
        public ProductoController(ProductoBL productoBL, CategoriaBL categoriaBL)
        {
            _productoBL = productoBL;
            _categoriaBL = categoriaBL;
        }

        // GET: ProductoController
        // Acción que muestra el listado de productos
        public async Task<IActionResult> Index(ProductoEN producto = null)
        {
            if (producto == null)
                producto = new ProductoEN();

            if (producto.Top_Aux == 0)
                producto.Top_Aux = 10;
            else if (producto.Top_Aux == -1)
                producto.Top_Aux = 0;

            var productos = await _productoBL.SearchAsync(producto);
            var categoria = await _categoriaBL.GetAllAsync();
            ViewBag.Top = producto.Top_Aux;

            return View(productos);
        }

        // GET: ProductoController/Details/5
        // Acción que muestra el detalle de un producto
        public async Task<IActionResult> Details(int id)
        {
            var producto = await _productoBL.GetByIdAsync(id);
            var categoria = await _categoriaBL.GetByIdAsync(id);
            return View(producto);
        }

        // GET: ProductoController/Create
        // Acción que muestra el formulario para crear un nuevo producto
        public async Task<IActionResult> Create()
        {
            var categorias = await _categoriaBL.GetAllAsync();

            ViewBag.Categorias = categorias.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre
            }).ToList();

            ViewBag.Error = "";
            return View();
        }

        // POST: ProductoController/Create
        // Acción que recibe los datos del formulario y los envía a la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoEN producto)
        {
            try
            {
                int result = await _productoBL.CreateAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categorias = await _categoriaBL.GetAllAsync();
                return View(producto);
            }
        }

        // GET: ProductoController/Edit/5
        // Acción que muestra el formulario para editar un producto existente
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _productoBL.GetByIdAsync(id);
            ViewBag.Categorias = (await _categoriaBL.GetAllAsync()).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == producto.IdCategoria
            }).ToList();
            ViewBag.Error = "";
            return View(producto);
        }

        // POST: ProductoController/Edit/5
        // Acción que recibe los datos para actualizar el producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductoEN producto)
        {
            try
            {
                int result = await _productoBL.UpdateAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categorias = await _categoriaBL.GetAllAsync();
                return View(producto);
            }
        }

        // GET: ProductoController/Delete/5
        // Acción que muestra los datos para confirmar la eliminación de un producto
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _productoBL.GetByIdAsync(id);
            var categoria = await _categoriaBL.GetByIdAsync(id);
            ViewBag.Error = "";
            return View(producto);
        }

        // POST: ProductoController/Delete/5
        // Acción que recibe la información para validar la eliminación de un producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProductoEN producto)
        {
            try
            {
                int result = await _productoBL.DeleteAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.categoria = await _categoriaBL.GetAllAsync();
                return View(producto);
            }
        }
    }
}
