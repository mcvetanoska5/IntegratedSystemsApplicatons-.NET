using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService) => _memberService = memberService;

        [HttpGet("/api/members")]
        public IActionResult GetMembersApi() => Ok(_memberService.GetAllMembers());

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("")]
        public IActionResult Index() => View(_memberService.GetAllMembers());

        [HttpGet("/api/members/{id}")]
        public IActionResult GetMemberApi(Guid id) => Ok(_memberService.GetMemberById(id));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Details/{id}")]
        public IActionResult Details(Guid id) => View(_memberService.GetMemberById(id));

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Create")]
        public IActionResult Create() => View();

        [HttpPost("/api/members")]
        public IActionResult CreateApi(Member member)
        {
            _memberService.InsertMember(member);
            return Ok(member);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Create")]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid) { _memberService.InsertMember(member); return RedirectToAction(nameof(Index)); }
            return View(member);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(Guid id) => View(_memberService.GetMemberById(id));

        [HttpPut("/api/members/{id}")]
        public IActionResult EditApi(Guid id, Member member) { _memberService.UpdateMember(member); return Ok(member); }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(Guid id, Member member)
        {
            if (ModelState.IsValid) { _memberService.UpdateMember(member); return RedirectToAction(nameof(Index)); }
            return View(member);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(Guid id) => View(_memberService.GetMemberById(id));

        [HttpDelete("/api/members/{id}")]
        public IActionResult DeleteApi(Guid id) { _memberService.DeleteMember(id); return NoContent(); }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id) { _memberService.DeleteMember(id); return RedirectToAction(nameof(Index)); }
    }
}