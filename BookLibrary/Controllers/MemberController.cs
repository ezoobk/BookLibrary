using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Dto;
using BookLibrary.Interface;
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
        private readonly IMemberRepository _memberRepository;

        public MemberController(DataContext context, IMapper mapper, IMemberRepository memberRepository)
        {
            _context = context;
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Member>))]
        public IActionResult getMembers()
        {
            var members = _mapper.Map<List<MemberDto>>(_memberRepository.GetMembers());

            if (members is null)
                return NotFound("Members Not Found");

            return Ok(members);
        }

        [HttpGet("{memberId}")]
        public IActionResult getMember(int memberId)
        {
            var member = _mapper.Map<MemberDto>(_memberRepository.GetMember(memberId));

            if (member is null)
                return NotFound("Members Not Found");

            return Ok(member);

        }

        [HttpPost]
        [Route("Add-Member")]
        public IActionResult AddMember([FromBody] MemberDto memberCreate)
        {
            if (memberCreate == null)
                return BadRequest();

            var newMember = _memberRepository.GetMembers()
                .Where(c => c.fullName.Trim().ToUpper() == memberCreate.fullName.Trim().ToUpper()
                 && c.phoneNum == memberCreate.phoneNum)
                .FirstOrDefault();



            if (newMember != null)
            {
                ModelState.AddModelError("", "Member Alrady Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var MemberMap = _mapper.Map<Member>(memberCreate);

            if (!_memberRepository.CreateMember(MemberMap))
            {
                ModelState.AddModelError("", "Somthing Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
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
