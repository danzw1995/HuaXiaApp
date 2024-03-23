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

        public List<EquipmentGradeModel> EquipmentGrades { get; set; }

        [BindProperty(SupportsGet = true)]
        [DisplayName("职业")]
        public int? PlayerRoleId { get; set; }

        [BindProperty(SupportsGet = true)]
		[DisplayName("部位")]
		public int? EquipmentPartId { get; set; }

		[BindProperty(SupportsGet = true)]
		[DisplayName("档次")]
		public int? EquipmentGradeId { get; set; }


		public IndexModel(IDatabaseData db)
        {
            _db = db;
        }
        public void OnGet()
        {
			PlayerRoles = _db.GetAllPlayerRoles();

			EquipmentParts = _db.GetEquipmentParts();

            EquipmentGrades = _db.GetEquipmentGrades();

        }

    }
        
}
