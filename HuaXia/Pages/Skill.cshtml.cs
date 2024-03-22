using HuaXiaLibrary.Data;
using HuaXiaLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HuaXia.Web.Pages
{
    public class SkillModel : PageModel
    {
		private readonly IDatabaseData _db;

		[BindProperty(SupportsGet = true)]
        public int RoleId { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; }
        public List<PlayerRoleModel> Roles { get; set; } = new List<PlayerRoleModel>();
        public List<SkillFullModel> Skills { get; set; } = new List<SkillFullModel>();  

        public SkillModel(IDatabaseData db)
        {
			_db = db;
		}
        public void OnGet()
        {
            Roles = _db.GetAllPlayerRoles();

            if (SearchEnabled == false)
            {
                PlayerRoleModel defaultRole = Roles.First(role => role.Name == "¼Å»Ã·¨Ê¦");
                RoleId = defaultRole.Id;
            }
            Skills = _db.SearchSkillsByRole(RoleId);
        }
    }
}
