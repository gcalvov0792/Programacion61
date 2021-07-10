using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBL;

namespace WebApp.Pages.Vehiculo
{
    public class EditModel : PageModel
    {
        private readonly IVehiculoService vehiculoService;
        private readonly IMarcaVehiculoService marcaVehiculoService;

        public EditModel(IVehiculoService vehiculoService, IMarcaVehiculoService marcaVehiculoService)
        {
            this.vehiculoService = vehiculoService;
            this.marcaVehiculoService = marcaVehiculoService;
        }

        [BindProperty]
        public VehiculoEntity Entity { get; set; } = new VehiculoEntity();

        public IEnumerable<MarcaVehiculoEntity> MarcaVehiculoLista { get; set; } = new List<MarcaVehiculoEntity>();

        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }


        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (id.HasValue)
                {
                    Entity = await vehiculoService.GetById(new() { VehiculoId = id });
                }
                MarcaVehiculoLista = await marcaVehiculoService.GetLista();
                return Page();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }



        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Entity.VehiculoId.HasValue)
                {
                    //actualizar
                    var result = await vehiculoService.Update(Entity);

                    if (result.CodeError != 0) throw new Exception(result.MsgError);
                    TempData["Msg"] = "Se actualizo correctamente";
                }
                else
                {

                    //Nuevo
                    var result = await vehiculoService.Create(Entity);

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
