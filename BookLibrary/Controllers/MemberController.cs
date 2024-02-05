using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MemberController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Member>))]
        public IActionResult getMembers()
        {
            var members = _context.members;

            if (members is null)
                return NotFound("members Not Found");

            return Ok(members);
        }

        [HttpGet("{memberId}")]
        public IActionResult getAuthor(int memberId)
        {
            var member = _context.members.Where(p => p.Id == memberId).FirstOrDefault();

            if (member is null)
                return NotFound("Member Not Found");

            return Ok(member);
        }

        [HttpPost]
        [Route("Add-Member")]
        public IActionResult AddAuthore([FromBody] MemberDto memberDto)
        {
            var member = _context.members.Where(p => p.fullName == memberDto.fullName).FirstOrDefault();

            if (member is null)
            {
                var newMember = new Member
                {
                    fullName = memberDto.fullName,
                    joiningDate = memberDto.joiningDate,
                    phoneNum = memberDto.phoneNum,
                };
                _context.members.Add(newMember);
                _context.SaveChanges();
            }
            else
                return BadRequest("تكرار بيانات");

            return Ok();
        }

        [HttpPut]
        [Route("update-member")]
        public IActionResult updateauthor([FromBody] MemberDto memberDto, [FromQuery] int memberId)
        {
            var memberDb = _context.members.Where(p => p.Id == memberId).FirstOrDefault();
            if (memberDb is null)
                return NotFound("Member is Not Found");

            if (memberDb.fullName != memberDto.fullName)
            {
                var existingMember = _context.members.FirstOrDefault(p => p.fullName == memberDto.fullName);
                if (existingMember != null)
                {
                    return BadRequest("Member name already exists");
                }
            }

            memberDb.fullName = memberDto.fullName;
            memberDb.joiningDate = memberDto.joiningDate;
            memberDb.phoneNum = memberDto.phoneNum;

            _context.SaveChanges();

            return Ok(memberDto);
        }

        [HttpDelete]
        [Route("Delete-member")]
        public async Task<ActionResult<List<Member>>> deleteMember(int id)
        {
            var memberDb = _context.members.Where(p => p.Id == id).FirstOrDefault();
            if (memberDb is null)
                return NotFound("Member is Not Found");

            _context.members.Remove(memberDb);
            await _context.SaveChangesAsync();

            return Ok(memberDb);
        }
    }
}
