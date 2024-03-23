using HuaXiaLibrary.Data;
using HuaXiaLibrary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HuaXia.Admin.Pages.Equipment
{
	public class CreateModel : PageModel
	{
		private readonly IDatabaseData _db;
		private readonly IWebHostEnvironment _webHostEnvironment;
		[BindProperty]
		public EquipmentFullModel Equipment { get; set; }

		public List<PlayerRoleModel> PlayerRoles { get; set; }

		public List<EquipmentPartModel> EquipmentParts { get; set; }

		public List<PlayerLevelModel> PlayerLevels { get; set; }
		public List<EquipmentGradeModel> EquipmentGrades {  get; set; }

		public CreateModel(IDatabaseData db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public void OnGet()
		{
			PlayerRoles = _db.GetAllPlayerRoles();

			EquipmentParts = _db.GetEquipmentParts();

			PlayerLevels = _db.GetPlayerLevels();
			EquipmentGrades = _db.GetEquipmentGrades();
		}

		public IActionResult OnPost(IFormFile? file)
		{
			ModelState.Remove("Equipment.Id");
			if (file == null)
			{
				ModelState.AddModelError(String.Empty, "È±ÉÙ×°±¸Í¼Æ¬");
			}

			if (ModelState.IsValid)
			{
			
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string equipmentPath = Path.Combine(_webHostEnvironment.WebRootPath + @"\images\equipments");
					using (var fileStream = new FileStream(Path.Combine(equipmentPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					Equipment.Image = Path.Combine(@"\images\equipments\", fileName);

				_db.CreateEquipment(Equipment.Name, Equipment.Description, Equipment.Image, Equipment.PlayerRoleId, Equipment.EquipmentPartId, Equipment.PlayerLevelId, Equipment.EquipmentGradeId);

				return RedirectToPage("Index");
			}
			PlayerRoles = _db.GetAllPlayerRoles();

			EquipmentParts = _db.GetEquipmentParts();

			PlayerLevels = _db.GetPlayerLevels();
			EquipmentGrades = _db.GetEquipmentGrades();

			return Page();


		}
	}
}
