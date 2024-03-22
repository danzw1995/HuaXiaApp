using HuaXiaLibrary.Data;
using HuaXiaLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HuaXia.Web.Pages
{
    public class EquipmentModel : PageModel
    {
        private readonly IDatabaseData _db;

        [BindProperty(SupportsGet = true)]
        public int RoleId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int EquipmentPartId { get; set; }

        public List<PlayerRoleModel> PlayerRoles { get; set; } = new List<PlayerRoleModel>();

        public List<EquipmentPartModel> EquipmentParts { get; set; } = new List<EquipmentPartModel>();

        public List<EquipmentFullModel> Equipments { get; set; } = new List<EquipmentFullModel>();

		[BindProperty(SupportsGet = true)]
		public bool SearchEnabled { get; set; } = false;

        public EquipmentModel(IDatabaseData db)
        {
            _db = db;
        }
        public void OnGet()
        {
            PlayerRoles = _db.GetAllPlayerRoles();
            EquipmentParts = _db.GetEquipmentParts();

            if (SearchEnabled == false)
            {
               PlayerRoleModel defaultRole =  PlayerRoles.First(role => role.Name == "¼Å»Ã·¨Ê¦");
               RoleId = defaultRole.Id;

                EquipmentPartModel defaultEquipmentPart = EquipmentParts.First(part => part.Name == "ÎäÆ÷");
                EquipmentPartId = defaultEquipmentPart.Id;
            }

            Equipments = _db.SearchEquipmentsByRoleAndEquipmentPart(RoleId, EquipmentPartId);

        }
    }
}
