using CapaEN;
using CapaBL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace UserInterface.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriaBL _categoriaBL;

        // Constructor que inyecta la capa de lógica de negocio
        public CategoriaController(CategoriaBL categoriaBL)
        {
            _categoriaBL = categoriaBL;
        }

        // GET: CategoriaController
        // Acción que muestra el listado de categorías
        public async Task<IActionResult> Index(CategoriaEN categoria = null)
        {
            if (categoria == null)
                categoria = new CategoriaEN();

            if (categoria.Top_Aux == 0)
                categoria.Top_Aux = 10;
            else if (categoria.Top_Aux == -1)
                categoria.Top_Aux = 0;

            var categorias = await _categoriaBL.SearchAsync(categoria);
            ViewBag.Top = categoria.Top_Aux;

            return View(categorias);
        }

        // GET: CategoriaController/Details/5
        // Acción que muestra el detalle de un registro
        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _categoriaBL.GetByIdAsync(id);
            return View(categoria);
        }

        // GET: CategoriaController/Create
        // Acción que muestra el formulario para crear una nueva categoría
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: CategoriaController/Create
        // Acción que recibe los datos del formulario y los envía a la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaEN categoria)
        {
            try
            {
                int result = await _categoriaBL.CreateAsync(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(categoria);
            }
        }

        // GET: CategoriaController/Edit/5
        // Acción que muestra el formulario para editar una categoría existente
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaBL.GetByIdAsync(id);
            ViewBag.Error = "";
            return View(categoria);
        }

        // POST: CategoriaController/Edit/5
        // Acción que recibe los datos para actualizar la categoría
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaEN categoria)
        {
            try
            {
                int result = await _categoriaBL.UpdateAsync(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(categoria);
            }
        }

        // GET: CategoriaController/Delete/5
        // Acción que muestra los datos para confirmar la eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaBL.GetByIdAsync(id);
            ViewBag.Error = "";
            return View(categoria);
        }

        // POST: CategoriaController/Delete/5
        // Acción que recibe la información para validar la eliminación
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CategoriaEN categoria)
        {
            try
            {
                int result = await _categoriaBL.DeleteAsync(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(categoria);
            }
        }
    }
}
