using HuaXiaLibrary.Data;
using HuaXiaLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace HuaXia.Admin.Pages.Equipment
{
    public class IndexModel : PageModel
    {
        private readonly IDatabaseData _db;

        public List<EquipmentFullModel> Equipments { get; set; }

		public List<PlayerRoleModel> PlayerRoles { get; set; }

		public List<EquipmentPartModel> EquipmentParts { get; set; }

        [BindProperty(SupportsGet = true)]
        [DisplayName("ְҵ")]
        public int? PlayerRoleId { get; set; }

        [BindProperty(SupportsGet = true)]
		[DisplayName("��λ")]
		public int? EquipmentPartId { get; set; }


        public IndexModel(IDatabaseData db)
        {
            _db = db;
        }
        public void OnGet()
        {
			PlayerRoles = _db.GetAllPlayerRoles();

			EquipmentParts = _db.GetEquipmentParts();


			Equipments = _db.SearchEquipmentsByRoleAndEquipmentPart(PlayerRoleId, EquipmentPartId);

        }

      /*  public IActionResult OnPost()
        {
            return RedirectToPage(new {  PlayerRoleId, EquipmentPartId });
        }*/
    }
        
}