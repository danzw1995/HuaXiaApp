using HuaXiaLibrary.Data;
using HuaXiaLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public ActionResult<IEnumerable<EquipmentFullModel>> GetEquipments([FromQuery] int? playerRoleId, [FromQuery] int? equipmentPartId, [FromQuery] int? page = 1, [FromQuery] int? limit = 10)
		{

			List<EquipmentFullModel> equipments = _db.GetEquipments(playerRoleId, equipmentPartId, page, limit);

			return Ok(equipments);
		}

		// DELETE: api/Equipment/id
		[HttpDelete("{id}")]
		public ActionResult<IEnumerable<EquipmentFullModel>> DeleteEquipment(int id)
		{

			_db.DeleteEquipment(id);

			return Ok("success");
		}
	}
}
