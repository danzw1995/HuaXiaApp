using HuaXiaLibrary.Data;
using HuaXiaLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HuaXia.Admin.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EquipmentController : ControllerBase
	{
		private readonly IDatabaseData _db;

		public EquipmentController(IDatabaseData db)
		{
			_db = db;
		}

		// GET: api/Equipment/GetList
		[HttpGet("GetList")]
		public ActionResult<IEnumerable<EquipmentFullModel>> GetEquipments([FromQuery] int? playerRoleId, [FromQuery] int? equipmentPartId, [FromQuery] int? equipmentGradeId, [FromQuery] int? page = 1, [FromQuery] int? limit = 10)
		{

			List<EquipmentFullModel> equipments = _db.GetEquipments(playerRoleId, equipmentPartId, equipmentGradeId, page, limit);

			return Ok(equipments);
		}

		// DELETE: api/Equipment/id
		[HttpDelete("{id}")]
		public ActionResult DeleteEquipment(int id)
		{

			_db.DeleteEquipment(id);

			return Ok("success");
		}

		// POST: api/Equipment/Import
		[HttpPost("Import")]
		public ActionResult ImportEquipment([FromForm] IFormFile file)
		{

			try
			{

				IWorkbook workbook = new XSSFWorkbook(file.OpenReadStream());

				ISheet sheet = workbook.GetSheetAt(0);

				List<EquipmentModel> equipments = new List<EquipmentModel>();

				IRow headerRow = sheet.GetRow(0);

				Dictionary<string, int> nameDict = new Dictionary<string, int>();
				
				for(int i =0; i < headerRow.Cells.Count; i ++)
				{
					string name = headerRow.GetCell(i).StringCellValue;

					nameDict.Add(name, i);
					
				}


				for (int i = 1; i <= sheet.LastRowNum; i ++)
				{
					IRow row = sheet.GetRow(i);

					equipments.Add(new EquipmentModel
					{
						Name = row.GetCell(nameDict[nameof(EquipmentModel.Name)]).StringCellValue,
						Description = row.GetCell(nameDict[nameof(EquipmentModel.Description)]).StringCellValue,
						Image = row.GetCell(nameDict[nameof(EquipmentModel.Image)]).StringCellValue,
						EquipmentPartId = (int)row.GetCell(nameDict[nameof(EquipmentModel.EquipmentPartId)]).NumericCellValue,
						PlayerLevelId = (int)row.GetCell(nameDict[nameof(EquipmentModel.PlayerLevelId)]).NumericCellValue,
						PlayerRoleId = (int)row.GetCell(nameDict[nameof(EquipmentModel.PlayerRoleId)]).NumericCellValue,
						EquipmentGradeId = (int)row.GetCell(nameDict[nameof(EquipmentModel.EquipmentGradeId)]).NumericCellValue,
					});

				}
				
				foreach(EquipmentModel equipment in equipments)
				{
					_db.CreateEquipment(equipment.Name, equipment.Description, equipment.Image, equipment.PlayerRoleId, equipment.EquipmentPartId, equipment.PlayerLevelId, equipment.EquipmentGradeId);
				}


				return Ok("success");

			} catch (Exception)
			{
				throw;
			}
			

			
		}


	}
}
