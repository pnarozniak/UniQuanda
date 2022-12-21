using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Test.GenerateTest
{
    public class GenerateTestRequestDTO
    {
        [Required]
        [IEnumerableSizeValidation(1, 5)]
        public IEnumerable<int> TagIds { get; set; }
    }

    public class GenerateTestResponseDTO
    {
        public int IdTest { get; set; }
    }
}
