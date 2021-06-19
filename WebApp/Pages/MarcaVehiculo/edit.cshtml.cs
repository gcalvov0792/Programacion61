using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBL;
using Entity;

namespace WebApp.Pages.MarcaVehiculo
{
    public class editModel : PageModel
    {
        private readonly IMarcaVehiculoService marcaVehiculoService;

        public editModel(IMarcaVehiculoService marcaVehiculoService)
        {
            this.marcaVehiculoService = marcaVehiculoService;
        }
        
        [BindProperty]
        public MarcaVehiculoEntity Entity { get; set; } = new MarcaVehiculoEntity();

        [BindProperty(SupportsGet =true)]
        public int? id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (id.HasValue)
                {
                    Entity = await marcaVehiculoService.GetById(new() { MarcaVehiculoId = id });
                }
                return Page();
            }
            catch (Exception ex)
            {
                return Content( ex.Message);
            }



        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Entity.MarcaVehiculoId.HasValue)
                {
                    //actualizar
                    var result = await marcaVehiculoService.Update(Entity);

                    if (result.CodeError != 0) throw new Exception(result.MsgError);
                    TempData["Msg"] = "Se actualizo correctamente";
                }
                else 
                {

                    //Nuevo
                    var result = await marcaVehiculoService.Create(Entity);

                    if (result.CodeError != 0) throw new Exception(result.MsgError);
                    TempData["Msg"] = "Se agrego correctamente";

                }
                
                return RedirectToPage("Grid");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }



        }

    }
}
