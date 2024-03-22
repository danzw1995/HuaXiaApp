using HuaXiaLibrary.Data;
using HuaXiaLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HuaXia.Admin.Pages.Equipment
{
    public class DetailModel : PageModel
    {
        private readonly IDatabaseData _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public EquipmentFullModel Equipment { get; set; }

        public List<PlayerRoleModel> PlayerRoles { get; set; }

        public List<EquipmentPartModel> EquipmentParts {  get; set; }

        public List<PlayerLevelModel> PlayerLevels { get; set; }

        public DetailModel(IDatabaseData db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public void OnGet(int id)
        {
            PlayerRoles = _db.GetAllPlayerRoles();

            EquipmentParts = _db.GetEquipmentParts();

            PlayerLevels = _db.GetPlayerLevels();

            Equipment = _db.GetEquipmentById(id);
        }

        public IActionResult OnPost(IFormFile? file)
        {
			if (file == null && string.IsNullOrEmpty(Equipment.Image))
			{
				ModelState.AddModelError(String.Empty, "È±ÉÙ×°±¸Í¼Æ¬");
			}

			if (ModelState.IsValid)
            {
				if (!string.IsNullOrEmpty(Equipment.Image))
				{
					string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, Equipment.Image.TrimStart('\\'));

					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}

				string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string equipmentPath = Path.Combine(_webHostEnvironment.WebRootPath +  @"\images\equipments");
                using (var fileStream  = new FileStream(Path.Combine(equipmentPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                Equipment.Image = Path.Combine(@"\images\equipments\", fileName);


				_db.UpdateEquipment(new EquipmentModel
				{
					Id = Equipment.Id,
					Name = Equipment.Name,
					Description = Equipment.Description,
					PlayerLevelId = Equipment.PlayerLevelId,
					PlayerRoleId = Equipment.PlayerRoleId,
					EquipmentPartId = Equipment.EquipmentPartId,
					Image = Equipment.Image,
				});

				return RedirectToPage("Index");


			}

			PlayerRoles = _db.GetAllPlayerRoles();

			EquipmentParts = _db.GetEquipmentParts();

			PlayerLevels = _db.GetPlayerLevels();
			return Page();


		

		}
    }
}
